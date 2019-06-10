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
                    var wmaToMp3Job = new XConvertJob { SourceFileName = job.SourceFileName, DestinationFileType = XFileType.Mp3, ReturnData = true };
                    xConverter.Convert(wmaToMp3Job);

                    var mp3ToWavJob = new XConvertJob { SourceData = wmaToMp3Job.OutputData, DestinationFileType = XFileType.Wav, ReturnData = true };
                    ProccessMp3(xConverter, xTempoProcessor, mp3ToWavJob);
                }
                if (job.SourceFileType == XFileType.Mp3)
                {
                    var mp3ToWavJob = new XConvertJob { SourceFileName = job.SourceFileName, DestinationFileType = XFileType.Wav, ReturnData = true };
                    ProccessMp3(xConverter, xTempoProcessor, mp3ToWavJob);
                }
                job.EndTime = DateTime.Now;
            }
        }

        private static void ProccessMp3(XConverter xConverter, XTempoProcessor xTempoProcessor, XConvertJob mp3ToWavJob)
        {
            xConverter.Convert(mp3ToWavJob);

            var wavTempoJob = new XTempoJob { SourceData = mp3ToWavJob.OutputData, SourceFileName = mp3ToWavJob.ResulFileName, ReturnData = true };
            xTempoProcessor.Process(wavTempoJob);

            var afterTempoWavToMp3Job = new XConvertJob { SourceData = wavTempoJob.OutputData, DestinationFileType = XFileType.Mp3 };
            xConverter.Convert(mp3ToWavJob);

            if (afterTempoWavToMp3Job.SourceData != null)
            {
                File.WriteAllBytes(afterTempoWavToMp3Job.SourceFileName, afterTempoWavToMp3Job.SourceData);
            }
        }

        public void Dispose()
        {
            
        }
    }
 }
