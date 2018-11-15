using SynnCore.Generics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;

namespace WebSimplify
{
    public class TableMigration
    {
        public string TableName { get; set; }
        public bool HasIdentity { get; set; }

        public List<TableMigrationField> Fields { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            string idcol = HasIdentity ? "[Id] [int] IDENTITY(1,1) NOT NULL," : string.Empty;
            string cols = string.Join(",", Fields);
            string identity = HasIdentity ? string.Format("CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED ([Id] ASC)", TableName) : string.Empty;
            sb.AppendFormat("CREATE TABLE [dbo].[{0}] ({3} {1} {2} ) ON [PRIMARY]",TableName,cols,identity,idcol);
            return sb.ToString();
        }
        public bool Generated { get; set; }
    }

    public class TableMigrationField
    {
        public string FieldName { get; set; }
        public bool IsNullAble { get; set; }
        public TableMigrationFieldType FieldType { get; set; }

        public int FieldLLenght { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("[{0}] ", FieldName);
            sb.AppendFormat("[{0}] ", GenericFormatter.GetEnumDescription(FieldType));
            if(FieldLLenght != 0 )
                sb.AppendFormat(" ({0}) ", FieldLLenght);
            sb.Append(IsNullAble ? "NULL" : "NOT NULL");
            return sb.ToString();
        }
    }

    public enum TableMigrationFieldType
    {
        [Description("decimal")]
        Decimal,
        [Description("int")]
        Integer,
        [Description("varchar")]
        Varchar,
        [Description("text")]
        Text,
        [Description("")]
        Data,
        [Description("datetime")]
        Date,
        [Description("bit")]
        Bit
    }
}