using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SynnWebOvi
{
    internal class SqlDatabaseProvider : IDatabaseProvider
    {
        private string _connectionString;
        internal IDbAuth IDbAuth = null;
        internal IDbUserDictionary IDbUserDictionary = null;
        internal IDbLog IDbLog = null;
        internal LoggedUser LoggedUser = null;

        public SqlDatabaseProvider(string _connectionString)
        {
            this._connectionString = _connectionString;
        }

        public IDbAuth DbAuth
        {
            get
            {
                if (IDbAuth == null)
                    IDbAuth = new SqlDbAuth(_connectionString);
                return IDbAuth;
            }
        }

        public IDbUserDictionary DbUserDictionary
        {
            get
            {
                if (IDbUserDictionary == null)
                    IDbUserDictionary = new SqlDbUserDictionary(_connectionString);
                return IDbUserDictionary;
            }
        }

        public IDbLog DbLog
        {
            get
            {
                if (IDbLog == null)
                    IDbLog = new SqlDbLog(_connectionString);
                return IDbLog;
            }
        }

        public LoggedUser CurrentUser
        {
            get
            {
                if (LoggedUser == null)
                    return null;
                return LoggedUser;
            }
        }

        public void SetUser(LoggedUser u)
        {
            LoggedUser = u;
        }
    }

    internal class SqlDbAuth : SqlDbController, IDbAuth
    {
        public SqlDbAuth(string _connectionString) : base(new SynnSqlDataProvider( _connectionString))
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
            List<LoggedUser> lst = GetUsersEx(userName, passwword);
            return lst.Count == 1;
        }
    }

    internal class SqlDbLog : SqlDbController, IDbLog
    {
        public SqlDbLog(string _connectionString) : base(new SynnSqlDataProvider(_connectionString))
        {
        }

        public void AddLog(string message)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("Date", DateTime.Now));
            sqlItems.Add(new SqlItem("Trace", string.Empty));
            sqlItems.Add(new SqlItem("Message", message));
            SetInsertIntoSql(SynnDataProvider.TableNames.Log, sqlItems);
            ExecuteSql();
        }

        public string AddLog(Exception l)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("Date", DateTime.Now));
            sqlItems.Add(new SqlItem("Trace", l.StackTrace));
            sqlItems.Add(new SqlItem("Message", l.Message));
            SetInsertIntoSql(SynnDataProvider.TableNames.Log, sqlItems);
            ExecuteSql();
            return GetMsSqlLastIdentityValue().ToString();
        }
    }

    internal class SqlDbUserDictionary : SqlDbController, IDbUserDictionary
    {
        public SqlDbUserDictionary(string _connectionString) : base(new SynnSqlDataProvider(_connectionString))
        {
        }

        public void Add(string key, string value)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("UserId", SynnDataProvider.DbProvider.CurrentUser.Id));
            sqlItems.Add(new SqlItem("dKey", key));
            sqlItems.Add(new SqlItem("Value", value));
            SetInsertIntoSql(SynnDataProvider.TableNames.UserDictionary, sqlItems);
            ExecuteSql();
        }

        public List<DictionaryItem> PerformSearch(string searchText)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.UserDictionary);
            ClearParameters();
            StartORGroup();
            AddORLikeField("dKey", searchText,LikeSelectionStyle.CheckBoth);
            AddORLikeField("Value", searchText,LikeSelectionStyle.CheckBoth);
            EndORGroup();
            var lst = new List<DictionaryItem>();
            FillList(lst, typeof(DictionaryItem));
            return lst;
        }
    }

}