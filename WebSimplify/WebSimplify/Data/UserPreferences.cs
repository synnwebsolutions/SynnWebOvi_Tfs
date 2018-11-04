using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SynnCore.Generics;

namespace WebSimplify.Data
{
    public class UserAppPreferencesContainer : IDbLoadable
    {
        public UserAppPreferences Value { get; set; }
        public UserAppPreferencesContainer()
        {
        }
        public UserAppPreferencesContainer(IDataReader data)
        {
            Load(data);
        }
        public void Load(IDataReader reader)
        {
            var xmlData = DataAccessUtility.LoadNullable<string>(reader, "pdata");
            if (xmlData.NotEmpty())
                Value = XmlHelper.CreateFromXml<UserAppPreferences>(xmlData);
            else
                Value = new UserAppPreferences();
        }
    }

    [Serializable]
    public class UserAppPreferences
    {
        public int CreditCardPaymentDay { get;  set; }
        public DateTime CreditLogStartDate { get;  set; }
    }
}