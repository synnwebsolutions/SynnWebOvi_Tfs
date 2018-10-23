using SynnCore.DataAccess;
using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSimplify.Data;

namespace WebSimplify
{
    public class SqlDbLog : SqlDbController, IDbLog
    {
        public SqlDbLog(string _connectionString) : base(new SynnSqlDataProvider(_connectionString))
        {
        }

        public void AddLog(string message)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("Date", DateTime.Now));
            sqlItems.Add(new SqlItem("Trace", string.Empty));
            sqlItems.Add(new SqlItem("Message", message));
            SetInsertIntoSql(SynnDataProvider.TableNames.Log, sqlItems);
            ExecuteSql();
        }

        public string AddLog(Exception l)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("Date", DateTime.Now));
            sqlItems.Add(new SqlItem("Trace", l.StackTrace));
            sqlItems.Add(new SqlItem("Message", l.Message));
            SetInsertIntoSql(SynnDataProvider.TableNames.Log, sqlItems);
            ExecuteSql();
            return "GetMsSqlLastIdentityValue()".ToString();
        }

        public void AddThemeLog(ThemeLog l)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("Date", DateTime.Now));
            sqlItems.Add(new SqlItem("DestinationPath", l.DestinationPath));
            sqlItems.Add(new SqlItem("NewText", l.NewText));
            sqlItems.Add(new SqlItem("PrevText", l.PrevText));
            sqlItems.Add(new SqlItem("XuiFile", (int)l.XiFile));
            SetInsertIntoSql(SynnDataProvider.TableNames.ThemeLog, sqlItems);
            ExecuteSql();
        }

        public ThemeLog GetLastItem()
        {
            return GetThemeLogs().OrderByDescending(x => x.Id).First();
        }

        public List<ThemeLog> GetThemeLogs()
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.ThemeLog);
            ClearParameters();
            var lst = new List<ThemeLog>();
            FillList(lst, typeof(ThemeLog));
            return lst;
        }

        public List<LogItem> GetLogs(LogSearchParameters lsp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.Log);
            ClearParameters();
            if (!string.IsNullOrEmpty(lsp.Text))
            {
                StartORGroup();
                AddORLikeField("Trace", lsp.Text, LikeSelectionStyle.CheckBoth);
                AddORLikeField("Message", lsp.Text, LikeSelectionStyle.CheckBoth);
                EndORGroup();
            }
            if (lsp.FromDate.HasValue)
                AddSqlWhereField("Date", lsp.FromDate, ">=");
            if (lsp.ToDate.HasValue)
                AddSqlWhereField("Date", lsp.ToDate, "<");
            var lst = new List<LogItem>();
            FillList(lst, typeof(LogItem));
            return lst;
        }
    }
  
}