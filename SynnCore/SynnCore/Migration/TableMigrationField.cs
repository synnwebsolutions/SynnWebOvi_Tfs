using SynnCore.Generics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynnCore.Migration
{
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
            if (FieldLLenght != 0)
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
