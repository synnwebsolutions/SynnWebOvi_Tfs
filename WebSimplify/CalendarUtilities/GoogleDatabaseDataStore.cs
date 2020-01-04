using Google.Apis.Json;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarUtilities
{
   
        /// <summary>
        /// Database data store that implements <see cref="IDataStore"/>. This store creates a different row for each
        /// combination of type and key. This file data store stores a JSON format of the specified object.
        /// </summary>
        internal abstract class DatabaseDatastore : IDataStore
        {
            /// <summary>Represents a connection to a database. </summary>
            public virtual DbConnection Connection { get; set; }

            /// <summary>Represents an SQL statement or stored procedure to execute against a data source.
            ///          Provides a base class for database-specific classes that represent commands.</summary>
            public virtual DbCommand Command { get; set; }

            /// <summary>The string used to open the connection.</summary>
            public virtual string ConnectionString { get; set; }

            public string CredentialsTableName { get; private set; }

            /// <summary>
            /// Creates a new table in the data base if the Users table does not exist within the database used in the connectionstring.
            /// </summary>
            /// <param name="connectionString">The string used to open the connection.</param>
            public DatabaseDatastore(string connectionString, string tableName = null)
            {
                CredentialsTableName = tableName;
                if (string.IsNullOrEmpty(CredentialsTableName))
                    CredentialsTableName = "GoogleUserCredentials";

                ConnectionString = connectionString;
                if (!DoesUserTableExist())
                {
                    Command.CommandType = System.Data.CommandType.Text;
                    Command.CommandText = string.Format("CREATE TABLE [{0}]([UserId][nvarchar](100) NOT NULL,[Credentials] [nvarchar](2000) NOT NULL)", CredentialsTableName);
                    Command.Connection.Open();
                    Command.ExecuteNonQueryAsync();
                    Command.Connection.Close();
                }
            }

            /// <summary>
            /// Stores the given value for the given key. It creates a new row in the database with the user id of
            /// (primary key <see cref="GenerateStoredKey"/>) in <see cref="CredentialsTableName"/>.
            /// </summary>
            /// <typeparam name="T">The type to store in the data store.</typeparam>
            /// <param name="key">The key.</param>
            /// <param name="value">The value to store in the data store.</param>
            Task IDataStore.StoreAsync<T>(string key, T value)
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentException("Key MUST have a value");
                }

                var serialized = NewtonsoftJsonSerializer.Instance.Serialize(value);
                save(GenerateStoredKey(key, typeof(T)), serialized);
                return Task.Delay(0);
            }

            /// <summary>
            /// Deletes the given key. It deletes the <see cref="GenerateStoredKey"/> row in
            /// <see cref="CredentialsTableName"/>.
            /// </summary>
            /// <param name="key">The key to delete from the data store.</param>
            Task IDataStore.DeleteAsync<T>(string key)
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentException("Key MUST have a value");
                }

                try
                {
                    Command.CommandType = System.Data.CommandType.Text;
                    Command.CommandText = string.Format("delete from {0} where userid = '{1}'", CredentialsTableName, GenerateStoredKey(key, typeof(T)));
                    Command.Connection.Open();
                    Command.ExecuteNonQueryAsync();
                    Command.Connection.Close();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    if (Command.Connection.State == System.Data.ConnectionState.Open)
                        Command.Connection.Close();

                    throw new Exception("Failed to delete credentials", ex);
                }

                return Task.Delay(0);
            }

            /// <summary>
            /// Returns the stored value for the given key or <c>null</c> if the matching row (<see cref="GenerateStoredKey"/>
            /// in <see cref="CredentialsTableName"/> doesn't exist.
            /// </summary>
            /// <typeparam name="T">The type to retrieve.</typeparam>
            /// <param name="key">The key to retrieve from the data store.</param>
            /// <returns>The stored object.</returns>
            Task<T> IDataStore.GetAsync<T>(string key)
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentException("Key MUST have a value");
                }

                TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
                var credentials = GetUserByKey(GenerateStoredKey(key, typeof(T)));
                if (credentials != null)
                {
                    try
                    {
                        tcs.SetResult(NewtonsoftJsonSerializer.Instance.Deserialize<T>(credentials));
                    }
                    catch (Exception ex)
                    {
                        tcs.SetException(ex);
                    }
                }
                else
                {
                    tcs.SetResult(default(T));
                }
                return tcs.Task;
            }

            /// <summary>
            /// Clears all values in the data store. This method deletes all files in <see cref="CredentialsTableName"/>.
            /// </summary>
            Task IDataStore.ClearAsync()
            {
                try
                {
                    Command.CommandType = System.Data.CommandType.Text;
                    Command.CommandText = "truncate table " + CredentialsTableName;
                    Command.Connection.Open();
                    DbDataReader reader = Command.ExecuteReader();
                    reader.Close();
                    Command.Connection.Close();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception("Failed to clear credentials", ex);
                }

                return Task.Delay(0);
            }

            /// <summary>
            /// Checks if the table exits <see cref="CredentialsTableName"/>.
            /// </summary>
            private bool DoesUserTableExist()
            {
                try
                {
                    Command.CommandType = System.Data.CommandType.Text;
                    Command.CommandText = "select * from " + CredentialsTableName;
                    Command.Connection.Open();
                    DbDataReader reader = Command.ExecuteReader();
                    reader.Close();
                    Command.Connection.Close();
                    return true;
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    Command.Connection.Close();
                    return false;
                }
            }

            /// <summary>
            /// Checks if the user exists <see cref="GenerateStoredKey"/>.
            /// </summary>
            private string GetUserByKey(string key)
            {
                try
                {
                    string credentials = string.Empty;
                    Command.CommandType = System.Data.CommandType.Text;
                    Command.CommandText = string.Format("select Credentials from {0} where userid = '{1}'", CredentialsTableName, key);
                    Command.Connection.Open();
                    DbDataReader reader = Command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        credentials = reader[0].ToString();
                    }

                    reader.Close();
                    Command.Connection.Close();

                    if (string.IsNullOrEmpty(credentials))
                        return null;

                    return credentials;
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    if (Command.Connection.State == System.Data.ConnectionState.Open)
                        Command.Connection.Close();
                    return null;
                }
            }

            /// <summary>
            /// Save the credentials.  If the user <see cref="GenerateStoredKey"/> does not exists we insert it other wise we will do an update.
            /// </summary>
            /// <param name="key"></param>
            /// <param name="serialized"></param>
            private void save(string key, string serialized)
            {
                try
                {
                    if (GetUserByKey(key) == null)
                    {
                        Command.CommandText = string.Format("insert into {0} (userid, Credentials) values ('{1}','{2}') ", CredentialsTableName, key, serialized);
                    }
                    else
                    {
                        Command.CommandText = string.Format("update {0}  set Credentials = '{1}' where key = '{2}'", CredentialsTableName, serialized, key);
                    }

                    Command.CommandType = System.Data.CommandType.Text;
                    Command.Connection.Open();
                    Command.ExecuteReader();
                    Command.Connection.Close();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    if (Command.Connection.State == System.Data.ConnectionState.Open)
                        Command.Connection.Close();
                    throw ex;
                }
            }

            /// <summary>Creates a unique stored key based on the key and the class type.</summary>
            /// <param name="key">The object key.</param>
            /// <param name="t">The type to store or retrieve.</param>
            public static string GenerateStoredKey(string key, Type t)
            {
                return string.Format("{0}-{1}", t.FullName, key);
            }
        }
    }
