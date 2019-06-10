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
                if (job.SourceFileType == XFileType.Wma)
                {
                    var cJob = new XConvertJob { SourceFileName = job.SourceFileName, DestinationFileType = XFileType.Mp3 , DeleteTemporaryFiles = job.DeleteTemporaryFiles};
                    xConverter.Convert(cJob);
                }
                xTempoProcessor.Process(xConverter, job);
            }
        }

        public void Dispose()
        {
            
        }
    }
 }
