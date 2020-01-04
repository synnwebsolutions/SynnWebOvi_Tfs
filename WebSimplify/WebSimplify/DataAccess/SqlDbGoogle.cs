using CalendarUtilities;
using SynnCore.DataAccess;
using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSimplify.Data;

namespace WebSimplify
{
    public class SqlDbGoogle : SqlDbController, IDbGoogle, IGoogleDataStore
    {
        public SqlDbGoogle(string _connectionString) : base(new SynnSqlDataProvider(_connectionString))
        {
        }

       
        int appUserId { get; set; }

        int IDbGoogle.AppUserId
        {
            get
            {
                return appUserId;
            }
            set
            {
                appUserId = value;
            }
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
            SetSqlFormat("DELETE FROM {0} WHERE userid = ? and appuserid = ?", SynnDataProvider.TableNames.GoogleTokens);
            SetParameters(key, appUserId);
            ExecuteSql();
        }

        public string GetUserCredentialsByKey(string key)
        {
            CheckKey(key);
            SetSqlFormat("select credentials from {0}", SynnDataProvider.TableNames.GoogleTokens);
            ClearParameters();

            AddSqlWhereField("appuserid", appUserId);
            AddSqlWhereLikeField("userid", key, LikeSelectionStyle.AsIs);

            try
            {
                var res = GetSingleRecordFirstValue();

                return res == null ? string.Empty : res.ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public void Upsert(string key, string serializedCredentials)
        {
            CheckKey(key);

            var userCreds = GetUserCredentialsByKey(key);
            if (!string.IsNullOrEmpty(userCreds))
            {
                SqlItemList where = new SqlItemList
                {
                    new SqlItem("userid", key),
                    new SqlItem("appuserid", appUserId)
                };
                SqlItemList items = new SqlItemList { new SqlItem("credentials", serializedCredentials) };
                SetUpdateSql(SynnDataProvider.TableNames.GoogleTokens, items, where);
            }
            else
            {
                SqlItemList items = new SqlItemList();
                items.Add(new SqlItem("userid", key));
                items.Add(new SqlItem("appuserid", appUserId));
                items.Add(new SqlItem("credentials", serializedCredentials));
                SetInsertIntoSql(SynnDataProvider.TableNames.GoogleTokens, items);
            }
            ExecuteSql();
        }
        
    }
}