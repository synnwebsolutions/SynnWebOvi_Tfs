using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xmusic.Extensions
{
    public static class Extensions
    {
        public const string Mp3Extension = ".mp3";
        public const string WavExtension = ".wav";
        public const string WmaExtension = ".wma";
        public static string ReplaceExtension(this string str, XFileType destinatinFileType)
        {
            var ext = Path.GetExtension(str);
            return str.Replace(Path.GetExtension(str), destinatinFileType.GetExtensionType());
        }

        public static XFileType RetrieveExtension(this string str)
        {
            var ext = Path.GetExtension(str);
            if (ext == Mp3Extension)
                return XFileType.Mp3;

            if (ext == WmaExtension)
                return XFileType.Wma;

            return XFileType.Wav;
        }

        public static string GenerateOutPutPath(this string str)
        {
            var ext = Path.GetFileNameWithoutExtension(str);
            return str.Replace(ext, $"{ext}_out");
        }

        public static string GetSpanString(this DateTime d, DateTime rel)
        {
            TimeSpan sp = d.GetSpan(rel);
            return string.Format("{0}:{1}:{2}",sp.Hours,sp.Minutes,sp.Seconds);
        }

        public static TimeSpan GetSpan(this DateTime d, DateTime rel)
        {
            TimeSpan sp = new TimeSpan();
            if (rel > d)
            {
                sp = rel - d;
            }
            else
            {
                sp = d - rel;
            }
            return sp;
        }

        public static string GenerateOutPutPath(this string str, XFileType destinationType)
        {
            var name = Path.GetFileNameWithoutExtension(str);
            var extension = Path.GetExtension(str);
            str = str.Replace(extension, destinationType.GetExtensionType());
            return str.Replace(name, $"{name}_out");
        }

        public static string GetExtensionType(this XFileType f)
        {
            if (f == XFileType.Mp3)
                return Mp3Extension;
            else if (f == XFileType.Wma)
                return WmaExtension;

            return WavExtension;
        }
    }
}
