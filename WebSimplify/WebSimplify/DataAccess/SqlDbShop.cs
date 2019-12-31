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

        public List<ShopItem> Get(ShopSearchParameters lsp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.ShoppingItems);
            ClearParameters();
            if (!string.IsNullOrEmpty(lsp.ItemName))
                AddSqlWhereField("Name", lsp.ItemName);
            if(lsp.Active.HasValue)
                AddSqlWhereField("Active", lsp.Active);

            var lst = new List<ShopItem>();
            FillList(lst, typeof(ShopItem));
            return lst;
        }

        public void ActivateShopItem(ShopSearchParameters sp)
        {
            if (sp.Id.HasValue)
            {
                sp.Active = true;
                Update(sp);
            }
        }

        private void Update(ShopSearchParameters i)
        {
            SqlItemList sqlItems = GetUpdateParams(i);
            SetUpdateSql(SynnDataProvider.TableNames.ShoppingItems, sqlItems, new SqlItemList { new SqlItem { FieldName = "Id", FieldValue = i.Id.Value } });
            ExecuteSql();
        }

        public SqlItemList GetUpdateParams(ShopSearchParameters i)
        {
            SqlItemList sqlItems = new SqlItemList();
            if (i.Active.HasValue)
                sqlItems.Add(new SqlItem("Active", i.Active.Value));
            if (i.LastBought.HasValue)
                sqlItems.Add(new SqlItem("LastBought", i.LastBought));
            return sqlItems;
        }


        public void DeActivateShopItem(ShopSearchParameters lsp)
        {
            if (lsp.Id.HasValue)
            {
                lsp.Active = false;
                Update(lsp);
            }
        }

        public void AddNewShopItem(ref ShopItem n)
        {
            var sqlItems = new SqlItemList();
            sqlItems.Add(new SqlItem("Name", n.Name));
            sqlItems.Add(new SqlItem("Active", true));
            SetInsertIntoSql(SynnDataProvider.TableNames.ShoppingItems, sqlItems);
            ExecuteSql();

            n.Id = GetLastIdentityValue();
        }

        private int GetLastIdentityValue()
        {
            SetSqlFormat("select max(Id) from {0}", SynnDataProvider.TableNames.ShoppingItems);
            ClearParameters();
            return (int)GetSingleRecordFirstValue();
        }
    }
}