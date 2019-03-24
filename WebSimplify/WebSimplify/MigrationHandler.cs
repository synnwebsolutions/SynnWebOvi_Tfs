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

        public static void DevTasksTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = "DevTasks"
            };
            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "Status", FieldType = TableMigrationFieldType.Integer, IsNullAble = false });
            t.Fields.Add(new TableMigrationField { FieldName = "Name", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 500, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "Description", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 5000, IsNullAble = true });

            db.ExecurteCreateTable(t.ToString());
        }


        public static void MoneyTransactionsTemplatesTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.MoneyTransactionTemplatess
            };
            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "TransactionType", FieldType = TableMigrationFieldType.Integer });
            t.Fields.Add(new TableMigrationField { FieldName = "Amount", FieldType = TableMigrationFieldType.Integer });
            t.Fields.Add(new TableMigrationField { FieldName = "Active", FieldType = TableMigrationFieldType.Bit});
            t.Fields.Add(new TableMigrationField { FieldName = "Auto", FieldType = TableMigrationFieldType.Bit});
            t.Fields.Add(new TableMigrationField { FieldName = "UserGroupId", FieldType = TableMigrationFieldType.Integer });
            t.Fields.Add(new TableMigrationField { FieldName = "Name", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 5000});

            t.Fields.Add(new TableMigrationField { FieldName = "FromDate", FieldType = TableMigrationFieldType.Date });
            t.Fields.Add(new TableMigrationField { FieldName = "ToDate", FieldType = TableMigrationFieldType.Date, IsNullAble = true });

            db.ExecurteCreateTable(t.ToString());
        }

        public static void MoneyTransactionItems(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.MoneyTransactionItems
            };
            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "Amount", FieldType = TableMigrationFieldType.Integer });
            t.Fields.Add(new TableMigrationField { FieldName = "TemplateId", FieldType = TableMigrationFieldType.Integer});
            t.Fields.Add(new TableMigrationField { FieldName = "UserGroupId", FieldType = TableMigrationFieldType.Integer});
            t.Fields.Add(new TableMigrationField { FieldName = "Month", FieldType = TableMigrationFieldType.Date});
            t.Fields.Add(new TableMigrationField { FieldName = "Closed", FieldType = TableMigrationFieldType.Bit });

            db.ExecurteCreateTable(t.ToString());
        }

        public static void ThemeItemsTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.ThemeItems
            };
            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "ElementIdentifier", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 8000, IsNullAble = false });
            t.Fields.Add(new TableMigrationField { FieldName = "CssAttribute", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 8000, IsNullAble = false });
            t.Fields.Add(new TableMigrationField { FieldName = "CssValue", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 8000, IsNullAble = false });

            db.ExecurteCreateTable(t.ToString());
        }

        public static void LottoRowsTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.LottoRows
            };
            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "N1", FieldType = TableMigrationFieldType.Integer, IsNullAble = false });
            t.Fields.Add(new TableMigrationField { FieldName = "N2", FieldType = TableMigrationFieldType.Integer, IsNullAble = false });
            t.Fields.Add(new TableMigrationField { FieldName = "N3", FieldType = TableMigrationFieldType.Integer, IsNullAble = false });
            t.Fields.Add(new TableMigrationField { FieldName = "N4", FieldType = TableMigrationFieldType.Integer, IsNullAble = false });
            t.Fields.Add(new TableMigrationField { FieldName = "N5", FieldType = TableMigrationFieldType.Integer, IsNullAble = false });
            t.Fields.Add(new TableMigrationField { FieldName = "N6", FieldType = TableMigrationFieldType.Integer, IsNullAble = false });
            t.Fields.Add(new TableMigrationField { FieldName = "S", FieldType = TableMigrationFieldType.Integer, IsNullAble = false });

            t.Fields.Add(new TableMigrationField { FieldName = "PoleKey", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 250, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "PoleDestinationDate", FieldType = TableMigrationFieldType.Date });
            t.Fields.Add(new TableMigrationField { FieldName = "CreationDate", FieldType = TableMigrationFieldType.Date });
            t.Fields.Add(new TableMigrationField { FieldName = "WinsData", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 8000, IsNullAble = true });

            db.ExecurteCreateTable(t.ToString());
        }

        public static void LottoPolesTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.LottoPoles
            };
            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "N1", FieldType = TableMigrationFieldType.Integer, IsNullAble = false });
            t.Fields.Add(new TableMigrationField { FieldName = "N2", FieldType = TableMigrationFieldType.Integer, IsNullAble = false });
            t.Fields.Add(new TableMigrationField { FieldName = "N3", FieldType = TableMigrationFieldType.Integer, IsNullAble = false });
            t.Fields.Add(new TableMigrationField { FieldName = "N4", FieldType = TableMigrationFieldType.Integer, IsNullAble = false });
            t.Fields.Add(new TableMigrationField { FieldName = "N5", FieldType = TableMigrationFieldType.Integer, IsNullAble = false });
            t.Fields.Add(new TableMigrationField { FieldName = "N6", FieldType = TableMigrationFieldType.Integer, IsNullAble = false });
            t.Fields.Add(new TableMigrationField { FieldName = "S", FieldType = TableMigrationFieldType.Integer, IsNullAble = false });

            t.Fields.Add(new TableMigrationField { FieldName = "PoleKey", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 250, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "PoleActionDate", FieldType = TableMigrationFieldType.Date });
            t.Fields.Add(new TableMigrationField { FieldName = "WinsData", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 8000, IsNullAble = true });

            db.ExecurteCreateTable(t.ToString());
        }

    }
}