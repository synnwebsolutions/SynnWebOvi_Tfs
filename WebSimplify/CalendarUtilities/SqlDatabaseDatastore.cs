using Google.Apis.Util.Store;
using System.Data.Common;
using System.Data.SqlClient;

namespace CalendarUtilities
{
    internal class SqlDatabaseDatastore : DatabaseDatastore
    {
        public SqlDatabaseDatastore(string connectionString, string tableName = null) : base(connectionString, tableName)
        {
            connection = Connection;
        }

        private DbConnection connection = null;
        private DbCommand command = null;


        public SqlDatabaseDatastore(string connectionString) : base(connectionString)
        {
            connection = Connection;
        }

        public override DbConnection Connection
        {
            get
            {
                if (connection == null)
                {

                    connection = new SqlConnection(ConnectionString);
                }

                return connection;
            }
            set { connection = value; }
        }

        public override DbCommand Command
        {
            get
            {
                if (connection == null)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                }

                return command;
            }
            set { command = value; }
        }
    }
}