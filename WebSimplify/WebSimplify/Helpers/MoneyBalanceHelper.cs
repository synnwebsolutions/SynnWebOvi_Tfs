using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SynnWebOvi;

namespace WebSimplify
{
    public static class MoneyBalanceHelper
    {
        internal static IDatabaseProvider DBController = SynnDataProvider.DbProvider;
        internal static List<MoneyTransactionTemplate> GetCurrentMonthOpenTemplates(MonthlyMoneyTransactionSearchParameters mp)
        {
            return DBController.DbMoney.GetMoneyTransactionTemplate(mp);
        }
    }
}