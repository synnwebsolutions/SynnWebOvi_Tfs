using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelper
{
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
        }

        public int Id { get; set; }
        public string Artist { get; set; }
        public string FullFileName { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public string MachineName { get; set; }
    }
}
