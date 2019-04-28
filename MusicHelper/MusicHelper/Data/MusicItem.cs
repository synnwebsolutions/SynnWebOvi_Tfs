using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HundredMilesSoftware.UltraID3Lib;
using System.IO;

namespace MusicHelper
{
    public class MusicItem 
    {
        public MusicItem(UltraID3 u)
        {
            Artist = u.Artist;
            Title = u.Title;
            FullFileName = u.FileName;
            FileName = Path.GetFileName(FullFileName);
        }

        public MusicItem(TagLib.File f)
        {
            Artist = f.Tag.AlbumArtists.FirstOrDefault();
            Title = f.Tag.Title;
            FullFileName = f.Name;
            FileName = Path.GetFileName(FullFileName);
        }

        public int Id { get; set; }
        public string Artist { get;  set; }
        public string FullFileName { get;  set; }
        public string FileName { get; set; }
        public string Title { get;  set; }
    }
}
