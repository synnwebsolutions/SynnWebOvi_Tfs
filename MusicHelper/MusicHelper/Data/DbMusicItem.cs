using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelper
{
    [Serializable]
    public class DbMusicItem : IDbLoadable
    {
        public DbMusicItem()
        {

        }
        public DbMusicItem(IDataReader data)
        {
            Load(data);
        }

        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            Artist = DataAccessUtility.LoadNullable<string>(reader, "Artist");
            FullFileName = DataAccessUtility.LoadNullable<string>(reader, "FullFileName");
            FileName = DataAccessUtility.LoadNullable<string>(reader, "FileName");
            Title = DataAccessUtility.LoadNullable<string>(reader, "Title");
            MachineName = DataAccessUtility.LoadNullable<string>(reader, "MachineName");

            //ToUsb = DataAccessUtility.LoadNullable<bool?>(reader, "ToUsb");
            //ToPlaylist = DataAccessUtility.LoadNullable<bool?>(reader, "ToPlaylist");
        }

        public int Id { get; set; }
        [GridInfo(1, "Artist", "Artist", true, true)]
        public string Artist { get; set; }
        
        public string FullFileName { get; set; }
        [GridInfo(3, "FileName", "File", true, true)]
        public string FileName { get; set; }
        [GridInfo(2, "Title", "Title", true, true)]
        public string Title { get; set; }
        public string MachineName { get; set; }
        //public bool? ToUsb { get;  set; }
        //public bool? ToPlaylist { get;  set; }
    }
}
