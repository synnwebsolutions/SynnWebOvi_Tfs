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
        public int CreditCardPaymentDay { get; set; }
        public DateTime CreditLogStartDate { get; set; }
        public DateTime BalanceLogStartDate { get; set; }
        public WorkHoursData CurrentWorkHoursData { get; set; }
        public WorkTime DailyRequiredWorkHours { get; set; }
        public bool UseCharts { get; set; }

        public override void LoadGenericFieldValue(int i, string genericFieldDbValue)
        {
            if (i == 0)
            {
                UserId = genericFieldDbValue.ToInteger();
            }
            if (i == 1)
            {
                CreditCardPaymentDay = genericFieldDbValue.ToInteger();
            }
            if (i == 2)
            {
                CreditLogStartDate = genericFieldDbValue.ToDateTime();
            }
            if (i == 3)
            {
                BalanceLogStartDate = genericFieldDbValue.ToDateTime();
            }
            if (i == 4)
            {
                if (genericFieldDbValue.NotEmpty())
                {
                    CurrentWorkHoursData = genericFieldDbValue.ParseXml<WorkHoursData>();
                }
            }
            if (i == 5)
            {
                if (genericFieldDbValue.NotEmpty())
                {
                    DailyRequiredWorkHours = genericFieldDbValue.ParseXml<WorkTime>();
                }
            }
            if (i == 6)
            {
                UseCharts = genericFieldDbValue.ToBoolean();
            }
            base.LoadGenericFieldValue(i, genericFieldDbValue);
        }
        public override string GetGenericFieldValue(int i, ref bool addEmpty)
        {
            if (i == 0)
            {
                return UserId.ToString();
            }
            if (i == 1)
            {
                return CreditCardPaymentDay.ToString();
            }
            if (i == 2)
            {
                return CreditLogStartDate.ToString();
            }
            if (i == 3)
            {
                return BalanceLogStartDate.ToString();
            }
            if (i == 4)
            {
                return CurrentWorkHoursData.ToXml() ?? new WorkHoursData().ToXml();
            }
            if (i == 5)
            {
                return DailyRequiredWorkHours.ToXml() ?? new WorkTime().ToXml();
            }
            if (i == 6)
            {
                return UseCharts.ToString();
            }
            return base.GetGenericFieldValue(i, ref addEmpty);
        }
    }
}