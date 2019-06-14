using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoundStretch;
using Xmusic.Extensions;

namespace Xmusic
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
        private int totalFiles = 1;
        private int currentFileIndex = 0;

        public void ProcessDirectoryTempoJob(string dir, float? tempo = null, float? pitch = null)
        {
            var allFiles = Directory.GetFiles(dir);
            totalFiles = allFiles.Count();
            foreach (var file in allFiles)
            {
                try
                {
                    currentFileIndex++;
                    ProcessSingleTempoJob(file, tempo,pitch);
                }
                catch (Exception ex)
                {
                    var trace = ex.StackTrace;
                    XMusicLogger.AddLog(ex.Message);
                    XMusicLogger.AddLog(trace);
                    RemoveTempFiles();
                    throw ex;
                }
            }
        }
        public void ProcessSingleTempoJob(string sourceFileName, float? tempo = null, float? pitch = null)
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
            if (totalFiles > 1)
            {
                msg = $"Processing ({currentFileIndex}) Out Of ({totalFiles}) : {msg}";
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
            string mixWavInFile = string.Empty;
            string mixWavOutFile = string.Empty;
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
                mixWavInFile = mp3ToWaveFile;
                tempFiles.Add(mp3ToWaveFile);
            }
            if (sourceType == XFileType.Wav)
            {
                mixWavInFile = sourceFileName;
            }
            mixWavOutFile = mixWavInFile.GenerateOutPutPath(TempoKey);
            var mixJob = new MusicService(mixWavInFile, mixWavOutFile, XActionType.TempoAdjustment);
            if (this.TempoDelta.HasValue)
            {
                mixJob.TempoDelta = TempoDelta.Value;
                mixJob.PitchDelta = this.PitchDelta ?? TempoDelta.Value / 10;
            }
            Actions.Add(mixJob);
            if (sourceType != XFileType.Wav)
                tempFiles.Add(mixWavInFile);
            tempFiles.Add(mixWavOutFile);

            var finalFile = sourceFileName.GenerateOutPutPath(XFileType.Mp3,ConvertionKey);
            Actions.Add(new MusicService(mixWavOutFile, finalFile, XActionType.Convertion));

        }
    }

    public class MusicService
    {
        public XActionType Action;
        public string SourceFile;
        public string DestinationFile;
        public float TempoDelta { get; set; }
        public float PitchDelta { get; set; }
        public float RateDelta { get; set; }
        public MusicService(string srcFile, string destinationFile, XActionType action)
        {
            this.SourceFile = srcFile;
            this.DestinationFile = destinationFile;
            this.Action = action;
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
    }
}
