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

        public static string GetDirectory(this string str)
        {
            return Path.GetDirectoryName(str);
        }

        public static string GenerateFileTempoAlteredName(this string str)
        {
            var nameWithoutExtension = Path.GetFileNameWithoutExtension(str);
            var extension = Path.GetExtension(str);
            return str.Replace(nameWithoutExtension, $"{nameWithoutExtension}_tempo").Replace(extension,Mp3Extension);
        }

        public static DateTime Stamp(this DateTime d)
        {
            d = DateTime.Now;
            return d;
        }

        public static string GenerateGuidPath(this string path, XFileType dst, string extraText = null)
        {
            //Guid g = Guid.NewGuid();
            //string GuidString = System.Convert.ToBase64String(g.ToByteArray());
            //GuidString = GuidString.Replace("=", "");
            //GuidString = GuidString.Replace("+", "");

            string GuidString = (new Random()).Next(1000000).ToString();
            var dir = Path.GetDirectoryName(path);
            var file = $"{GuidString} [{extraText ?? string.Empty}]{dst.GetExtensionType()}";
            return Path.Combine(dir, file);
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

        public static string GenerateOutPutPath(this string str, string extraText = null)
        {
            var ext = Path.GetFileNameWithoutExtension(str);
            return str.Replace(ext, $"{ext}[{extraText ?? string.Empty}]_out");
        }

        public static string GetSpanString(this DateTime d, DateTime rel)
        {
            TimeSpan sp = d.GetSpan(rel);
            return string.Format("{0:c}",sp);
            //return string.Format("{0}:{1}:{2}",sp.Hours,sp.Minutes,sp.Seconds);
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

        public static string GenerateOutPutPath(this string str, XFileType destinationType, string extraText = null)
        {
            var name = Path.GetFileNameWithoutExtension(str);
            var extension = Path.GetExtension(str);
            str = str.Replace(extension, destinationType.GetExtensionType());
            return str.Replace(name, $"{name}[{extraText ?? string.Empty}]_out");
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
