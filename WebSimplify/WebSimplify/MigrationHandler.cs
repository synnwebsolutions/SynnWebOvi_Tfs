using System;
using System.Collections.Generic;
using System.IO;
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
                CheckDbRecreation();
                CheckSAndPerformFirstInit();
                var finishedSteps = _DBr.DbMigration.GetAlreadyFinishedSteps();
                var steps = methods.Where(x => !finishedSteps.Contains(x.Name)).ToList();
                var dbaction = _DBr.DbMigration;
                foreach (var m in steps)
                {
                    stepName = m.Name;
                    m.Invoke(null, new object[] { dbaction });
                    dbaction.FinishMethod(stepName);
                }
                //PerformFirstInserts(_DBr);
            }
            catch (Exception ex)
            {
                string rd = ex.StackTrace;
            }
        }

        private static void CheckDbRecreation()
        {
            if (Global.ClearDb)
            {
                var clearDbScript = @"DECLARE @Sql NVARCHAR(500) DECLARE @Cursor CURSOR

                                        SET @Cursor = CURSOR FAST_FORWARD FOR
                                        SELECT DISTINCT sql = 'ALTER TABLE [' + tc2.TABLE_SCHEMA + '].[' +  tc2.TABLE_NAME + '] DROP [' + rc1.CONSTRAINT_NAME + '];'
                                        FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc1
                                        LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc2 ON tc2.CONSTRAINT_NAME =rc1.CONSTRAINT_NAME
                                        OPEN @Cursor FETCH NEXT FROM @Cursor INTO @Sql
                                        WHILE (@@FETCH_STATUS = 0)
                                        BEGIN
                                        Exec sp_executesql @Sql
                                        FETCH NEXT FROM @Cursor INTO @Sql
                                        END
                                        CLOSE @Cursor DEALLOCATE @Cursor
                                        GO
                                        EXEC sp_MSforeachtable 'DROP TABLE ?'
                                        GO";

                _DBr.DbMigration.ClearDb(clearDbScript);
            }
        }

        private static void CheckSAndPerformFirstInit()
        {
            var methods = typeof(MigrationInitItems).GetMethods(BindingFlags.Static | BindingFlags.Public);
            foreach (var m in methods)
            {
                if (!_DBr.DbMigration.CheckTableExistence(m.Name.Replace("Table", string.Empty)))
                    m.Invoke(null, new object[] { _DBr.DbMigration });
            }

            InitGenericTables();
        }

        private static void InitGenericTables()
        {
            var subclassTypes = Assembly.GetAssembly(typeof(GenericData)).GetTypes().Where(t => t.IsSubclassOf(typeof(GenericData)));
            var names = subclassTypes.Select(x => x.Name).ToList();
            foreach (var genericItem in subclassTypes)
            {
                if (!_DBr.DbMigration.CheckTableExistence(genericItem.Name))
                {
                    var t = new TableMigration
                    {
                        HasIdentity = true,
                        TableName = genericItem.Name
                    };
                    t.Fields = new List<TableMigrationField>();

                    t.Fields.Add(new TableMigrationField { FieldName = "Description", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 500, IsNullAble = true });
                    t.Fields.Add(new TableMigrationField { FieldName = "CreationDate", FieldType = TableMigrationFieldType.Date });
                    t.Fields.Add(new TableMigrationField { FieldName = "UpdateDate", FieldType = TableMigrationFieldType.Date });
                    t.Fields.Add(new TableMigrationField { FieldName = "Active", FieldType = TableMigrationFieldType.Bit });

                    //var attribute = (GenericDataFieldAttribute[])genericItem.GetType().GetCustomAttributes(typeof(GenericDataFieldAttribute), false);
                    var attribute = genericItem.GetAttributes<GenericDataFieldAttribute>();
                    if (attribute.NotEmpty())
                    {
                        foreach (var gField in attribute)
                        {
                            t.Fields.Add(new TableMigrationField { FieldName = gField.FieldName,
                                FieldType = TableMigrationFieldType.NVarchar,
                                FieldLLenght = 500,
                                IsNullAble = true });
                        }
                    }

                    _DBr.DbMigration.ExecurteCreateTable(t.ToString());
                }
            }
        }
    }
}