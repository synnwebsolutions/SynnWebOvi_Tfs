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
            var sqlItems = Get(m);
            SetInsertIntoSql(xmConsts.MusicItems, sqlItems);
            ExecuteSql();
        }

        private static SqlItemList Get(MusicItem m)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("Artist", m.Artist));
            sqlItems.Add(new SqlItem("FileName", m.FileName));
            sqlItems.Add(new SqlItem("FullFileName", m.FullFileName));
            sqlItems.Add(new SqlItem("MachineName", m.MachineName));
            sqlItems.Add(new SqlItem("Title", m.Title));
            sqlItems.Add(new SqlItem("ToUsb", m.ToUsb));
            sqlItems.Add(new SqlItem("ToPlaylist", m.ToPlaylist));
            return sqlItems;
        }

        public void ClearData(MusicSearchParameters musicSearchParameters)
        {
            SetSqlFormat("delete from {0}", xmConsts.MusicItems);
            ExecuteSql();
        }

        public List<MusicItem> GetMusicItems(MusicSearchParameters p)
        {
            SetSqlFormat("select * from {0}", xmConsts.MusicItems);
            ClearParameters();
            if (!string.IsNullOrEmpty(p.SearchText))
            {
                StartORGroup();
                AddORLikeField("Artist", p.SearchText, LikeSelectionStyle.CheckBoth);
                AddORLikeField("FileName", p.SearchText, LikeSelectionStyle.CheckBoth);
                AddORLikeField("FullFileName", p.SearchText, LikeSelectionStyle.CheckBoth);
                AddORLikeField("Title", p.SearchText, LikeSelectionStyle.CheckBoth);
                EndORGroup();
            }
            if (p.InUsbList.HasValue)
                AddSqlWhereField("ToUsb", p.InUsbList.Value);
            if (p.InPlayList.HasValue)
                AddSqlWhereField("ToPlaylist", p.InPlayList.Value);
            var lst = new List<MusicItem>();
            FillList(lst, typeof(MusicItem));
            return lst;
        }

        public bool Match(MusicItem m)
        {
            return false;
        }

        public void Update(MusicItem m)
        {
            var sqlItems = Get(m);
            SetUpdateSql(xmConsts.MusicItems, sqlItems, new SqlItemList { new SqlItem { FieldName = "Id", FieldValue = m.Id } });
            ExecuteSql();
        }
    }
}
