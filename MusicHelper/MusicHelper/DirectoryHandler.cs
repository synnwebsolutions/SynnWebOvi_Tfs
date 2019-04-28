using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelper
{
    public class DirectoryHandler
    {
        internal static void Handle(IDatabaseProvider dbController, string selectedPath)
        {

            //var p = @"D:\SmachData\2Pac - Letter to my unborn child.mp3\2Pac - Letter to my unborn child.mp3";

            //var fi = TagLib.File.Create(p);
            //MusicItem ti = new MusicItem(fi);

            //UltraID3 u = new UltraID3();
            //u.Read(p);
            //MusicItem i = new MusicItem(u);

            var files = Directory.GetFiles(selectedPath, "*.mp3|*.wma", SearchOption.AllDirectories);
            foreach (var fi in files)
            {
                try
                {
                    var ii = TagLib.File.Create(fi);
                    MusicItem ti = new MusicItem(ii);
                    if(!dbController.Match(ti))
                    {
                        dbController.AddMusicItem(ti);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
