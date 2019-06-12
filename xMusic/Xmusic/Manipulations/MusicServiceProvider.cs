using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xmusic.Extensions;

namespace Xmusic.Manipulations
{
    public class MusicServiceProvider
    {
        private List<MusicService>  Actions;
        XJobResult Result;
        private List<string> tempFiles;
        private XConverter xConverter;
        private XTempoProcessor xTempoProcessor;

        public void ProcessTempoJob(string sourceFileName)
        {
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

        private void GenerateWorkFlow(string sourceFileName)
        {
            tempFiles = new List<string>();
            var sourceType = sourceFileName.RetrieveExtension();
            string tmp = sourceFileName;
            Actions = new List<MusicService>();
            string mixWavFile = string.Empty;

            if (sourceType == XFileType.Wma)
            {
                var wmaToMp3File = tmp.GenerateGuidPath(XFileType.Mp3);
                Actions.Add(new MusicService(tmp, wmaToMp3File, XActionType.Convertion));
                sourceType = XFileType.Mp3;
                tmp = wmaToMp3File;
                tempFiles.Add(wmaToMp3File);
            }
            if (sourceType == XFileType.Mp3)
            {
                var mp3ToWaveFile = tmp.GenerateGuidPath(XFileType.Wav);
                Actions.Add(new MusicService(tmp, mp3ToWaveFile,XActionType.Convertion));
                mixWavFile = mp3ToWaveFile;
                tempFiles.Add(mp3ToWaveFile);
            }
            var mixOutFile = mixWavFile.GenerateOutPutPath();
            Actions.Add(new MusicService(mixWavFile, mixOutFile, XActionType.TempoAdjustment));
            tempFiles.Add(mixWavFile);
            tempFiles.Add(mixOutFile);

            var finalFile = sourceFileName.GenerateOutPutPath(XFileType.Mp3);
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
