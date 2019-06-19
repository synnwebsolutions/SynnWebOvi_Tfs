using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HundredMilesSoftware.UltraID3Lib;
using System.IO;
using System.Data;

namespace XmusicCore
{
    [Serializable]
    public class MusicItem : DbMusicItem
    {

        public MusicItem()
        {

        }

        public MusicItem(string filePath)
        {
            var ii = TagLib.File.Create(filePath);
            UltraID3 u = new UltraID3();
            u.Read(filePath);
            Init(ii, u);
        }
        public MusicItem(IDataReader data)
        {
            Load(data);
        }

        public MusicItem(TagLib.File f, UltraID3 u)
        {
            Init(f, u);
        }

        private void Init(TagLib.File f, UltraID3 u)
        {
            Artist = u.Artist ?? f.Tag.AlbumArtists.FirstOrDefault();
            Title = f.Tag.Title ?? u.Title;
            FullFileName = u.FileName;
            MachineName = Environment.UserName;
            FileName = Path.GetFileName(FullFileName);
        }
    }
}
