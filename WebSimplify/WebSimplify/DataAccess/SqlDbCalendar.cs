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

        public void Add(QuickTask p)
        {
            SqlItemList sqlItems = Get(p);
            SetInsertIntoSql(SynnDataProvider.TableNames.QuickTasks, sqlItems);
            ExecuteSql();
        }

        private static SqlItemList Get(QuickTask p)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("Name", p.Name));
            sqlItems.Add(new SqlItem("Description", p.Description));
            sqlItems.Add(new SqlItem("CreationDate", p.CreationDate));
            sqlItems.Add(new SqlItem("Active", p.Active));
            return sqlItems;
        }

        public void Add(MemoItem pInsertItem)
        {
            pInsertItem.CreationDate = DateTime.Now;
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("title", pInsertItem.title));
            sqlItems.Add(new SqlItem("description", pInsertItem.Description));
            sqlItems.Add(new SqlItem("CreationDate", pInsertItem.CreationDate));
            sqlItems.Add(new SqlItem("date", pInsertItem.Date));
            sqlItems.Add(new SqlItem("RepeatEvery", pInsertItem.RepeatEvery));
            sqlItems.Add(new SqlItem("Shared", pInsertItem.Shared));
            sqlItems.Add(new SqlItem("UserId", pInsertItem.UserId));
            SetInsertIntoSql(SynnDataProvider.TableNames.DiaryData, sqlItems);
            ExecuteSql();
        }

        public List<QuickTask> Get(QuickTasksSearchParameters lsp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.QuickTasks);
            ClearParameters();
            if (!lsp.FromWs)
            {
                if (lsp.Active.HasValue)
                    AddSqlWhereField("Active", lsp.Active);
                if (lsp.Id.HasValue)
                    AddSqlWhereField("Id", lsp.Id);
            }
            var lst = new List<QuickTask>();
            FillList(lst, typeof(QuickTask));
            return lst;
        }

        public List<MemoItem> Get(CalendarSearchParameters lsp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.DiaryData);
            ClearParameters();

            if (lsp.FromDate.HasValue)
                AddSqlWhereField("Date", lsp.FromDate, ">=");
            if (lsp.ToDate.HasValue)
                AddSqlWhereField("Date", lsp.ToDate, "<=");
            if (lsp.ID.HasValue)
                AddSqlWhereField("Id", lsp.ID);
            var lst = new List<MemoItem>();
            FillList(lst, typeof(MemoItem));
            return lst;
        }

        public void Update(QuickTask item)
        {
            SqlItemList sqlItems = Get(item);
            SetUpdateSql(SynnDataProvider.TableNames.QuickTasks, sqlItems, new SqlItemList { new SqlItem { FieldName = "Id", FieldValue = item.Id } });
            ExecuteSql();
        }
    }
}