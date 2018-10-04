using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynnCore.DataAccess
{
    public class SynnSqlDataProvider : SynnDataProviderBase
    {
        public override IDbCommand getCommand()
        {
            return new SqlCommand();
        }

        public SynnSqlDataProvider(string connStr) : base(connStr)
        {

        }
        public override IDbConnection getConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public override IDbDataParameter getParameter(object value)
        {
            SqlParameter p = new SqlParameter();
            p.Value = value;
            p.ParameterName = "?";
            return p;
        }
    }
}
