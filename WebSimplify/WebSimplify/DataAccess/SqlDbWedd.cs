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

        public List<WeddingGuest> GetGuests(string searchText)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.WeddingItems);
            ClearParameters();
            if (!string.IsNullOrEmpty(searchText))
            {
                StartORGroup();
                AddORLikeField("GuestName", searchText, LikeSelectionStyle.CheckBoth);
                EndORGroup();
            }
            var lst = new List<WeddingGuest>();
            FillList(lst, typeof(WeddingGuest));
            return lst;
        }
    }
}