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
    public class XMp3Converter
    {
        internal static void Convert(XConvertJob param)
        {
            switch (param.DestinationFileType.Value)
            {
                case XFileType.Wav:
                    Mp3ToWav(param);
                    break;
                case XFileType.Wma:
                    Mp3ToWma(param);
                    break;
                default:
                    break;
            }
        }

        private static void Mp3ToWma(XConvertJob param)
        {
            throw new NotImplementedException();
        }

        private static void Mp3ToWav(XConvertJob param)
        {
            var outputFile = param.SourceFileName.ReplaceExtension(XFileType.Wav);
            using (var fs = new FileStream(outputFile, FileMode.Create))
            { }
            using (var reader = new Mp3FileReader(param.SourceFileName))
            {
                using (WaveStream pcmStream = WaveFormatConversionStream.CreatePcmStream(reader))
                {
                    WaveFileWriter.CreateWaveFile(outputFile, pcmStream);
                }
            }
            param.ResulFileName = outputFile;
        }
    }
}
