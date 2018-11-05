using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify.Data
{
    public class CashMonthlyData : MoneyMonthlyData
    {
        public CashMonthlyData()
        {

        }

        public CashMonthlyData(IDataReader data)
        {
            Load(data);
        }
    }
}