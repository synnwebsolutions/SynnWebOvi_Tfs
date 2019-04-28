using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelper
{
    public class MusicItem /*: TagLib.File*/
    {
        public MusicItem(TagLib.File f)
        {
            this.Artist = f.Tag.AlbumArtists.FirstOrDefault();
            this.Title = f.Tag.Title;
            this.FullFileName = f.Name;
        }

        public string Artist { get;  set; }
        public string FullFileName { get;  set; }
        public string Title { get;  set; }
    }
}
