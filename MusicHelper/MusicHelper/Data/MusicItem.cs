using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HundredMilesSoftware.UltraID3Lib;
using System.IO;
using System.Data;

namespace MusicHelper
{
    public class MusicItem : DbMusicItem
    {

        public MusicItem()
        {

        }
        public MusicItem(IDataReader data)
        {
            Load(data);
        }

        //public MusicItem(UltraID3 u)
        //{
        //    Artist = u.Artist;
        //    Title = u.Title;
        //    FullFileName = u.FileName;
        //    MachineName = Environment.UserName;
        //    FileName = Path.GetFileName(FullFileName);
        //}

        public MusicItem(TagLib.File f, UltraID3 u)
        {
            Artist = u.Artist ?? f.Tag.AlbumArtists.FirstOrDefault();
            Title = f.Tag.Title ?? u.Title;
            FullFileName = u.FileName;
            MachineName = Environment.UserName;
            FileName = Path.GetFileName(FullFileName);
        }
    }
}
