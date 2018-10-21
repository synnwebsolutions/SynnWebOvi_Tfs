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

        public void Add(DictionarySearchParameters p)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("UserGroupId", p.CurrentUser.Id));
            sqlItems.Add(new SqlItem("dKey", p.Key));
            sqlItems.Add(new SqlItem("Value", p.Value));
            SetInsertIntoSql(SynnDataProvider.TableNames.UserDictionary, sqlItems);
            ExecuteSql();
        }

        public List<DictionaryItem> PerformSearch(DictionarySearchParameters p)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.UserDictionary);
            ClearParameters();
            AddSqlWhereField("UserGroupId", p.CurrentUser.Id);
            if (!string.IsNullOrEmpty(p.SearchText))
            {
                StartORGroup();
                AddORLikeField("dKey", p.SearchText, LikeSelectionStyle.CheckBoth);
                AddORLikeField("Value", p.SearchText, LikeSelectionStyle.CheckBoth);
                EndORGroup();
            }
            var lst = new List<DictionaryItem>();
            FillList(lst, typeof(DictionaryItem));
            return lst;
        }
    }

}