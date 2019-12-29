using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SynnCore.DataAccess;
using System.Data;
using WebSimplify;
using SynnCore.Generics;
using WebSimplify.Data;

namespace SynnWebOvi
{
    public class LoggedUser : IDbLoadable
    {
        public LoggedUser()
        {
            AllowedClientPagePermissions = new List<ClientPagePermissions>();
        }
        public LoggedUser(IDataReader data)
        {
            Load(data);
        }
        public int Id { get;  set; }
        public string UserName { get;  set; }
        public string Password { get; set; }
        public int UserGroupId { get;  set; }
        public string DisplayName { get; set; }

        public UserAppPreferences Preferences { get; set; }
        public List<ClientPagePermissions> AllowedClientPagePermissions { get; set; }
        
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
            DisplayName = DataAccessUtility.LoadNullable<string>(reader, "DisplayName");
            string cper = DataAccessUtility.LoadNullable<string>(reader, "AllowedClientPagePermissions");
            if (string.IsNullOrEmpty(cper))
                AllowedClientPagePermissions = new List<ClientPagePermissions>();
            else
                AllowedClientPagePermissions = XmlHelper.CreateFromXml<List<ClientPagePermissions>>(cper);
            
        }

        internal bool Allowed(ClientPagePermissions p)
        {
            return IsAdmin || AllowedClientPagePermissions.Contains(p);
        }
    }
}