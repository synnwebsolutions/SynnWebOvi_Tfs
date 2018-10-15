using SynnCore.DataAccess;
using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSimplify
{

    internal class SqlDbAuth : SqlDbController, IDbAuth
    {
        public SqlDbAuth(string _connectionString) : base(new SynnSqlDataProvider(_connectionString))
        {
        }

        public LoggedUser LoadUserSettings(string userName, string passwword)
        {
            List<LoggedUser> lst = GetUsersEx(userName, passwword);
            return lst.FirstOrDefault();
        }

        private List<LoggedUser> GetUsersEx(string userName, string passwword)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.Users);
            ClearParameters();
            AddSqlWhereLikeField("UserName", userName);
            AddSqlWhereLikeField("Password", passwword);
            var lst = new List<LoggedUser>();
            FillList(lst, typeof(LoggedUser));
            return lst;
        }

        public bool ValidateUserCredentials(string userName, string passwword)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passwword))
                return false;
            List<LoggedUser> lst = GetUsersEx(userName, passwword);
            return lst.Count == 1;
        }
    }


}