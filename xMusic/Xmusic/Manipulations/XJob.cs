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
        public string SourceFileName { get; set; }
        public RunParameters ExecutionParameters { get; set; }
        public string AlternativeOutputPath { get; set; }

        public bool DeleteTemporaryFiles { get; set; }
        public bool UseBackGroundProcess { get; set; }

        public List<string> TempFiles { get; set; }
        public XFileType SourceFileType
        {
            get
            {
                return SourceFileName.RetrieveExtension();
            }
        }
    }

    public class XConvertJob : XJob
    {
        public XFileType? DestinationFileType { get; set; }
        public byte[] OutputData { get; internal set; }
        public string ResulFileName { get;  set; }
        public bool ReturnData { get;  set; }
    }
}
