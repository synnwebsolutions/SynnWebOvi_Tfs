using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class MoneyMonthlyData : IDbLoadable 
    {
        public MoneyMonthlyData()
        {

        }

        public MoneyMonthlyData(IDataReader data)
        {
            Load(data);
        }

        public int TotalSpent { get; set; }
        public DateTime Date { get; set; }
        public int Id { get; set; }
        public bool Active { get; set; }

        public string DaylyValue
        {
            get
            {
                var res = TotalSpent / (Active? DateTime.Now.Day : Date.NumberOfDays());
                return res.FormattedString();
            }
        }
        public string MonthlyPrediction
        {
            get
            {
                var res = Convert.ToInt32(DaylyValue) * Date.NumberOfDays();
                return res.FormattedString();
            }
        }


        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            TotalSpent = DataAccessUtility.LoadNullable<int>(reader, "TotalSpent");
            Date = DataAccessUtility.LoadNullable<DateTime>(reader, "Date");
            Active = DataAccessUtility.LoadNullable<bool>(reader, "Active");
        }
    }
}