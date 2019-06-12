using NAudio.Lame;
using NAudio.Wave;
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
    public class XConverter : IDisposable
    {

        public XJobResult Convert(string srcPath,  XFileType dst)
        {
            var res = new XJobResult();
            XConvertJob p = new XConvertJob();
            p.ISourceFileType = srcPath.RetrieveExtension();
            p.AlternativeOutputPath = srcPath.GenerateGuidPath(dst);
            p.DestinationFileType = dst;
            p.SourceFileName = srcPath;
            res.EndTime = DateTime.Now;
            Convert(p);
            XMusicLogger.AddLog($"{DateTime.Now} : Converting {srcPath} to {p.AlternativeOutputPath}");
            res.OutputData = File.ReadAllBytes(p.AlternativeOutputPath);
            File.Delete(p.AlternativeOutputPath);
            res.TempFileName = p.AlternativeOutputPath;
            return res;
        }

        private void Convert(XConvertJob param)
        {
            DoWork(param);
        }

        private static void DoWork(XConvertJob param)
        {
            switch (param.SourceFileType)
            {
                case XFileType.Wav:
                    switch (param.DestinationFileType.Value)
                    {
                        case XFileType.Mp3:
                            WavToMp3(param);
                            break;
                        case XFileType.Wma:
                            //WavToWma(param);
                            break;
                        default:
                            break;
                    }
                    break;
                case XFileType.Mp3:
                    switch (param.DestinationFileType.Value)
                    {
                        case XFileType.Wav:
                            Mp3ToWav(param);
                            break;
                        case XFileType.Wma:
                            //Mp3ToWma(param);
                            break;
                        default:
                            break;
                    }
                    break;
                case XFileType.Wma:
                    switch (param.DestinationFileType.Value)
                    {
                        case XFileType.Wav:
                            //WmaToWav(param);
                            break;
                        case XFileType.Mp3:
                            WmaToMp3(param);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        internal void Convert(MusicService job)
        {
            var convertJob = new XConvertJob();
            convertJob.SourceFileName = job.SourceFile;
            convertJob.AlternativeOutputPath = job.DestinationFile;
            convertJob.DestinationFileType = job.DestinationFileType;
            DoWork(convertJob);
        }

        private static void WmaToMp3(XConvertJob param)
        {
            var targetFilename = string.IsNullOrEmpty(param.AlternativeOutputPath) ? param.SourceFileName.GenerateOutPutPath(XFileType.Mp3) :
                param.AlternativeOutputPath;
            if (param.SourceData != null)
            {
                File.WriteAllBytes(param.SourceFileName, param.SourceData);
            }
            using (var reader = new NAudio.Wave.AudioFileReader(param.SourceFileName))
            using (var writer = new NAudio.Lame.LameMP3FileWriter(targetFilename, reader.WaveFormat, NAudio.Lame.LAMEPreset.STANDARD))
            {
                reader.CopyTo(writer);
            }
            param.ResulFileName = targetFilename;
        }

        private static void Mp3ToWav(XConvertJob param)
        {
            //using (var fs = new FileStream(param.AlternativeOutputPath, FileMode.Create))
            //{ }
            using (var reader = GetMp3Reader(param))
            {
                using (WaveStream pcmStream = WaveFormatConversionStream.CreatePcmStream(reader))
                {
                    WaveFileWriter.CreateWaveFile(param.AlternativeOutputPath, pcmStream);
                }
            }
        }

        private static Mp3FileReader GetMp3Reader(XConvertJob param)
        {
            if (param.SourceData != null)
            {
                var mStream = new MemoryStream(param.SourceData.Length);
                return new Mp3FileReader(mStream);
            }
            return new Mp3FileReader(param.SourceFileName);
        }

        private static void WavToMp3(XConvertJob param)
        {
            var savetofilename = string.IsNullOrEmpty(param.AlternativeOutputPath) ? param.SourceFileName.ReplaceExtension(XFileType.Mp3) :
                param.AlternativeOutputPath;
            using (var rdr = GetWavReader(param))
            using (var wtr = new LameMP3FileWriter(savetofilename, rdr.WaveFormat, LAMEPreset.VBR_90))
            {
                rdr.CopyTo(wtr);
            }
            param.ResulFileName = savetofilename;
        }

        private static WaveFileReader GetWavReader(XConvertJob param)
        {
            if (param.SourceData != null)
            {
                var mStream = new MemoryStream(param.SourceData.Length);
                return new WaveFileReader(mStream);
            }
            return new WaveFileReader(param.SourceFileName);
        }


        public void Dispose()
        {
            
        }
    }
}
