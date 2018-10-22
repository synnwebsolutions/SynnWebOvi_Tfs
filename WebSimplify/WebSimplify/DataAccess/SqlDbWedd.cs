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

        private void SetPermissions(WeddSearchParameters sp)
        {
            if (!sp.CurrentUser.IsAdmin)
            {
                StartORGroup();
                foreach (int gid in sp.CurrentUser.AllowedSharedPermissions)
                    AddOREqualField("UserGroupId", gid);
                EndORGroup();
            }
        }

        public List<WeddingGuest> GetGuests(WeddSearchParameters sp)
        {
            SetSqlFormat("select * from {0}", SynnDataProvider.TableNames.WeddingItems);
            ClearParameters();
            SetPermissions(sp);

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