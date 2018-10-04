using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SynnCore.DataAccess;
using System.Data;

namespace SynnWebOvi
{
    public class LoggedUser : IDbLoadable
    {
        public LoggedUser()
        {

        }
        public LoggedUser(IDataReader data)
        {
            Load(data);
        }
        public int Id { get; private set; }
        public string UserName { get; private set; }

        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            UserName = DataAccessUtility.LoadNullable<string>(reader, "UserName");
            //Description = DataAccessUtility.LoadNullable<string>(reader, "Description");
        }
    }
}