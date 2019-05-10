using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelper
{
    public class SqlDatabaseProvider : SqlDbController, IDatabaseProvider
    {
       
        public SqlDatabaseProvider(SynnDataProviderBase d) : base(d)
        {
        }

        public void AddMusicItem(MusicItem m)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("Artist", m.Artist));
            sqlItems.Add(new SqlItem("FileName", m.FileName));
            sqlItems.Add(new SqlItem("FullFileName", m.FullFileName));
            sqlItems.Add(new SqlItem("MachineName", m.MachineName));
            sqlItems.Add(new SqlItem("Title", m.Title));
            SetInsertIntoSql(xmConsts.MusicItems, sqlItems);
            ExecuteSql();
        }

        public bool Match(MusicItem m)
        {
            return false;
        }
    }
}
