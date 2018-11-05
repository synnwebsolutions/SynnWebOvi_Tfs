using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class CreditCardMonthlyData : MoneyMonthlyData
    {
        public CreditCardMonthlyData()
        {

        }

        public CreditCardMonthlyData(IDataReader data)
        {
            Load(data);
        }
        
    }
}