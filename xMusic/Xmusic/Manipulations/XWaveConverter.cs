using NAudio.Lame;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xmusic.Extensions;

namespace Xmusic.Manipulations
{
    public class XWaveConverter
    {
        internal static void Convert(XConvertJob param)
        {
            switch (param.DestinationFileType.Value)
            {
                case XFileType.Mp3:
                    WavToMp3(param);
                    break;
                case XFileType.Wma:
                    WavToWma(param);
                    break;
                default:
                    break;
            }
            //Finalize(param);
        }

        private static void WavToWma(XConvertJob param)
        {
            throw new NotImplementedException();
        }

        private static void WavToMp3(XConvertJob param)
        {
            var savetofilename = param.SourceFileName.ReplaceExtension(XFileType.Mp3);
            using (var rdr = GetReader(param))
            using (var wtr = new LameMP3FileWriter(savetofilename, rdr.WaveFormat, LAMEPreset.VBR_90))
            {
                rdr.CopyTo(wtr);
            }
            param.ResulFileName = savetofilename;
        }

        private static WaveFileReader GetReader(XConvertJob param)
        {
            if (param.SourceData != null)
            {
                var mStream = new MemoryStream(param.SourceData.Length);
                return new WaveFileReader(mStream);
            }
            return new WaveFileReader(param.SourceFileName);
        }
    }
}
