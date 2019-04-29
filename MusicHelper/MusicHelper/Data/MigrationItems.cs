using SynnCore.Migration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelper
{
    public class MigrationItems
    {
        //public static void T3(IDbMigration db)
        //{
        //    //var t = new TableMigration
        //    //{
        //    //    HasIdentity = true,
        //    //    TableName = "testmigration3"
        //    //};
        //    //t.Fields = new List<TableMigrationField>();
        //    //t.Fields.Add(new TableMigrationField { FieldName = "col1", FieldType = TableMigrationFieldType.Integer, IsNullAble = false });
        //    //t.Fields.Add(new TableMigrationField { FieldName = "col2", FieldType = TableMigrationFieldType.Decimal, IsNullAble = true });
        //    //t.Fields.Add(new TableMigrationField { FieldName = "col3", FieldType = TableMigrationFieldType.Date, IsNullAble = true });
        //    //t.Fields.Add(new TableMigrationField { FieldName = "col4", FieldType = TableMigrationFieldType.Text, IsNullAble = true });
        //    //t.Fields.Add(new TableMigrationField { FieldName = "col5", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 500, IsNullAble = true });

        //    //db.ExecurteCreateTable(t.ToString());
        //}

        public static void MigrationTable(IdbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = "migItems"
            };
            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "Name", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 5000, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "Date", FieldType = TableMigrationFieldType.Date, IsNullAble = true });

            db.ExecurteCreateTable(t.ToString());
        }

        public static void MusicItemsTable(IdbMigration db)
        {
            var t = new TableMigration
            {
                HasIdentity = true,
                TableName = "MusicItems"
            };
            t.Fields = new List<TableMigrationField>();
            t.Fields.Add(new TableMigrationField { FieldName = "Artist", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 5000, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "FullFileName", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 5000, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "FileName", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 5000, IsNullAble = true });
            t.Fields.Add(new TableMigrationField { FieldName = "Title", FieldType = TableMigrationFieldType.Varchar, FieldLLenght = 5000, IsNullAble = true });

            db.ExecurteCreateTable(t.ToString());
        }
    }
}
