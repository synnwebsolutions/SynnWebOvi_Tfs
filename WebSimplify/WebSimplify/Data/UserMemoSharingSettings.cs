using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class UserMemoSharingSettings : GenericData
    {
        public int OwnerUserId { get; set; }
        [GenericDataField("OwnerUserIdText", "OwnerUserId")]
        public string OwnerUserIdText
        {
            get { return OwnerUserId.ToString(); }
            set { OwnerUserId = value.ToInteger(); }
        }

        public List<int> UsersToShare { get; set; }
        [GenericDataField("UsersToShareText", "UsersToShare", DisableGridEdit = true)]
        public string UsersToShareText
        {
            get
            {
                if (UsersToShare.IsEmpty())
                    UsersToShare = new List<int>();
                return UsersToShare.ToXml();
            }
            set
            {
                UsersToShare = value.ParseXml<List<int>>();
            }
        }

        public UserMemoSharingSettings(IDataReader data)
        {
            Load(data);
        }

        public UserMemoSharingSettings()
        {

        }
    }
}