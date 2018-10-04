using System.Data;

namespace SynnCore.DataAccess
{
    public abstract class SynnDataProviderBase
    {
        protected string _connectionString;

        public SynnDataProviderBase()
        {
        }

        public SynnDataProviderBase(string connString)
        {
            setConnectionString(connString);
        }

        public void setConnectionString(string connString)
        {
            _connectionString = connString;
        }

        public abstract IDbConnection getConnection();
        public abstract IDbCommand getCommand();
        public abstract IDbDataParameter getParameter(object value);
    }
}