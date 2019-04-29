using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynnCore.Migration
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
            sb.AppendFormat("CREATE TABLE [dbo].[{0}] ({3} {1} {2} ) ON [PRIMARY]", TableName, cols, identity, idcol);
            return sb.ToString();
        }
        public bool Generated { get; set; }
    }

}
