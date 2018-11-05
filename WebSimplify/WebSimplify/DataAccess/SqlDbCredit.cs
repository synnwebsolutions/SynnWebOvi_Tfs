using SynnCore.DataAccess;
using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSimplify.Data;

namespace WebSimplify
{
    public class SqlDbCredit : SqlDbController, IDbMoney
    {
        public SqlDbCredit(string _connectionString) : base(new SynnSqlDataProvider(_connectionString))
        {
        }

        public void Add(CreditCardMonthlyData i)
        {
            SqlItemList sqlItems = Get(i);
            SetInsertIntoSql(SynnDataProvider.TableNames.CreditData, sqlItems);
            ExecuteSql();
        }

        public void Update(CreditCardMonthlyData i)
        {
            SqlItemList sqlItems = Get(i);
            SetUpdateSql(SynnDataProvider.TableNames.CreditData, sqlItems, new SqlItemList { new SqlItem { FieldName = "Id", FieldValue = i.Id } });
            ExecuteSql();
        }

        private static SqlItemList Get(MoneyMonthlyData p)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("UserGroupId", p.UserGroupId));
            sqlItems.Add(new SqlItem("Date", p.Date));
            sqlItems.Add(new SqlItem("TotalSpent", p.TotalSpent));
            sqlItems.Add(new SqlItem("Active", p.Active));
            return sqlItems;
        }

        public List<CreditCardMonthlyData> Get(CreditSearchParameters lsp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.CreditData);
            ClearParameters();
            SetPermissions(lsp);
            if (lsp.Month.HasValue)
            {
                var d = lsp.Month.Value;
                AddSqlWhereField("Date",new DateTime(d.Year,d.Month,1), ">=");
                AddSqlWhereField("Date", new DateTime(d.Year, d.Month, d.NumberOfDays()), "<");
            }
            if(lsp.Id.HasValue)
                AddSqlWhereField("Id",lsp.Id.Value);

            var lst = new List<CreditCardMonthlyData>();
            FillList(lst, typeof(CreditCardMonthlyData));
            return lst;
        }
        private void SetPermissions(BaseSearchParameters sp)
        {
            if (!sp.CurrentUser.IsAdmin)
            {
                StartORGroup();
                foreach (int gid in sp.CurrentUser.AllowedSharedPermissions)
                    AddOREqualField("UserGroupId", gid);
                EndORGroup();
            }
        }

        public void Add(CashMonthlyData i)
        {
            SqlItemList sqlItems = Get(i);
            SetInsertIntoSql(SynnDataProvider.TableNames.CashData, sqlItems);
            ExecuteSql();
        }

        public List<CashMonthlyData> Get(CashSearchParameters lsp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.CashData);
            ClearParameters();
            SetPermissions(lsp);
            if (lsp.Month.HasValue)
            {
                var d = lsp.Month.Value;
                AddSqlWhereField("Date", new DateTime(d.Year, d.Month, 1), ">=");
                AddSqlWhereField("Date", new DateTime(d.Year, d.Month, d.NumberOfDays()), "<");
            }
            if (lsp.Id.HasValue)
                AddSqlWhereField("Id", lsp.Id.Value);

            var lst = new List<CashMonthlyData>();
            FillList(lst, typeof(CashMonthlyData));
            return lst;
        }

        public void Update(CashMonthlyData i)
        {
            SqlItemList sqlItems = Get(i);
            SetUpdateSql(SynnDataProvider.TableNames.CashData, sqlItems, new SqlItemList { new SqlItem { FieldName = "Id", FieldValue = i.Id } });
            ExecuteSql();
        }

        public List<CashMoneyItem> GetCashItems(CashSearchParameters lsp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.CashItems);
            ClearParameters();
            SetPermissions(lsp);
            if (lsp.Month.HasValue)
            {
                var d = lsp.Month.Value;
                AddSqlWhereField("Date", new DateTime(d.Year, d.Month, 1), ">=");
                AddSqlWhereField("Date", new DateTime(d.Year, d.Month, d.NumberOfDays()), "<");
            }
            if (lsp.Id.HasValue)
                AddSqlWhereField("Id", lsp.Id.Value);

            var lst = new List<CashMoneyItem>();
            FillList(lst, typeof(CashMoneyItem));
            return lst;
        }

        public void Add(CashMoneyItem p)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("UserGroupId", p.UserGroupId));
            sqlItems.Add(new SqlItem("Date", p.Date));
            sqlItems.Add(new SqlItem("TotalSpent", p.TotalSpent));
            sqlItems.Add(new SqlItem("Description", p.Description));
            SetInsertIntoSql(SynnDataProvider.TableNames.CashItems, sqlItems);
            ExecuteSql();
        }
    }
}