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

        }
        public LoggedUser(IDataReader data)
        {
            Load(data);
        }
        public int Id { get;  set; }
        public string UserName { get;  set; }
        public int UserGroupId { get;  set; }

        public List<ClientPagePermissions> AllowedClientPagePermissions { get; set; }
        public List<UserSharedGroupPermissions> AllowedSharedPermissions { get; set; }
        
        public LoggedUser(string u, int i)
        {
            Id = i;
            UserName  = u;
        }


        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            UserName = DataAccessUtility.LoadNullable<string>(reader, "UserName");
            string cper = DataAccessUtility.LoadNullable<string>(reader, "AllowedClientPagePermissions");
            if (string.IsNullOrEmpty(cper))
                AllowedClientPagePermissions = new List<ClientPagePermissions>();
            else
                AllowedClientPagePermissions = XmlHelper.CreateFromXml<List<ClientPagePermissions>>(cper);

            string shaper = DataAccessUtility.LoadNullable<string>(reader, "AllowedSharedPermissions");
            if (string.IsNullOrEmpty(shaper))
                AllowedSharedPermissions = new List<UserSharedGroupPermissions>();
            else
                AllowedSharedPermissions = XmlHelper.CreateFromXml<List<UserSharedGroupPermissions>>(shaper);
        }
    }
}