using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SynnCore.DataAccess;
using System.Data;
using WebSimplify;
using SynnCore.Generics;

namespace SynnWebOvi
{
    public class LoggedUser : IDbLoadable
    {
        public LoggedUser()
        {
            AllowedClientPagePermissions = new List<ClientPagePermissions>();
            AllowedSharedPermissions = new List<int>();
        }
        public LoggedUser(IDataReader data)
        {
            Load(data);
        }
        public int Id { get;  set; }
        public string UserName { get;  set; }
        public string Password { get; set; }
        public int UserGroupId { get;  set; }

        public List<ClientPagePermissions> AllowedClientPagePermissions { get; set; }
        public List<int> AllowedSharedPermissions { get; set; }
        public bool IsAdmin { get; set; }

        public LoggedUser(string u, int i)
        {
            Id = i;
            UserName  = u;
            IsAdmin = true;
        }


        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            UserName = DataAccessUtility.LoadNullable<string>(reader, "UserName");
            Password = DataAccessUtility.LoadNullable<string>(reader, "Password");

            string cper = DataAccessUtility.LoadNullable<string>(reader, "AllowedClientPagePermissions");
            if (string.IsNullOrEmpty(cper))
                AllowedClientPagePermissions = new List<ClientPagePermissions>();
            else
                AllowedClientPagePermissions = XmlHelper.CreateFromXml<List<ClientPagePermissions>>(cper);

            string shaper = DataAccessUtility.LoadNullable<string>(reader, "AllowedSharedPermissions");
            if (string.IsNullOrEmpty(shaper))
                AllowedSharedPermissions = new List<int>();
            else
                AllowedSharedPermissions = XmlHelper.CreateFromXml<List<int>>(shaper);
        }

        internal bool Allowed(ClientPagePermissions p)
        {
            return IsAdmin || AllowedClientPagePermissions.Contains(p);
        }
    }
}