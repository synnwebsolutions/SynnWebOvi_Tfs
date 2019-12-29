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

        public static void DiaryTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.DiaryData
            };

            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "title", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 8000, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "description", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 8000, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "date", FieldType = TableMigrationFieldType.Date });

            db.ExecurteCreateTable(t.ToString());
        }

        public static void ShiftsDataTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.ShiftsData
            };

            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "OwnerId", FieldType = TableMigrationFieldType.Integer });
            t.Fields.Add(new TableMigrationField { FieldName = "DaylyShift", FieldType = TableMigrationFieldType.Integer });
            t.Fields.Add(new TableMigrationField { FieldName = "Date", FieldType = TableMigrationFieldType.Date });

            db.ExecurteCreateTable(t.ToString());
        }

        public static void SQuickTasksDataTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.QuickTasks
            };


            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "Name", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 8000, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "Description", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 8000, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "Active", FieldType = TableMigrationFieldType.Bit });
            t.Fields.Add(new TableMigrationField { FieldName = "CreationDate", FieldType = TableMigrationFieldType.Date });

            db.ExecurteCreateTable(t.ToString());
        }

        public static void UserDictionaryTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.UserDictionary
            };


            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "dKey", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 8000, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "Value", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 8000, IsNullAble = true });

            db.ExecurteCreateTable(t.ToString());
        }

        public static void CreditDataTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.CreditData
            };


            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "Date", FieldType = TableMigrationFieldType.Date });
            t.Fields.Add(new TableMigrationField { FieldName = "Active", FieldType = TableMigrationFieldType.Bit });
            t.Fields.Add(new TableMigrationField { FieldName = "TotalSpent", FieldType = TableMigrationFieldType.Integer });


            db.ExecurteCreateTable(t.ToString());
        }

        public static void CashDataTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.CashData
            };

            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "Date", FieldType = TableMigrationFieldType.Date });
            t.Fields.Add(new TableMigrationField { FieldName = "Active", FieldType = TableMigrationFieldType.Bit });
            t.Fields.Add(new TableMigrationField { FieldName = "TotalSpent", FieldType = TableMigrationFieldType.Integer });


            db.ExecurteCreateTable(t.ToString());
        }

        public static void CashItemsTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.CashItems
            };


            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "Date", FieldType = TableMigrationFieldType.Date });
            t.Fields.Add(new TableMigrationField { FieldName = "Description", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 8000, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "TotalSpent", FieldType = TableMigrationFieldType.Integer });


            db.ExecurteCreateTable(t.ToString());
        }

        public static void WeddingItemsTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.WeddingItems
            };


            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "GuestName", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 8000, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "Payment", FieldType = TableMigrationFieldType.Integer });

            db.ExecurteCreateTable(t.ToString());
        }

        public static void ShoppingItemsTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.ShoppingItems
            };


            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "Name", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 8000, IsNullAble = true });

            db.ExecurteCreateTable(t.ToString());
        }

        public static void User_ShoppingItemsTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.User_ShoppingItems
            };


            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "Name", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 8000, IsNullAble = true });

            db.ExecurteCreateTable(t.ToString());
        }

        public static void UsersTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.Users
            };

            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "UserName", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 8000 });
            t.Fields.Add(new TableMigrationField { FieldName = "DisplayName", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 8000 });
            t.Fields.Add(new TableMigrationField { FieldName = "Password", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 12 });

            db.ExecurteCreateTable(t.ToString());
        }


        public static void PermissionGroupsTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.PermissionGroups
            };

            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "Name", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 8000 });

            db.ExecurteCreateTable(t.ToString());
        }

        public static void LogsTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.Log
            };


            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "Message", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 8000 });
            t.Fields.Add(new TableMigrationField { FieldName = "Trace", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 8000 });
            t.Fields.Add(new TableMigrationField { FieldName = "Date", FieldType = TableMigrationFieldType.Date });

            db.ExecurteCreateTable(t.ToString());
        }
    }
}