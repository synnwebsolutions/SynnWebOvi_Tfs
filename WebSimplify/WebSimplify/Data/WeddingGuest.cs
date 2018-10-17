using SynnCore.DataAccess;
using System.Data;

namespace SynnWebOvi
{
    public class WeddingGuest : IDbLoadable
    {
        public WeddingGuest()
        {

        }
        public WeddingGuest(IDataReader data)
        {
            Load(data);
        }

        public int Id { get; set; }
        public int Amount { get; set; }
        public int UserGroupId { get; set; }
        public string Name { get; set; }

        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            Amount = DataAccessUtility.LoadInt32(reader, "Payment");
            Name = DataAccessUtility.LoadNullable<string>(reader, "GuestName");
            UserGroupId = DataAccessUtility.LoadInt32(reader, "UserGroupId");
        }
    }
}