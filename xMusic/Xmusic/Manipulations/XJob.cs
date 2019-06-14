using SoundStretch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xmusic.Extensions;

namespace Xmusic.Manipulations
{
    public class XJob
    {
        public XJob()
        {
            
        }
        public string SourceFileName { get; set; }
        public string AlternativeOutputPath { get; set; }

        public bool UseBackGroundProcess { get; set; }
        public byte[] OutputData { get;  set; }

        public bool ReturnData { get; set; }
        public byte[] SourceData { get; set; }

        public XFileType ISourceFileType { get; set; }
        public XFileType SourceFileType
        {
            get
            {
                return SourceFileName.RetrieveExtension();
            }
        }
        public string ResulFileName { get; set; }
        public DateTime StartTime { get;  set; }
        public DateTime EndTime { get;  set; }
    }

    public class XConvertJob : XJob
    {
        public XFileType? DestinationFileType { get; set; }
    }

    public class XTempoJob : XJob
    {
        public float TempoDelta { get; set; }
        public float PitchDelta { get; set; }
    }

    public class XJobResult
    {
        public string TempFileName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public byte[] OutputData { get; set; }

        public XJobResult()
        {
            StartTime = DateTime.Now;
        }
    }
}
