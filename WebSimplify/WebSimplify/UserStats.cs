using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class UserStats : GenericData
    {
        public int UserId { get; set; }
        [GenericDataField("UserIdText", "UserId")]
        public string UserIdText
        {
            get { return UserId.ToString(); }
            set { UserId = value.ToInteger(); }
        }

        public DateTime LastLogged { get; set; }
        [GenericDataField("LastLoggedText", "LastLogged")]
        public string LastLoggedText
        {
            get { return LastLogged.ToString(); }
            set { LastLogged = value.ToDateTime(); }
        }

        [GenericDataField("PageName", "PageName")]
        public string PageName { get; set; }

        internal override string FormatedGenericValue(string valueToFormat, GenericDataFieldAttribute genericFieldInfo, IDatabaseProvider db)
        {
            if (genericFieldInfo.PropertyName == "UserIdText")
            {
                if (valueToFormat.IsInteger())
                {
                    var u = db.DbAuth.GetUser(valueToFormat.ToInteger());
                    return u.DisplayName;
                }
            }
            if (genericFieldInfo.PropertyName == "LastLoggedText")
            {
                if (valueToFormat.IsDateTime())
                {
                    var days = (DateTime.Now - valueToFormat.ToDateTime()).Hours;
                    return $" לפני {days} שעות";
                }
            }
            return base.FormatedGenericValue(valueToFormat, genericFieldInfo, db);
        }

        public UserStats(IDataReader data)
        {
            Load(data);
        }

        public UserStats()
        {

        }
    }
}