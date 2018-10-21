using SynnCore.DataAccess;
using SynnCore.Generics;
using System;
using System.Data;
using WebSimplify;

namespace SynnWebOvi
{
    internal class SqlDbShop : SqlDbController,IDbShop
    {
        public SqlDbShop(string _connectionString) : base(new SynnSqlDataProvider(_connectionString))
        {
        }

        public ShoppingData GetData(ShopSearchParameters sp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.ShoppingData);
            ClearParameters();

            SetPermissions(sp);
            ShoppingData prefs = new ShoppingData();
            using (IDataReader data = DoSelect())
            {
                if (data.Read())
                    prefs = XmlHelper.CreateFromXml<ShoppingData>(data["ShopingData"].ToString());
                return prefs;
            }
        }
        private void SetPermissions(ShopSearchParameters sp)
        {
            StartORGroup();
            foreach (int gid in sp.CurrentUser.AllowedSharedPermissions)
                AddOREqualField("UserGroupId", gid);
            EndORGroup();
        }

        public void Update(ShopSearchParameters sp)
        {
            string prefs = XmlHelper.ToXml(sp.ItemForAction);
            DeleteExsisting(sp);
            SetSqlFormat("insert  into {0} ( ShopingData,UserGroupId) values ( ?,? )", SynnDataProvider.TableNames.ShoppingData);
            ClearParameters();
            SetParameters(prefs, sp.CurrentUser.AllowedSharedPermissions[0].ToString());
            ExecuteSql();
        }

        private void DeleteExsisting(ShopSearchParameters sp)
        {
            SetSqlFormat("delete {0}", SynnDataProvider.TableNames.ShoppingData);
            ClearParameters();
            SetPermissions(sp);
            ExecuteSql();
        }
    }
}