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
            if(lsp.FromDate.HasValue)
                AddSqlWhereField("Date", lsp.FromDate, ">=");
            if (lsp.ToDate.HasValue)
                AddSqlWhereField("Date", lsp.ToDate, "<");

            if (lsp.Id.HasValue)
                AddSqlWhereField("Id",lsp.Id.Value);
            if (lsp.Active.HasValue)
                AddSqlWhereField("Active", lsp.Active.Value);
            var lst = new List<CreditCardMonthlyData>();
            FillList(lst, typeof(CreditCardMonthlyData));
            return lst;
        }
        private void SetPermissions(BaseSearchParameters sp, string tablePrefix = "")
        {
            if (!sp.CurrentUser.IsAdmin)
            {
                StartORGroup();
                foreach (int gid in sp.CurrentUser.AllowedSharedPermissions)
                    AddOREqualField(tablePrefix + "UserGroupId", gid);
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

        public List<MoneyTransactionTemplate> GetMoneyTransactionTemplates(MonthlyMoneyTransactionSearchParameters mp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.MoneyTransactionTemplatess);
            ClearParameters();
            SetPermissions(mp);
            if (mp.Id.HasValue)
                AddSqlWhereField("Id", mp.Id.Value);
            var lst = new List<MoneyTransactionTemplate>();
            FillList(lst, typeof(MoneyTransactionTemplate));
            return lst;
        }

        public void Add(MonthlyMoneyTransaction p)
        {
            SqlItemList sqlItems = GetFrom(p);
            SetInsertIntoSql(SynnDataProvider.TableNames.MoneyTransactionItems, sqlItems);
            ExecuteSql();
        }

        private static SqlItemList GetFrom(MonthlyMoneyTransaction p)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("UserGroupId", p.UserGroupId));
            sqlItems.Add(new SqlItem("Amount", p.Amount));
            sqlItems.Add(new SqlItem("Closed", p.Closed));
            sqlItems.Add(new SqlItem("Month", p.Month));
            sqlItems.Add(new SqlItem("TemplateId", p.TemplateId));
            return sqlItems;
        }

        public MoneyTransactionTemplate GetTransactionTemplate(int templateId)
        {
            return GetMoneyTransactionTemplates(new MonthlyMoneyTransactionSearchParameters { Id = templateId }).FirstOrDefault();
        }

        public List<MonthlyMoneyTransaction> GetMoneyTransactions(MonthlyMoneyTransactionSearchParameters mp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.MoneyTransactionItems);
            if(mp.TranType.HasValue)
                AddSqlText(string.Format("inner join {0} tmp on tmp.Id = TemplateId", SynnDataProvider.TableNames.MoneyTransactionTemplatess));
            ClearParameters();
            SetPermissions(mp, string.Format("{0}.", SynnDataProvider.TableNames.MoneyTransactionItems));
            if (mp.Id.HasValue)
                AddSqlWhereField("Id", mp.Id.Value);
            if (mp.Closed.HasValue)
                AddSqlWhereField("Closed", mp.Closed.Value);
            if (mp.TranType.HasValue)
                AddSqlWhereField("tmp.TransactionType", (int)mp.TranType.Value);
            if (mp.Month.HasValue)
            {
                var d = mp.Month.Value;
                AddSqlWhereField("Month", new DateTime(d.Year, d.Month, 1), ">=");
                AddSqlWhereField("Month", new DateTime(d.Year, d.Month, d.NumberOfDays()), "<");
            }
            if (mp.TemplateId.HasValue)
                AddSqlWhereField("TemplateId", mp.TemplateId.Value);
            var lst = new List<MonthlyMoneyTransaction>();
            FillList(lst, typeof(MonthlyMoneyTransaction));
            return lst;
        }

        public MonthlyMoneyTransaction GetTransaction(MonthlyMoneyTransactionSearchParameters mp)
        {
            return GetMoneyTransactions(mp).FirstOrDefault();
        }

        public void Update(MoneyTransactionTemplate tmpl)
        {
            SqlItemList sqlItems = Get(tmpl);
            SetUpdateSql(SynnDataProvider.TableNames.MoneyTransactionTemplatess, sqlItems, new SqlItemList { new SqlItem { FieldName = "Id", FieldValue = tmpl.Id } });
            ExecuteSql();
        }

        private SqlItemList Get(MoneyTransactionTemplate p)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("UserGroupId", p.UserGroupId));
            sqlItems.Add(new SqlItem("Amount", p.Amount));
            sqlItems.Add(new SqlItem("Active", p.Active));
            sqlItems.Add(new SqlItem("Auto", p.Auto));
            sqlItems.Add(new SqlItem("FromDate", p.FromDate));
            sqlItems.Add(new SqlItem("ToDate", p.ToDate));
            sqlItems.Add(new SqlItem("Name", p.Name));
            sqlItems.Add(new SqlItem("TransactionType", p.TransactionType));
            return sqlItems;
        }

        public void Add(MoneyTransactionTemplate i)
        {
            SqlItemList sqlItems = Get(i);
            SetInsertIntoSql(SynnDataProvider.TableNames.MoneyTransactionTemplatess, sqlItems);
            ExecuteSql();
        }

        public void Update(MonthlyMoneyTransaction i)
        {
            SqlItemList sqlItems = GetFrom(i);
            SetUpdateSql(SynnDataProvider.TableNames.MoneyTransactionItems, sqlItems, new SqlItemList { new SqlItem { FieldName = "Id", FieldValue = i.Id } });
            ExecuteSql();
        }
    }
}