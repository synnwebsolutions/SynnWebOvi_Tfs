using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace WebSimplify.Data
{
    public class CashMoneyItem : IDbLoadable
    {
        public CashMoneyItem()
        {

        }

        public CashMoneyItem(IDataReader data)
        {
            Load(data);
        }

        public bool DummyItem { get; set; }

        public int TotalSpent { get; set; }
        public DateTime Date { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public int UserGroupId { get; set; }
        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            TotalSpent = DataAccessUtility.LoadNullable<int>(reader, "TotalSpent");
            Date = DataAccessUtility.LoadNullable<DateTime>(reader, "Date");
            Description = DataAccessUtility.LoadNullable<string>(reader, "Description");
            UserGroupId = DataAccessUtility.LoadInt32(reader, "UserGroupId");
        }
    }
}