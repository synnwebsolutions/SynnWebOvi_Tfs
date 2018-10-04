using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynnCore.DataAccess
{
    public interface IDatabaseHelper
    {
        DataSet GetTableData(string iquery);
        bool Insert(string iquery);
        bool Delete(string iquery);
        bool Update(string iquery);
        IList Get(string SelectCommand, Type ListType, Type ItemType);
    }
}
