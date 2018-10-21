using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    [Serializable]
    public class ShoppingData
    {
        public List<ShopItem> CurrentToBuy { get; set; }
        public List<ShopItem> AllShopItems { get; set; }

        public ShoppingData()
        {
            CurrentToBuy = new List<ShopItem>();
            AllShopItems = new List<ShopItem>();
        }

        internal void AddToShoplist(string productName)
        {
            var cus = AllShopItems.FirstOrDefault(x => x.Name == productName);
            if (cus == null)
            {
                cus = new ShopItem { Name = productName };
                if(AllShopItems.Any(x => x.Name == productName))
                    AllShopItems.Add(cus);
            }
            if (CurrentToBuy.Any(x => x.Name == productName))
                CurrentToBuy.Add(cus);
        }
    }

    [Serializable]
    public class ShopItem
    {
        public string Name { get; set; }
        public DateTime LastBought { get; set; }

        public ShopItem()
        {

        }
    }
}