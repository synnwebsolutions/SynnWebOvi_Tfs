using System;
using System.Collections.Generic;

namespace SynnWebOvi
{
    internal class MySqlDbWedd : IDbWedd
    {
        private string _connectionString;

        public MySqlDbWedd(string _connectionString)
        {
            this._connectionString = _connectionString;
        }

        public List<WeddingGuest> GetGuests(string searchText)
        {
            throw new NotImplementedException();
        }
    }
}