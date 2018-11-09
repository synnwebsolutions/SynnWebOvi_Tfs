using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebSimplify;

namespace SynnWebOvi
{
    internal class MigrationHandler
    {
        private static IDatabaseProvider _DBr = SynnDataProvider.DbProvider;

        internal static void Perform()
        {
            string stepName = null;
            try
            {
                var methods = typeof(MigrationItems).GetMethods(BindingFlags.Static | BindingFlags.Public);
                var finishedSteps = _DBr.DbMigration.GetAlreadyFinishedSteps();
                var steps = methods.Where(x => !finishedSteps.Contains(x.Name)).ToList();
                var dbaction = _DBr.DbMigration;
                foreach (var m in steps)
                {
                    stepName = m.Name;
                    m.Invoke(null, new object[] { dbaction });
                    dbaction.FinishMethod(stepName);
                }
            }
            catch (Exception ex)
            {
                string rd = ex.StackTrace;
            }
        }
    }

    public class MigrationItems
    {
        public static void T3(IDbMigration db)
        {
            //var t = new TableMigration
            //{
            //    HasIdentity = true,
            //    TableName = "testmigration3"
            //};
            //t.Fields = new List<TableMigrationField>();
            //t.Fields.Add(new TableMigrationField { FieldName = "col1", FieldType = TableMigrationFieldType.Integer, IsNullAble = false });
            //t.Fields.Add(new TableMigrationField { FieldName = "col2", FieldType = TableMigrationFieldType.Decimal, IsNullAble = true });
            //t.Fields.Add(new TableMigrationField { FieldName = "col3", FieldType = TableMigrationFieldType.Date, IsNullAble = true });
            //t.Fields.Add(new TableMigrationField { FieldName = "col4", FieldType = TableMigrationFieldType.Text, IsNullAble = true });
            //t.Fields.Add(new TableMigrationField { FieldName = "col5", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 500, IsNullAble = true });

            //db.ExecurteCreateTable(t.ToString());
        }

    }
}