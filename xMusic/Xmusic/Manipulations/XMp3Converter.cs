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
            if (param.SourceData != null)
            {

            }
            else
            {
                using (var fs = new FileStream(outputFile, FileMode.Create))
                { }
            }
            using (var reader = GetReader(param))
            {
                using (WaveStream pcmStream = WaveFormatConversionStream.CreatePcmStream(reader))
                {
                    WaveFileWriter.CreateWaveFile(outputFile, pcmStream);
                }
            }
            param.ResulFileName = outputFile;
        }

        private static Mp3FileReader GetReader(XConvertJob param)
        {
            if (param.SourceData != null)
            {
                var mStream = new MemoryStream(param.SourceData.Length);
                return new Mp3FileReader(mStream);
            }
            return new Mp3FileReader(param.SourceFileName);
        }
    }
}
