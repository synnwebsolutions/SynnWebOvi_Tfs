using NAudio.Lame;
using NAudio.Wave;
using SoundStretch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xmusic.Extensions;
using Xmusic.Manipulations;

namespace Xmusic
{
    public enum XFileType
    {
        Wav,
        Mp3,
        Wma
    }
    public enum XActionType
    {
        Convertion,
        TempoAdjustment
    }
    public enum XActionResult
    {
        Success,
        Error,
        Other
    }
    public class XSoundProcessor : IDisposable
    {
        public void ProcessTempoJob(XJob job)
        {
            using (var xConverter = new XConverter())
            using (var xTempoProcessor = new XTempoProcessor())
            {
                job.StartTime = DateTime.Now;
                if (job.SourceFileType == XFileType.Wma)
                {
                    XJobResult res = xConverter.Convert(job.SourceFileName, XFileType.Mp3);
                    SaveTempFile(res.TempFileName,res.OutputData);

                    ProccessMp3(xConverter, xTempoProcessor, res.TempFileName, job.SourceFileName);
                }
                if (job.SourceFileType == XFileType.Mp3)
                {
                    ProccessMp3(xConverter, xTempoProcessor, job.SourceFileName, job.SourceFileName);
                }
                job.EndTime = DateTime.Now;
            }
        }

        private void SaveTempFile(string tmpPath, byte[] outputData)
        {
            if (outputData != null)
            {
                File.WriteAllBytes(tmpPath, outputData);
            }
        }

        private  void ProccessMp3(XConverter xConverter, XTempoProcessor xTempoProcessor, string  mp3Path, string mainSourceFileName)
        {
            XJobResult res = xConverter.Convert(mp3Path, XFileType.Wav);
            SaveTempFile(res.TempFileName, res.OutputData);

            var wavTempoJob = new XTempoJob { SourceFileName = res.TempFileName, ReturnData = true };
            xTempoProcessor.Process(wavTempoJob);

            //var afterTempoWavToMp3Job = new XConvertJob { SourceData = wavTempoJob.OutputData, DestinationFileType = XFileType.Mp3 };
            SaveTempFile(mainSourceFileName.GenerateGuidPath(XFileType.Mp3), wavTempoJob.OutputData);
            //xConverter.Convert(mp3ToWavJob);

            //if (afterTempoWavToMp3Job.SourceData != null)
            //{
            //    File.WriteAllBytes(afterTempoWavToMp3Job.SourceFileName, afterTempoWavToMp3Job.SourceData);
            //}
        }

        public void Dispose()
        {
            
        }
    }

    public static class XMusicLogger
    {
        static List<string> Logs;
        public static void AddLog(string l)
        {
            if (Logs == null)
                Logs = new List<string>();

            Logs.Add(l);
            Console.WriteLine($"{ DateTime.Now} : {l}");
        }
        public static void Init()
        {
            Logs = new List<string>();
        }
        public static string GetLogs()
        {
            return string.Join(Environment.NewLine, Logs);
        }
    }
 }
