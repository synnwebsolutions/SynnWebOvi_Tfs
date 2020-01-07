using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class MigrationItems
    {
        public static void GoogleTokensTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                TableName = SynnDataProvider.TableNames.GoogleTokens
            };

            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "appuserid", FieldType = TableMigrationFieldType.Integer });
            t.Fields.Add(new TableMigrationField { FieldName = "userid", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000 });
            t.Fields.Add(new TableMigrationField { FieldName = "Credentials", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000 });

            db.ExecurteCreateTable(t.ToString());
        }

        public static void GoogleAPICredentialsTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                TableName = SynnDataProvider.TableNames.GoogleAPICredentials
            };

            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "userid", FieldType = TableMigrationFieldType.Integer });
            t.Fields.Add(new TableMigrationField { FieldName = "Credentials", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000 });

            db.ExecurteCreateTable(t.ToString());
        }

        public static void DevTasksTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.DevTasks
            };
            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "Status", FieldType = TableMigrationFieldType.Integer, IsNullAble = false });
            t.Fields.Add(new TableMigrationField { FieldName = "Name", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 500, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "Description", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000, IsNullAble = true });

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
            t.Fields.Add(new TableMigrationField { FieldName = "Active", FieldType = TableMigrationFieldType.Bit });
            t.Fields.Add(new TableMigrationField { FieldName = "Auto", FieldType = TableMigrationFieldType.Bit });
            t.Fields.Add(new TableMigrationField { FieldName = "Name", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000 });

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
            t.Fields.Add(new TableMigrationField { FieldName = "TemplateId", FieldType = TableMigrationFieldType.Integer });
            t.Fields.Add(new TableMigrationField { FieldName = "Month", FieldType = TableMigrationFieldType.Date });
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
            t.Fields.Add(new TableMigrationField { FieldName = "ElementIdentifier", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000, IsNullAble = false });
            t.Fields.Add(new TableMigrationField { FieldName = "CssAttribute", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000, IsNullAble = false });
            t.Fields.Add(new TableMigrationField { FieldName = "CssValue", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000, IsNullAble = false });

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

            t.Fields.Add(new TableMigrationField { FieldName = "PoleKey", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 250, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "PoleDestinationDate", FieldType = TableMigrationFieldType.Date });
            t.Fields.Add(new TableMigrationField { FieldName = "CreationDate", FieldType = TableMigrationFieldType.Date });
            t.Fields.Add(new TableMigrationField { FieldName = "WinsData", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000, IsNullAble = true });

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

            t.Fields.Add(new TableMigrationField { FieldName = "PoleKey", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 250, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "PoleActionDate", FieldType = TableMigrationFieldType.Date });
            t.Fields.Add(new TableMigrationField { FieldName = "WinsData", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000, IsNullAble = true });

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
            t.Fields.Add(new TableMigrationField { FieldName = "title", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "description", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "date", FieldType = TableMigrationFieldType.Date });
            t.Fields.Add(new TableMigrationField { FieldName = "CreationDate", FieldType = TableMigrationFieldType.Date });
            t.Fields.Add(new TableMigrationField { FieldName = "Shared", FieldType = TableMigrationFieldType.Bit });
            t.Fields.Add(new TableMigrationField { FieldName = "RepeatEvery", FieldType = TableMigrationFieldType.Integer, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "UserId", FieldType = TableMigrationFieldType.Integer });

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
            t.Fields.Add(new TableMigrationField { FieldName = "Name", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "Description", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000, IsNullAble = true });
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
            t.Fields.Add(new TableMigrationField { FieldName = "dKey", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "Value", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000, IsNullAble = true });

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
            t.Fields.Add(new TableMigrationField { FieldName = "Description", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000, IsNullAble = true });
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
            t.Fields.Add(new TableMigrationField { FieldName = "GuestName", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000, IsNullAble = true });
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
            t.Fields.Add(new TableMigrationField { FieldName = "Name", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "LastBought", FieldType = TableMigrationFieldType.Date, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "Active", FieldType = TableMigrationFieldType.Bit });
            t.Fields.Add(new TableMigrationField { FieldName = "CategoryId", FieldType = TableMigrationFieldType.Bit, IsNullAble = true });

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
            t.Fields.Add(new TableMigrationField { FieldName = "Name", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000 });

            db.ExecurteCreateTable(t.ToString());
        }

        public static void UserPreferencesTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.UserPreferences
            };

            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "pdata", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000 });
            t.Fields.Add(new TableMigrationField { FieldName = "UserId", FieldType = TableMigrationFieldType.Integer });

            db.ExecurteCreateTable(t.ToString());
        }

        public static void WorkHoursDataTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.WorkHoursData
            };

            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "Month", FieldType = TableMigrationFieldType.Date });
            t.Fields.Add(new TableMigrationField { FieldName = "Active", FieldType = TableMigrationFieldType.Bit });
            t.Fields.Add(new TableMigrationField { FieldName = "CurrentMonthTotal", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000 });
            t.Fields.Add(new TableMigrationField { FieldName = "CurrentShiftStart", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000 });
            t.Fields.Add(new TableMigrationField { FieldName = "CurrentShiftEnd", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000 });
            db.ExecurteCreateTable(t.ToString());
        }

        //public static void GenericDataTable(IDbMigration db)
        //{
        //    var t = new TableMigration
        //    {
        //        HasIdentity = true,
        //        TableName = SynnDataProvider.TableNames.GenericData
        //    };
        //    t.Fields = new List<TableMigrationField>();
        //    t.Fields.Add(new TableMigrationField { FieldName = "GenericDataEnum", FieldType = TableMigrationFieldType.Integer });
        //    t.Fields.Add(new TableMigrationField { FieldName = "GenericDataName", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000, IsNullAble = true });
        //    t.Fields.Add(new TableMigrationField { FieldName = "CreationDate", FieldType = TableMigrationFieldType.Date });
        //    t.Fields.Add(new TableMigrationField { FieldName = "UpdateDate", FieldType = TableMigrationFieldType.Date, IsNullAble = true });
        //    t.Fields.Add(new TableMigrationField { FieldName = "Active", FieldType = TableMigrationFieldType.Bit });
        //    t.Fields.Add(new TableMigrationField { FieldName = "Description", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000, IsNullAble = true });

        //    for (int i = 0; i < GenericData.GenericDataExtraFieldCount; i++)
        //        t.Fields.Add(new TableMigrationField { FieldName = $"{GenericData.GenericDataExtraFieldPrefix}{i}", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000, IsNullAble = true });

        //    db.ExecurteCreateTable(t.ToString());
        //}
    }

}