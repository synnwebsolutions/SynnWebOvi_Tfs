using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace SynnWebOvi
{
    public class BaseSqlDbExecuter
    {
        internal DbConnection conn = null;
        internal IDataReader rdr = null;
        internal DbCommand cmd = null;
        internal string _connectionString;

        public BaseSqlDbExecuter(string _connectionString)
        {
            this._connectionString = _connectionString;
        }
    }
}