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
        internal void Convert(XConvertJob param)
        {
            switch (param.SourceFileType)
            {
                case XFileType.Wav:
                    XWaveConverter.Convert(param);
                    break;
                case XFileType.Mp3:
                    XMp3Converter.Convert(param);
                    break;
                case XFileType.Wma:
                    XWmaConverter.Convert(param);
                    break;
                default:
                    break;
            }
            if (param.ReturnData)
            {
                param.OutputData = File.ReadAllBytes(param.ResulFileName);
                File.Delete(param.ResulFileName);
            }
        }
        public void Dispose()
        {
            
        }
    }
}
