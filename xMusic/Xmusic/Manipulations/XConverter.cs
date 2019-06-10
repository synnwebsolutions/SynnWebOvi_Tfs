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
    public static class XConverter
    {
        #region From Wav


        internal static void FromWav(XSoundActionParameters param)
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
        }

        internal static void DoWork(XSoundActionParameters param)
        {
            if (param.DestinationFileType.HasValue)
            {
                switch (param.SourceFileType)
                {
                    case XFileType.Wav:
                        XConverter.FromWav(param);
                        break;
                    case XFileType.Mp3:
                        XConverter.FromMp3(param);
                        break;
                    case XFileType.Wma:
                        XConverter.FromWma(param);
                        break;
                    default:
                        break;
                }
            }
        }

        private static void WavToWma(XSoundActionParameters param)
        {
            throw new NotImplementedException();
        }

        private static void WavToMp3(XSoundActionParameters param)
        {
            var savetofilename = param.SourceFileName.ReplaceExtension(XFileType.Mp3);
            using (var rdr = new WaveFileReader(param.SourceFileName))
            using (var wtr = new LameMP3FileWriter(savetofilename, rdr.WaveFormat, LAMEPreset.VBR_90))
            {
                rdr.CopyTo(wtr);
            }
            ReportSucces(param, savetofilename);
        }

        #endregion

        #region From Mp3


        internal static void FromMp3(XSoundActionParameters param)
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

        private static void Mp3ToWma(XSoundActionParameters param)
        {
            throw new NotImplementedException();
        }

        private static void Mp3ToWav(XSoundActionParameters param)
        {
            var outputFile = param.SourceFileName.ReplaceExtension(XFileType.Wav);
            using (var fs = new FileStream(outputFile, FileMode.Create))
            {  }
            using (var reader = new Mp3FileReader(param.SourceFileName))
            {
                using (WaveStream pcmStream = WaveFormatConversionStream.CreatePcmStream(reader))
                {
                    WaveFileWriter.CreateWaveFile(outputFile, pcmStream);
                }
            }
            ReportSucces(param, outputFile);
        }

        #endregion

        #region From Wma

        internal static void FromWma(XSoundActionParameters param)
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

        private static void WmaToMp3(XSoundActionParameters param)
        {
            var targetFilename = param.SourceFileName.GenerateOutPutPath(XFileType.Mp3);
            using (var reader = new NAudio.Wave.AudioFileReader(param.SourceFileName))
            using (var writer = new NAudio.Lame.LameMP3FileWriter(targetFilename, reader.WaveFormat, NAudio.Lame.LAMEPreset.STANDARD))
            {
                reader.CopyTo(writer);
            }
            ReportSucces(param, targetFilename);
        }

        private static void WmaToWav(XSoundActionParameters param)
        {
            var outputFile = param.SourceFileName.ReplaceExtension(XFileType.Wav);
            //using (var reader = new NAudio.Wa (param.SourceFileName))
            //{
            //    using (WaveStream pcmStream = WaveFormatConversionStream.CreatePcmStream(reader))
            //    {
            //        WaveFileWriter.CreateWaveFile(outputFile, pcmStream);
            //    }
            //}
            ReportSucces(param, outputFile);
        }

        #endregion

        private static void ReportSucces(XSoundActionParameters param, object outputResult)
        {
            param.LastActionResult = XActionResult.Success;
            param.OutputResultFilePath = outputResult.ToString();
            param.TempFiles.Add(param.SourceFileName);
        }
    }
}
