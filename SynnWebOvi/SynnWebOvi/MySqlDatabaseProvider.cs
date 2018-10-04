using MySql.Data.MySqlClient;
using System;

namespace SynnWebOvi
{
    internal class MySqlDatabaseProvider : IDatabaseProvider
    {
        private string _connectionString;
        internal IDbAuth IDbAuth = null;
        internal IDbUserDictionary IDbUserDictionary = null;
        internal IDbLog IDbLog = null;
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

        public IDbUserDictionary DbUserDictionary
        {
            get
            {
                if (IDbUserDictionary == null)
                    IDbUserDictionary = new MySqlDbUserDictionary(_connectionString);
                return IDbUserDictionary;
            }
        }

        public IDbLog DLog
        {
            get
            {
                if (IDbLog == null)
                    IDbLog = new MySqlDbLog(_connectionString);
                return IDbLog;
            }
        }
    }

    public class BaseMySqlDb
    {
        internal MySqlConnection conn = null;
        internal MySqlDataReader rdr = null;
        internal MySqlCommand cmd = null;
        internal string _connectionString;

        public BaseMySqlDb(string _connectionString)
        {
            this._connectionString = _connectionString;
        }
    }

    internal class MySqlDbAuth : BaseMySqlDb,IDbAuth
    {
        public MySqlDbAuth(string _connectionString) : base(_connectionString)
        {
        }

        public bool ValidateUserCredentials(string userName, string passwword)
        {       
            try
            {
                using (conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string stm = string.Format("SELECT * FROM {0} where UserName=@Name and Password=@pass", DataProvider.TableNames.Users);
                    using (MySqlCommand cmd = new MySqlCommand(stm, conn))
                    {

                        cmd.Parameters.AddWithValue("@Name", userName);
                        cmd.Parameters.AddWithValue("@pass", passwword);

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

    public class MySqlDbLog : BaseMySqlDb, IDbLog
    {
        public MySqlDbLog(string _connectionString) : base(_connectionString)
        {
        }
    }

    public class MySqlDbUserDictionary : BaseMySqlDb, IDbUserDictionary
    {
        public MySqlDbUserDictionary(string _connectionString) : base(_connectionString)
        {
        }
    }
}