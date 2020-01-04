using CalendarUtilities;
using SynnCore.DataAccess;
using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class SqlDbGoogle : SqlDbController, IDbGoogle, IGoogleDataStore
    {
        public SqlDbGoogle(string _connectionString) : base(new SynnSqlDataProvider(_connectionString))
        {
        }

        public void ClearData()
        {
            SetSqlFormat("truncate table  {0} ", SynnDataProvider.TableNames.GoogleTokens);
            ExecuteSql();
        }

        private void CheckKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key MUST have a value");
            }
        }

        public void DeleteStoredKey(string key)
        {
            CheckKey(key);
            SetSqlFormat("DELETE FROM {0} WHERE userid = ?", SynnDataProvider.TableNames.GoogleTokens);
            SetParameters(key);
            ExecuteSql();
        }

        public string GetUserCredentialsByKey(string key)
        {
            CheckKey(key);
            SetSqlFormat("select credentials from {0}", SynnDataProvider.TableNames.GoogleTokens);
            ClearParameters();
            
            AddSqlWhereLikeField("userid", key, LikeSelectionStyle.AsIs);

            return GetSingleRecordFirstValue().ToString();
        }

        public void Upsert(string key, string serializedCredentials)
        {
            CheckKey(key);

            var userCreds = GetUserCredentialsByKey(key);
            if (!string.IsNullOrEmpty(userCreds))
            {
                SqlItemList where = new SqlItemList { new SqlItem("userid", key) };
                SqlItemList items = new SqlItemList { new SqlItem("credentials", serializedCredentials) };
                SetUpdateSql(SynnDataProvider.TableNames.GoogleTokens, items, where);
            }
            else
            {
                SqlItemList items = new SqlItemList();
                items.Add(new SqlItem("userid", key));
                items.Add(new SqlItem("credentials", serializedCredentials));
                SetInsertIntoSql(SynnDataProvider.TableNames.GoogleTokens, items);
            }
            ExecuteSql();
        }

        public string GetCredentialsJsonString(int userId)
        {
            SetSqlFormat("select credentials from {0}", SynnDataProvider.TableNames.GoogleAPICredentials);
            ClearParameters();

            AddSqlWhereField("userid", userId);

            return GetSingleRecordFirstValue().ToString();
        }
    }
}