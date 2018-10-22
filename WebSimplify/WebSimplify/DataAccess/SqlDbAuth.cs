using SynnCore.DataAccess;
using SynnCore.Generics;
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
            List<LoggedUser> lst = GetUsersEx(new UserSearchParameters { UserName = userName, Password = passwword });
            return lst.FirstOrDefault();
        }

        private List<LoggedUser> GetUsersEx(UserSearchParameters lp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.Users);
            ClearParameters();
            if (!string.IsNullOrEmpty(lp.UserName))
                AddSqlWhereLikeField("UserName", lp.UserName);
            if (!string.IsNullOrEmpty(lp.Password))
                AddSqlWhereLikeField("Password", lp.Password);
            if (lp.Id.HasValue)
                AddSqlWhereLikeField("Id", lp.Id.ToString());
            var lst = new List<LoggedUser>();
            FillList(lst, typeof(LoggedUser));
            return lst;
        }

        public bool ValidateUserCredentials(string userName, string passwword)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passwword))
                return false;
            List<LoggedUser> lst = GetUsersEx(new UserSearchParameters { UserName = userName, Password = passwword });
            return lst.Count == 1;
        }

        public List<PermissionGroup> GetPermissionGroup()
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.PermissionGroups);
            ClearParameters();
            var lst = new List<PermissionGroup>();
            FillList(lst, typeof(PermissionGroup));
            return lst;
        }

        public void Add(PermissionGroup g)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("Name", g.Name));
            SetInsertIntoSql(SynnDataProvider.TableNames.PermissionGroups, sqlItems);
            ExecuteSql();
        }

        public void Add(LoggedUser u)
        {
            SqlItemList sqlItems = Get(u);
            SetInsertIntoSql(SynnDataProvider.TableNames.Users, sqlItems);
            ExecuteSql();
        }

        public List<LoggedUser> GetUsers(UserSearchParameters lp)
        {
            return GetUsersEx(lp);
        }

        public void Update(LoggedUser u)
        {
            SqlItemList sqlItems = Get(u);
            var wItems = new SqlItemList { new SqlItem("Id", u.Id) };
            SetUpdateSql(SynnDataProvider.TableNames.Users, sqlItems, wItems);
            ExecuteSql();
        }

        private static SqlItemList Get(LoggedUser u)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("UserName", u.UserName));
            sqlItems.Add(new SqlItem("Password", u.Password));
            sqlItems.Add(new SqlItem("DisplayName", u.DisplayName));
            sqlItems.Add(new SqlItem("AllowedClientPagePermissions", XmlHelper.ToXml(u.AllowedClientPagePermissions)));
            sqlItems.Add(new SqlItem("AllowedSharedPermissions", XmlHelper.ToXml(u.AllowedSharedPermissions)));
            return sqlItems;
        }

        public LoggedUser GetUser(int uId)
        {
            return GetUsersEx(new UserSearchParameters { Id = uId }).First();
        }
    }


}