using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicHelper
{
    public static class ExtensionsHandler
    {
   
        public static IDatabaseProvider InitDataProvider(this Form frm)
        {
            IDatabaseProvider dbc = null;
            if (dbc == null)
            {
                string _connectionString = GetConnectionString();
                dbc = new SqlDatabaseProvider(new SynnSqlDataProvider(_connectionString));
            }
            return dbc;
        }

        public static string GetConnectionString()
        {
            string _connectionString = string.Empty;
#if DEBUG
            _connectionString = ConfigurationSettings.AppSettings["connectionString"];
#else
            _connectionString = ConfigurationSettings.AppSettings["prodConnectionString"];
#endif
            return _connectionString;
        }
    }
}
