using SynnWebOvi;
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
        [GenericDataField("UsersToShareText", "UsersToShare")]
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

        internal override string FormatedGenericValue(string valueToFormat, GenericDataFieldAttribute genericFieldInfo, IDatabaseProvider db)
        {
            if (genericFieldInfo.PropertyName == "UsersToShareText")
            {
                var usersToShare = valueToFormat.ParseXml<List<int>>();
                var users = db.DbAuth.GetUsers(new UserSearchParameters { Ids = usersToShare });
                return string.Join(",", users.Select(x => x.DisplayName).ToList());
            }
            if (genericFieldInfo.PropertyName == "OwnerUserIdText")
            {
                if (valueToFormat.IsInteger())
                {
                    var u = db.DbAuth.GetUser(valueToFormat.ToInteger());
                    return u.DisplayName;
                }
            }
            return base.FormatedGenericValue(valueToFormat, genericFieldInfo, db);
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