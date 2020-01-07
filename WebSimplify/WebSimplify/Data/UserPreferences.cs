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

        public UserAppPreferences()
        {
        }
        public UserAppPreferences(IDataReader data)
        {
            Load(data);
        }

        public int UserId { get; set; }
        [GenericDataField("UserIdText", "UserId")]
        public string UserIdText
        {
            get { return UserId.ToString(); }
            set { UserId = value.ToInteger(); }
        }

        public int CreditCardPaymentDay { get; set; }
        [GenericDataField("CreditCardPaymentDayText", "CreditCardPaymentDay")]
        public string CreditCardPaymentDayText
        {
            get { return CreditCardPaymentDay.ToString(); }
            set { CreditCardPaymentDay = value.ToInteger(); }
        }

        public DateTime CreditLogStartDate { get; set; }
        [GenericDataField("CreditLogStartDateText", "CreditLogStartDate")]
        public string CreditLogStartDateText
        {
            get { return CreditLogStartDate.ToString(); }
            set { CreditLogStartDate = value.ToDateTime(); }
        }

        public DateTime BalanceLogStartDate { get; set; }
        [GenericDataField("BalanceLogStartDateText", "BalanceLogStartDate")]
        public string BalanceLogStartDateText
        {
            get { return BalanceLogStartDate.ToString(); }
            set { BalanceLogStartDate = value.ToDateTime(); }
        }

        public WorkHoursData CurrentWorkHoursData { get; set; }
        [GenericDataField("CurrentWorkHoursDataText", "CurrentWorkHoursData")]
        public string CurrentWorkHoursDataText
        {
            get { return CurrentWorkHoursData.ToXml() ?? new WorkHoursData().ToXml(); }
            set { CurrentWorkHoursData = value.ParseXml<WorkHoursData>();  }
        }

        public WorkTime DailyRequiredWorkHours { get; set; }
        [GenericDataField("DailyRequiredWorkHoursText", "DailyRequiredWorkHours")]
        public string DailyRequiredWorkHoursText
        {
            get { return DailyRequiredWorkHours.ToXml() ?? new WorkTime().ToXml(); }
            set { DailyRequiredWorkHours = value.ParseXml<WorkTime>(); }
        }

        public bool UseCharts { get; set; }
        [GenericDataField("UseChartsText", "UseCharts")]
        public string UseChartsText
        {
            get { return UseCharts.ToString(); }
            set { UseCharts = value.ToBoolean(); }
        }
    }
}