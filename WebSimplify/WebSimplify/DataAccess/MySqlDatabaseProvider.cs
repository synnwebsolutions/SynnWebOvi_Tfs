using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

namespace SynnWebOvi
{
    internal class MySqlDatabaseProvider : IDatabaseProvider
    {
        private string _connectionString;
        internal IDbAuth IDbAuth = null;
        internal IDbUserDictionary IDbUserDictionary = null;
        internal IDbLog IDbLog = null;
        internal IDbWedd IDbWedd = null;
        public MySqlDatabaseProvider(string _connectionString)
        {
            this._connectionString = _connectionString;
        }

        public IDbAuth DbAuth
        {
            get
            {
                if (IDbAuth == null)
                    IDbAuth = new MySqlDbAuth(_connectionString);
                return IDbAuth;
            }
        }

        public IDbWedd DbWedd
        {
            get
            {
                if (IDbWedd == null)
                    IDbWedd = new MySqlDbWedd(_connectionString);
                return IDbWedd;
            }
        }

        public IDbUserDictionary DbUserDictionary
        {
            get
            {
                if (IDbUserDictionary == null)
                    IDbUserDictionary = new MySqlDbUserDictionary(_connectionString);
                return IDbUserDictionary;
            }
        }

        public IDbLog DbLog
        {
            get
            {
                if (IDbLog == null)
                    IDbLog = new MySqlDbLog(_connectionString);
                return IDbLog;
            }
        }

        public LoggedUser CurrentUser
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void SetUser(LoggedUser u)
        {
            throw new NotImplementedException();
        }
    }

    internal class MySqlDbAuth : BaseSqlDbExecuter,IDbAuth
    {
        public MySqlDbAuth(string _connectionString) : base(_connectionString)
        {
        }

        public LoggedUser LoadUserSettings(string userName, string passwword)
        {
            throw new NotImplementedException();
        }

        public bool ValidateUserCredentials(string userName, string passwword)
        {       
            try
            {
                using (conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string stm = string.Format("SELECT * FROM {0} where UserName=@Name and Password=@pass", SynnDataProvider.TableNames.Users);
                    using (cmd = new MySqlCommand(stm, conn as MySqlConnection))
                    {

                        //cmd.Parameters.AddWithValue("@Name", userName);
                        //cmd.Parameters.AddWithValue("@pass", passwword);

                        using (rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                //string det = rdr.GetInt32(0) + ": " + rdr.GetString(1);
                                return true;
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }

            }
            return false;
        }
    }

    public class MySqlDbLog : BaseSqlDbExecuter, IDbLog
    {
        public MySqlDbLog(string _connectionString) : base(_connectionString)
        {
        }

        public void AddLog(string message)
        {
            throw new NotImplementedException();
        }

        public string AddLog(Exception ex)
        {
            throw new NotImplementedException();
        }

        public List<LogItem> GetLogs(LogSearchParameters lsp)
        {
            throw new NotImplementedException();
        }
    }

    public class MySqlDbUserDictionary : BaseSqlDbExecuter, IDbUserDictionary
    {
        public MySqlDbUserDictionary(string _connectionString) : base(_connectionString)
        {
        }

        public void Add(string key, string value)
        {
            throw new NotImplementedException();
        }

        public List<DictionaryItem> PerformSearch(string searchText)
        {
            throw new NotImplementedException();
        }
    }
}