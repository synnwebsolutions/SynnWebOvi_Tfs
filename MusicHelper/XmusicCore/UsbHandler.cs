using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmusicCore
{
    public static class UsbHandler
    {

        public static void Sync(DriveInfo currentDrive, List<MusicItem> m)
        {
            string destDir = Path.Combine(currentDrive.Name,  (new Random()).Next(0, 15214).ToString());
            Directory.CreateDirectory(destDir);
            var files = m.Select(x => new FileInfo(x.FullFileName)).ToList();
            var usbFiles = Directory.GetFiles(currentDrive.Name).Select(x => new FileInfo(x)).ToList();
            foreach (var file in files)
                if (File.Exists(file.FullName) && !usbFiles.Any(x => x.Name == file.Name))
                    File.Copy(file.FullName, Path.Combine(destDir, file.Name));
        }
    }
}
