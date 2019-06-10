using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xmusic.Extensions;

namespace Xmusic.Manipulations
{
    public class XWmaConverter
    {
        internal static void Convert(XConvertJob param)
        {
            switch (param.DestinationFileType.Value)
            {
                case XFileType.Wav:
                    WmaToWav(param);
                    break;
                case XFileType.Mp3:
                    WmaToMp3(param);
                    break;
                default:
                    break;
            }
        }

        private static void WmaToMp3(XConvertJob param)
        {
            var targetFilename = param.SourceFileName.GenerateOutPutPath(XFileType.Mp3);
            using (var reader = new NAudio.Wave.AudioFileReader(param.SourceFileName))
            using (var writer = new NAudio.Lame.LameMP3FileWriter(targetFilename, reader.WaveFormat, NAudio.Lame.LAMEPreset.STANDARD))
            {
                reader.CopyTo(writer);
            }
            param.ResulFileName = targetFilename;
        }

        private static void WmaToWav(XConvertJob param)
        {
            var outputFile = param.SourceFileName.ReplaceExtension(XFileType.Wav);
            //using (var reader = new NAudio.Wa (param.SourceFileName))
            //{
            //    using (WaveStream pcmStream = WaveFormatConversionStream.CreatePcmStream(reader))
            //    {
            //        WaveFileWriter.CreateWaveFile(outputFile, pcmStream);
            //    }
            //}
        }
    }
}
