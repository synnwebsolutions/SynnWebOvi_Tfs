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
            sqlItems.Add(new SqlItem("UserGroupId", p.UserGroupId));
            sqlItems.Add(new SqlItem("title", p.InsertItem.title));
            sqlItems.Add(new SqlItem("description", p.InsertItem.Description));
            sqlItems.Add(new SqlItem("date", p.InsertItem.Date));
            SetInsertIntoSql(SynnDataProvider.TableNames.DiaryData, sqlItems);
            ExecuteSql();
        }

        public List<MemoItem> Get(CalendarSearchParameters sp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.DiaryData);
            ClearParameters();
            AddSqlWhereField("UserGroupId", sp.UserGroupId);
     
            var lst = new List<MemoItem>();
            FillList(lst, typeof(MemoItem));
            return lst;
        }
    }
}