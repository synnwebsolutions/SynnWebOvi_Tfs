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
        public static string GetConnectionString(this object frm)
        {
            return ConfigurationSettings.AppSettings["connectionString"];
        }
        public static IDatabaseProvider InitDataProvider(this object frm)
        {
            IDatabaseProvider dbc = null;
            if (dbc == null)
            {
                string _connectionString = string.Empty;
#if DEBUG
                _connectionString = ConfigurationSettings.AppSettings["connectionString"];
#else
            _connectionString = ConfigurationSettings.AppSettings["prodConnectionString"];
#endif

                dbc = new SqlDatabaseProvider(new SynnSqlDataProvider(_connectionString));
            }
            return dbc;
        }
    }
}
