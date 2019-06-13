using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoundStretch;
using Xmusic.Extensions;

namespace Xmusic.Manipulations
{
    public class MusicServiceProvider 
    {
        private List<MusicService>  Actions;
        //XJobResult Result;
        private List<string> tempFiles;
        private XConverter xConverter;
        private XTempoProcessor xTempoProcessor;
        public float? TempoDelta { get; set; }
        public float? PitchDelta { get; set; }

        public void ProcessTempoJob(string sourceFileName, float? tempo = null, float? pitch = null)
        {
            TempoDelta = tempo;
            PitchDelta = pitch;
            XMusicLogger.Init();
            GenerateWorkFlow(sourceFileName);
            using (xConverter = new XConverter())
            using (xTempoProcessor = new XTempoProcessor())
            {
                Process();
            }
            RemoveTempFiles();
            XMusicLogger.AddLog($"Finished !!");
        }

        private void RemoveTempFiles()
        {
            foreach (var tmpFile in tempFiles)
            {
                if (File.Exists(tmpFile))
                {
                    File.Delete(tmpFile);
                    XMusicLogger.AddLog($"Deleting Temporary File - {tmpFile} ....");
                }
            }
        }

        private void Process()
        {
            foreach (var action in Actions)
            {
                Log(action);
                Perform(action);
            }
        }

        private void Perform(MusicService job)
        {
            if (job.Action == XActionType.Convertion)
            {
                xConverter.Convert(job);
            }
            if (job.Action == XActionType.TempoAdjustment)
            {
                xTempoProcessor.Process(job);
            }
        }

        private void Log(MusicService job)
        {
            string msg = string.Empty;
            if (job.Action == XActionType.Convertion)
            {
                msg = $"Converting { job.SourceFileType.ToString()} To { job.DestinationFileType.ToString() }";
            }
            if (job.Action == XActionType.TempoAdjustment)
            {
                msg = $"Processing Tempo Adjustment ....";
            }
            XMusicLogger.AddLog(msg);
        }
        private string ConvertionKey = "Converted";
        private string TempoKey = "Tempo Adjusted";
        private void GenerateWorkFlow(string sourceFileName)
        {
            tempFiles = new List<string>();
            var sourceType = sourceFileName.RetrieveExtension();
            string tmp = sourceFileName;
            Actions = new List<MusicService>();
            string mixWavFile = string.Empty;

            if (sourceType == XFileType.Wma)
            {
                var wmaToMp3File = tmp.GenerateGuidPath(XFileType.Mp3, ConvertionKey);
                Actions.Add(new MusicService(tmp, wmaToMp3File, XActionType.Convertion));
                sourceType = XFileType.Mp3;
                tmp = wmaToMp3File;
                tempFiles.Add(wmaToMp3File);
            }
            if (sourceType == XFileType.Mp3)
            {
                var mp3ToWaveFile = tmp.GenerateGuidPath(XFileType.Wav, ConvertionKey);
                Actions.Add(new MusicService(tmp, mp3ToWaveFile,XActionType.Convertion));
                mixWavFile = mp3ToWaveFile;
                tempFiles.Add(mp3ToWaveFile);
            }
            var mixOutFile = mixWavFile.GenerateOutPutPath(TempoKey);
            var mixJob = new MusicService(mixWavFile, mixOutFile, XActionType.TempoAdjustment);
            if (this.TempoDelta.HasValue)
            {
                mixJob.ExecutionParameters.TempoDelta = TempoDelta.Value;
                mixJob.ExecutionParameters.PitchDelta = this.PitchDelta ?? TempoDelta.Value / 10;
            }
            Actions.Add(mixJob);
            tempFiles.Add(mixWavFile);
            tempFiles.Add(mixOutFile);

            var finalFile = sourceFileName.GenerateOutPutPath(XFileType.Mp3,ConvertionKey);
            Actions.Add(new MusicService(mixOutFile, finalFile, XActionType.Convertion));

        }
    }

    public class MusicService
    {
        public XActionType Action;
        public string SourceFile;
        public string DestinationFile;

        public MusicService(string srcFile, string destinationFile, XActionType action)
        {
            this.SourceFile = srcFile;
            this.DestinationFile = destinationFile;
            this.Action = action;
            ExecutionParameters = new RunParameters();
        }

        public XFileType SourceFileType
        {
            get
            {
                return SourceFile.RetrieveExtension();
            }
        }

        public XFileType DestinationFileType
        {
            get
            {
                return DestinationFile.RetrieveExtension();
            }
        }

        public RunParameters ExecutionParameters { get;  set; }
    }
}
