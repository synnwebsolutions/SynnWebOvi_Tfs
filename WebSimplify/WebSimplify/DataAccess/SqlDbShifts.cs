using SynnCore.DataAccess;
using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSimplify.Data;

namespace WebSimplify
{
    public class SqlDbShifts : SqlDbController, IDbShifts
    {
        public SqlDbShifts(string _connectionString) : base(new SynnSqlDataProvider(_connectionString))
        {

        }

        public UserShiftsContainer GetShiftsData(ShiftsSearchParameters shiftsSearchParameters)
        {
            throw new NotImplementedException();
        }

        public void Save(UserShiftsContainer currentData)
        {
            throw new NotImplementedException();
        }
    }
}