using SynnCore.DataAccess;
using System;
using System.Collections.Generic;

namespace SynnWebOvi
{
    internal class SqlDbWedd : SqlDbController, IDbWedd
    {

        public SqlDbWedd(string _connectionString) : base(new SynnSqlDataProvider(_connectionString))
        {
        }
        public List<WeddingGuest> GetGuests(WeddSearchParameters sp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.WeddingItems);
            ClearParameters();
      

            if (!string.IsNullOrEmpty(sp.SearchText))
            {
                StartORGroup();
                AddORLikeField("GuestName", sp.SearchText, LikeSelectionStyle.CheckBoth);
                EndORGroup();
            }
            var lst = new List<WeddingGuest>();
            FillList(lst, typeof(WeddingGuest));
            return lst;
        }
    }
}