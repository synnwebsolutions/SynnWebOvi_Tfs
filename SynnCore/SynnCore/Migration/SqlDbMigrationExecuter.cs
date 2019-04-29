using SynnCore.DataAccess;
using SynnCore.Migration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;

namespace SynnCore.Migration
{
    public class SqlDbMigrationExecuter : SqlDbController, IdbMigration
    {
        public SqlDbMigrationExecuter(SynnDataProviderBase d) : base(d)
        {
        }

        public SqlDbMigrationExecuter(string connectionstring, string tableName) : base(new SynnSqlDataProvider(connectionstring))
        {
            TableName = tableName;
        }

        public string TableName { get; private set; }

        public void ExecurteCreateTable(string tsql)
        {
            ClearParameters();
            ExecuteSql(tsql);
        }

        public void FinishMethod(string stepName)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("Name", stepName));
            sqlItems.Add(new SqlItem("Date", DateTime.Now));
            SetInsertIntoSql(TableName, sqlItems);
            ExecuteSql();
        }

        public List<string> GetAlreadyFinishedSteps()
        {
            SetSqlFormat("select Name from {0}", TableName);
            ClearParameters();

            var lst = new List<string>();
            using (IDataReader data = DoSelect())
            {
                while (data.Read())
                    lst.Add(data.GetString(0));
            }
            return lst;
        }
    }
}
