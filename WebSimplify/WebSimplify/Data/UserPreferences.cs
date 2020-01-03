using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SynnCore.Generics;
using CalendarUtilities;

namespace WebSimplify.Data
{
    public class UserAppPreferencesContainer : GenericData
    {
        public override GenericDataEnum GenericDataType
        {
            get
            {
                return GenericDataEnum.UserAppPreferences;
            }
        }

        public UserAppPreferences Value { get; set; }
        public UserAppPreferencesContainer()
        {
        }
        public UserAppPreferencesContainer(IDataReader data)
        {
            Load(data);
        }

        public override void LoadExtraFields(IDataReader reader)
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
        public DateTime BalanceLogStartDate { get; set; }
        public WorkHoursData CurrentWorkHoursData { get; set; }
        public WorkTime DailyRequiredWorkHours { get; set; }
        public bool UseCharts { get;  set; }

        public CalendarPreferences CalendarPrefs { get; set; }
    }

    [Serializable]
    public class CalendarPreferences
    {
        public string CalendarItemsGenericSubject { get;  set; }
        public string SystemName { get;  set; }
        public string SystemEmailAddress { get;  set; }
        public string SystemEmailPassword { get;  set; }
        public List<string> UserSharingEmails { get;  set; }
        public List<MyCalendarAlarm> Alarms { get;  set; }
    }
}