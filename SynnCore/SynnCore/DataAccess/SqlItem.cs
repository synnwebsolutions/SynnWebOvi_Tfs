using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynnCore.DataAccess
{
    public class SqlItem
    {
        public SqlItem()
        {
        }

        public SqlItem(string fieldName, object fieldValue)
        {
            FieldName = fieldName;
            FieldValue = fieldValue;
        }

        public string FieldName { get; set; }
        public object FieldValue { get; set; }
    }

    public class SqlItemList : List<SqlItem>
    {

    }
}
