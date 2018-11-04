using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace WebSimplify
{
    [Serializable]
    public class ShopItem : IDbLoadable 
    {
        public string Name { get; set; }
        public DateTime? LastBought { get; set; }
        public int Id { get;  set; }
        public int? CategoryId { get;  set; }

        public ShopItem()
        {

        }

        public ShopItem(IDataReader data)
        {
            Load(data);
        }

        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            CategoryId = DataAccessUtility.LoadNullable<int?>(reader, "CategoryId");
            LastBought = DataAccessUtility.LoadNullable<DateTime?>(reader, "LastBought");
            Name = DataAccessUtility.LoadNullable<string>(reader, "Name");
        }
    }
}