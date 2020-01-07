using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class MigrationInitItems
    {
        public static void MigrationItemsTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.MigrationItems
            };
            t.Fields = new List<TableMigrationField>();

            t.Fields.Add(new TableMigrationField { FieldName = "Name", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 500, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "Date", FieldType = TableMigrationFieldType.Date });

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
            t.Fields.Add(new TableMigrationField { FieldName = "UserName", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000 });
            t.Fields.Add(new TableMigrationField { FieldName = "DisplayName", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000 });
            t.Fields.Add(new TableMigrationField { FieldName = "Password", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 12 });
            t.Fields.Add(new TableMigrationField { FieldName = "AllowedClientPagePermissions", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "EmailAdress", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000, IsNullAble = true });
            db.ExecurteCreateTable(t.ToString());
        }

        public static void LogTable(IDbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = SynnDataProvider.TableNames.Log
            };


            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "Message", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000 });
            t.Fields.Add(new TableMigrationField { FieldName = "Trace", FieldType = TableMigrationFieldType.NVarchar, FieldLLenght = 4000 });
            t.Fields.Add(new TableMigrationField { FieldName = "Date", FieldType = TableMigrationFieldType.Date });

            db.ExecurteCreateTable(t.ToString());
        }
    }

}