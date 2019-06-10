using NAudio.Lame;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xmusic.Extensions;

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
    public class XSoundProcessor
    {
        public static void Process(XSoundActionParameters param)
        {
            param.StartTime = DateTime.Now;
            switch (param.Action)
            {
                case XActionType.Convertion:
                    XConverter.DoWork(param);
                    break;
                case XActionType.TempoAdjustment:
                    XTempoProcessor.Process(param);
                    break;
                default:
                    break;
            }
            param.EndTime = DateTime.Now;
        }
    }

    public class XSoundActionParameters
    {
        public XFileType SourceFileType
        {
            get
            {
                return SourceFileName.RetrieveExtension();
            }
        }
        public XFileType? DestinationFileType { get; set; }
        public XActionType Action { get; set; }

        public string SourceFileName { get; set; }
        public string AlternativeOutputPath { get; set; }

        public string OutputResultFilePath { get; set; }
        public XActionResult LastActionResult { get; set; }

        public bool DeleteTemporaryFiles { get; set; }
        public bool UseBackGroundProcess { get; set; }

        public List<string> TempFiles { get; set; }

        public XSoundActionParameters()
        {
            TempFiles = new List<string>();
        }

        internal void AddTempFile()
        {
            TempFiles.Add(SourceFileName);
        }

        public XSoundActionParameters InnerParameters { get; set; }
        public DateTime StartTime { get;  set; }
        public DateTime EndTime { get;  set; }
        public string TotalTimeSpan
        {
            get
            {
                return StartTime.GetSpanString(EndTime);
            }
        }

        internal void InitParameters(XFileType destinationFileType, string sourceFileName)
        {
            InnerParameters = new XSoundActionParameters
            {
                DestinationFileType = destinationFileType,
                SourceFileName = sourceFileName,
            };
        }

        internal void AppendTempParameters()
        {
            TempFiles.AddRange(InnerParameters.TempFiles);
        }
    }
}
