using System;

namespace SynnWebOvi
{
    internal class SqlDatabaseProvider : IDatabaseProvider
    {
        private string _connectionString;

        public SqlDatabaseProvider(string _connectionString)
        {
            this._connectionString = _connectionString;
        }

        public IDbAuth DbAuth
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IDbUserDictionary DbUserDictionary
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IDbLog DLog
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}