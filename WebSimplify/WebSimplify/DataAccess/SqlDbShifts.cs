using SynnCore.DataAccess;
using SynnCore.Generics;
using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebSimplify.Data;

namespace WebSimplify
{
    public class SqlDbShifts : SqlDbController, IDbShifts
    {
        public SqlDbShifts(string _connectionString) : base(new SynnSqlDataProvider(_connectionString))
        {

        }

        private void SetPermissions(ShiftsSearchParameters sp)
        {
            if (!sp.CurrentUser.IsAdmin)
            {
                StartORGroup();
                foreach (int gid in sp.CurrentUser.AllowedSharedPermissions)
                    AddOREqualField("UserGroupId", gid);
                EndORGroup();
            }
        }

        public void Save(ShiftsSearchParameters sp)
        {
            var i = sp.ItemForAction;
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("Date",i.Date));
            sqlItems.Add(new SqlItem("UserGroupId", sp.CurrentUser.AllowedSharedPermissions[0]));
            sqlItems.Add(new SqlItem("OwnerId", sp.CurrentUser.Id));
            sqlItems.Add(new SqlItem("DaylyShift", (int)i.DaylyShift));
            SetInsertIntoSql(SynnDataProvider.TableNames.ShiftsData, sqlItems);
            ExecuteSql();
        }
        

        public List<ShiftDayData> GetShifts(ShiftsSearchParameters lsp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.ShiftsData);
            ClearParameters();
            SetPermissions(lsp);
            if (lsp.FromDate.HasValue)
                AddSqlWhereField("Date", lsp.FromDate, ">=");
            if (lsp.ToDate.HasValue)
                AddSqlWhereField("Date", lsp.ToDate, "<");
            var lst = new List<ShiftDayData>();
            FillList(lst, typeof(ShiftDayData));
            return lst;
        }

        public void Delete(int id)
        {
            SetSqlFormat("delete {0}", SynnDataProvider.TableNames.ShiftsData);
            ClearParameters();
            AddSqlWhereField("Id", id);
            ExecuteSql();
        }
    }
}