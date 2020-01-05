using SynnCore.DataAccess;
using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class SqlDbMigration : SqlDbController, IDbMigration
    {
        public SqlDbMigration(string _connectionString) : base(new SynnSqlDataProvider(_connectionString))
        {
        }

        public bool CheckTableExistence(string tableName)
        {
            SetSqlFormat("SELECT CASE WHEN EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{0}') THEN 1  ELSE 0 END AS tableCheck", tableName);
            ClearParameters();

            var res = (int)GetSingleRecordFirstValue();
            return res == 1;
        }

        public void ClearDb(string clearDbScript)
        {
            SetSql(clearDbScript);
            ClearParameters();
            ExecuteSql();
        }

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
            SetInsertIntoSql(SynnDataProvider.TableNames.MigrationItems, sqlItems);
            ExecuteSql();
        }

        public List<string> GetAlreadyFinishedSteps()
        {
            SetSqlFormat("select Name from {0}", SynnDataProvider.TableNames.MigrationItems);
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