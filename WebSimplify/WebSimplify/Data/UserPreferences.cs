using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SynnCore.Generics;
using CalendarUtilities;
using System.Reflection;

namespace WebSimplify.Data
{
    [Serializable]
    public class UserAppPreferences : GenericData
    {
        public override GenericDataEnum GenericDataType
        {
            get
            {
                return GenericDataEnum.UserAppPreferences;
            }
        }

        public UserAppPreferences()
        {
        }
        public UserAppPreferences(IDataReader data)
        {
            Load(data);
        }

        public int UserId { get; set; }
        public int CreditCardPaymentDay { get;  set; }
        public DateTime CreditLogStartDate { get;  set; }
        public DateTime BalanceLogStartDate { get; set; }
        public WorkHoursData CurrentWorkHoursData { get; set; }
        public WorkTime DailyRequiredWorkHours { get; set; }
        public bool UseCharts { get;  set; }

        public CalendarPreferences CalendarPrefs { get; set; }


        public override void AppendExtraFieldsValues(List<KeyValuePair<int, object>> extraFields)
        {
            extraFields.Add(new KeyValuePair<int, object>(0, UserId.ToString()));
            extraFields.Add(new KeyValuePair<int, object>(1, CreditCardPaymentDay));
            extraFields.Add(new KeyValuePair<int, object>(2, CreditLogStartDate));
            extraFields.Add(new KeyValuePair<int, object>(3, BalanceLogStartDate));
            extraFields.Add(new KeyValuePair<int, object>(4, CurrentWorkHoursData.ToXml() ?? new WorkHoursData().ToXml()));
            extraFields.Add(new KeyValuePair<int, object>(5, DailyRequiredWorkHours.ToXml() ?? new WorkTime().ToXml()));
            extraFields.Add(new KeyValuePair<int, object>(6, UseCharts));
            extraFields.Add(new KeyValuePair<int, object>(7, CalendarPrefs.ToXml() ?? new CalendarPreferences().ToXml()));
        }

        public override void LoadExtraFields(IDataReader reader)
        {
            UserId = DataAccessUtility.LoadNullable<string>(reader, 0.ApplyGenericDataPrefix()).ToInteger();
            CreditCardPaymentDay = DataAccessUtility.LoadNullable<string>(reader, 1.ApplyGenericDataPrefix()).ToInteger();
            CreditLogStartDate = DataAccessUtility.LoadNullable<string>(reader, 2.ApplyGenericDataPrefix()).ToDateTime();
            BalanceLogStartDate = DataAccessUtility.LoadNullable<string>(reader, 3.ApplyGenericDataPrefix()).ToDateTime();

            reader.SetFromDbXmlField<WorkHoursData>(this,4.ApplyGenericDataPrefix(), this.GetPropertyInfo("CurrentWorkHoursData"));
            reader.SetFromDbXmlField<WorkTime>(this, 5.ApplyGenericDataPrefix(), this.GetPropertyInfo("DailyRequiredWorkHours"));

            var res = DataAccessUtility.LoadNullable<string>(reader, 6.ApplyGenericDataPrefix()).ToInteger();
            UseCharts = Convert.ToBoolean(res);
            reader.SetFromDbXmlField<CalendarPreferences>(this, 7.ApplyGenericDataPrefix(), this.GetPropertyInfo("CalendarPrefs"));

        }
    }

    [Serializable]
    public class CalendarPreferences
    {
        public string CalendarItemsGenericSubject { get;  set; }
        public string SystemName { get;  set; }
        public string SystemEmailAddress { get;  set; }
        public string SystemEmailPassword { get;  set; }
        public List<string> UserSharingEmails { get;  set; }
    }
}