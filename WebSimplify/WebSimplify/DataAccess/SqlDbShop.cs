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

        public ShoppingData GetData()
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.ShoppingData);
            ShoppingData prefs = new ShoppingData();
            using (IDataReader data = DoSelect())
            {
                if (data.Read())
                    prefs = XmlHelper.CreateFromXml<ShoppingData>(data["ShopingData"].ToString());
                return prefs;
            }
        }

        public void Update(ShoppingData sd)
        {
            string prefs = XmlHelper.ToXml(sd);
            DeleteExsisting();
            SetSqlFormat("insert  into {0} ( ShopingData ) values ( ? )", SynnDataProvider.TableNames.ShoppingData);
            SetParameters(prefs);
            ExecuteSql();
            Commit();
        }

        private void DeleteExsisting()
        {
            SetSqlFormat("delete {0}", SynnDataProvider.TableNames.ShoppingData);
            ExecuteSql();
        }
    }
}