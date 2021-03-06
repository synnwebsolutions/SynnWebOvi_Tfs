﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SynnWebOvi
{
    public class SynnDataProvider
    {
        private static IDatabaseProvider dbc;
        public static IDatabaseProvider DbProvider
        {
            get
            {
                if (dbc == null)
                {
                    string _connectionString = string.Empty;
#if DEBUG
                    _connectionString = ConfigurationSettings.AppSettings["testconnectionString"];
#else
            _connectionString = ConfigurationSettings.AppSettings["prodConnectionString"];
#endif

                    if (ConfigurationSettings.AppSettings["dbtype"] == "sql")
                        dbc = new SqlDatabaseProvider(_connectionString);
                    else
                        dbc = new MySqlDatabaseProvider(_connectionString);
                }
                return dbc;
            }
        }

        public static class TableNames
        {
            public static string UserDictionary = "UserDictionary";
            public static string Users = "Users";
            public static string Log = "Log";
        }
    }

    public interface IDatabaseProvider
    {
         IDbAuth DbAuth { get; }
         IDbLog DbLog { get; }
         IDbUserDictionary DbUserDictionary { get; }
        LoggedUser CurrentUser { get; }

        void SetUser(LoggedUser u);
    }

    abstract class BaseDatabaseProvider : IDatabaseProvider
    {
        private string connString;

        public BaseDatabaseProvider(string connectionString)
        {
            this.connString = connectionString;
        }

        public virtual IDbAuth DbAuth
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual IDbUserDictionary DbUserDictionary
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual IDbLog DbLog
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual LoggedUser CurrentUser
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual void SetUser(LoggedUser u)
        {
            throw new NotImplementedException();
        }
    }

 
}