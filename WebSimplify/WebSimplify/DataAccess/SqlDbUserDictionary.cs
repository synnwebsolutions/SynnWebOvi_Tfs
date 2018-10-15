using SynnCore.DataAccess;
using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class SqlDbUserDictionary : SqlDbController, IDbUserDictionary
    {
        public SqlDbUserDictionary(string _connectionString) : base(new SynnSqlDataProvider(_connectionString))
        {
        }

        public void Add(string key, string value,int userId)
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
            if (!string.IsNullOrEmpty(searchText))
            {
                StartORGroup();
                AddORLikeField("dKey", searchText, LikeSelectionStyle.CheckBoth);
                AddORLikeField("Value", searchText, LikeSelectionStyle.CheckBoth);
                EndORGroup();
            }
            var lst = new List<DictionaryItem>();
            FillList(lst, typeof(DictionaryItem));
            return lst;
        }
    }

}