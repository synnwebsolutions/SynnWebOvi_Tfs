using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class UserMemoSharingSettings : GenericData
    {
        public override GenericDataEnum GenericDataType
        {
            get
            {
                return GenericDataEnum.UserMemoSharingSettings;
            }
        }

        public int OwnerUserId { get; set; }
        public List<int> UsersToShare { get; set; }
        public override string GetGenericFieldValue(int i, ref bool addEmpty)
        {
            if (i == 0)
            {
                return OwnerUserId.ToString();
            }
            if (i == 1)
            {
                if (UsersToShare.IsEmpty())
                    UsersToShare = new List<int>();
                return UsersToShare.ToXml();
            }
            return base.GetGenericFieldValue(i, ref addEmpty);
        }

        public override void LoadGenericFieldValue(int i, string genericFieldDbValue)
        {
            if (i == 0)
            {
                OwnerUserId = genericFieldDbValue.ToInteger();
            }
            if (i == 1)
            {
                UsersToShare = genericFieldDbValue.ParseXml<List<int>>();
            }
            base.LoadGenericFieldValue(i, genericFieldDbValue);
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