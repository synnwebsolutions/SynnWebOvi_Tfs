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
            //sqlItems.Add(new SqlItem("ToUsb", m.ToUsb));
            //sqlItems.Add(new SqlItem("ToPlaylist", m.ToPlaylist));
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
            if(p.InUsbList.HasValue)
                AddSqlText(string.Format("inner join {0} usb on usb.ItemId = {1}.Id", xmConsts.UserUsbList, xmConsts.MusicItems));

            if (p.InPlayList.HasValue)
                AddSqlText(string.Format("inner join {0} pls on pls.ItemId = {1}.Id", xmConsts.UserPlayList, xmConsts.MusicItems));

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
                AddSqlWhereField("usb.UserId", GlobalAppData.CurrentUser.Id);
            if (p.InPlayList.HasValue)
                AddSqlWhereField("pls.UserId",GlobalAppData.CurrentUser.Id);
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

        public void AddToUsbList(MusicItem m)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("UserId", GlobalAppData.CurrentUser.Id));
            sqlItems.Add(new SqlItem("ItemId", m.Id));
            SetInsertIntoSql(xmConsts.UserUsbList, sqlItems);
            ExecuteSql();
        }

        public void AddToPlayList(MusicItem m)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("UserId", GlobalAppData.CurrentUser.Id));
            sqlItems.Add(new SqlItem("ItemId", m.Id));
            SetInsertIntoSql(xmConsts.UserPlayList, sqlItems);
            ExecuteSql();
        }

        public void ClearUsbList()
        {
            SetSqlFormat("delete from {0}", xmConsts.UserUsbList);
            ClearParameters();
            AddSqlWhereField("UserId", GlobalAppData.CurrentUser.Id);
            ExecuteSql();
        }

        public bool ValidateUser(ref LoggedUser u)
        {
            if (!string.IsNullOrEmpty(u.UserName) && !string.IsNullOrEmpty(u.Password))
            {
                SetSqlFormat("select * from {0}", xmConsts.Users);
                ClearParameters();
                AddSqlWhereField("UserName", u.UserName);
                AddSqlWhereField("Password", u.Password);
                var lst = new List<LoggedUser>();
                FillList(lst, typeof(LoggedUser));
                if (lst.Count == 1)
                {
                    u = lst[0];
                    return true;
                }
            }
            return false;
        }

        public UserTheme GetUserTheme()
        {
            throw new NotImplementedException();
        }
    }
}
