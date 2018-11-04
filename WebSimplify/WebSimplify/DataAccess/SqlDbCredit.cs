using SynnCore.DataAccess;
using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class SqlDbCredit : SqlDbController, IDbCredit
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

        private static SqlItemList Get(CreditCardMonthlyData p)
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
        private void SetPermissions(CreditSearchParameters sp)
        {
            if (!sp.CurrentUser.IsAdmin)
            {
                StartORGroup();
                foreach (int gid in sp.CurrentUser.AllowedSharedPermissions)
                    AddOREqualField("UserGroupId", gid);
                EndORGroup();
            }
        }
    }
}