using System;
using System.Collections.Generic;
using SynnCore.DataAccess;
using WebSimplify;

namespace SynnWebOvi
{
    internal class SqlDbCalendar : SqlDbController, IDbCalendar
    {

        public SqlDbCalendar(string _connectionString) : base(new SynnSqlDataProvider(_connectionString))
        {
        }

        public void Add(CalendarSearchParameters p)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("UserGroupId", p.CurrentUser.AllowedSharedPermissions[0].ToString()));
            sqlItems.Add(new SqlItem("title", p.InsertItem.title));
            sqlItems.Add(new SqlItem("description", p.InsertItem.Description));
            sqlItems.Add(new SqlItem("date", p.InsertItem.Date));
            SetInsertIntoSql(SynnDataProvider.TableNames.DiaryData, sqlItems);
            ExecuteSql();
        }

        public List<MemoItem> Get(CalendarSearchParameters lsp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.DiaryData);
            ClearParameters();

            if (!lsp.CurrentUser.IsAdmin)
            {
                StartORGroup();
                foreach (int gid in lsp.CurrentUser.AllowedSharedPermissions)
                    AddOREqualField("UserGroupId", gid);
                EndORGroup();
            }

            if (lsp.FromDate.HasValue)
                AddSqlWhereField("Date", lsp.FromDate, ">=");
            if (lsp.ToDate.HasValue)
                AddSqlWhereField("Date", lsp.ToDate, "<");
            var lst = new List<MemoItem>();
            FillList(lst, typeof(MemoItem));
            return lst;
        }
    }
}