using SynnCore.DataAccess;
using SynnCore.Generics;
using System;
using System.Data;
using WebSimplify;
using System.Collections.Generic;
using System.Linq;

namespace SynnWebOvi
{
    internal class SqlDbShop : SqlDbController,IDbShop
    {
        public SqlDbShop(string _connectionString) : base(new SynnSqlDataProvider(_connectionString))
        {
        }
        private void SetPermissions(ShopSearchParameters sp)
        {
            StartORGroup();
            foreach (int gid in sp.CurrentUser.AllowedSharedPermissions)
                AddOREqualField("UserGroupId", gid);
            EndORGroup();
        }

        public List<ShopItem> Get(ShopSearchParameters lsp)
        {
            SetSqlFormat("select * from {0} si", SynnDataProvider.TableNames.ShoppingItems);
            if (lsp.Active.HasValue)
                AddSqlText(string.Format("inner join {0} usi on usi.ItemId = si.Id", SynnDataProvider.TableNames.User_ShoppingItems)); // join
     
            ClearParameters();
            if (lsp.Active.HasValue)
            {
                StartORGroup();
                foreach (int gid in lsp.CurrentUser.AllowedSharedPermissions)
                    AddOREqualField("usi.UserGroupId", gid);
                EndORGroup();
            }
            if(!string.IsNullOrEmpty(lsp.ItemName))
                AddSqlWhereField("Name", lsp.ItemName);


            var lst = new List<ShopItem>();
            FillList(lst, typeof(ShopItem));
            return lst;
        }

        public void ActivateShopItem(ShopSearchParameters sp)
        {
            if (sp.IdToActivate.HasValue)
            {
                var sqlItems = new SqlItemList();
                sqlItems.Add(new SqlItem("ItemId", sp.IdToActivate.Value));
                sqlItems.Add(new SqlItem("UserGroupId", sp.CurrentUser.AllowedSharedPermissions[0]));
                SetInsertIntoSql(SynnDataProvider.TableNames.User_ShoppingItems, sqlItems);
                ExecuteSql();
            }
        }

        public void DeActivateShopItem(ShopSearchParameters lsp)
        {
            if (lsp.IdToDeactivate.HasValue)
            {
                SetSqlFormat("delete {0}", SynnDataProvider.TableNames.User_ShoppingItems);
                ClearParameters();
                AddSqlWhereField("ItemId", lsp.IdToDeactivate.Value);
                AddSqlWhereField("UserGroupId", lsp.CurrentUser.AllowedSharedPermissions[0]);
                ExecuteSql();
            }
        }

        public void AddNewShopItem(ShopItem n)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("Name", n.Name));
            SetInsertIntoSql(SynnDataProvider.TableNames.ShoppingItems, sqlItems);
            ExecuteSql();
        }
    }
}