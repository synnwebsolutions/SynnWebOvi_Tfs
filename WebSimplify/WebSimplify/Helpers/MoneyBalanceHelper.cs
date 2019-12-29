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
        internal static List<MonthlyMoneyTransaction> GetOpenTrans(MonthlyMoneyTransactionSearchParameters mp)
        {
            mp.Closed = false;
            return DBController.DbMoney.GetMoneyTransactions(mp);
        }

        internal static void RefreshUserData(LoggedUser u)
        {
            var mp = new MonthlyMoneyTransactionSearchParameters();
            mp.Closed = true;
            List<MoneyTransactionTemplate> tmpls = DBController.DbMoney.GetMoneyTransactionTemplates(mp);
            foreach (var tmpl in tmpls)
            {
                DateTime startdate = tmpl.FromDate.StartOfMonth();
                if (tmpl.ToDate.HasValue && tmpl.ToDate < DateTime.Now)
                {
                    tmpl.Active = false;
                    DBController.DbMoney.Update(tmpl);
                }
                else
                {
                    while (startdate < DateTime.Now.StartOfMonth())
                    {
                        var tp = new MonthlyMoneyTransactionSearchParameters();
                        tp.TemplateId = tmpl.Id;
                        tp.Month = startdate;
                        MonthlyMoneyTransaction trnForMonth = DBController.DbMoney.GetTransaction(tp);
                        if (trnForMonth == null)
                        {
                            trnForMonth = new MonthlyMoneyTransaction
                            {
                                Amount = tmpl.Amount,
                                Month = startdate,
                                TemplateId = tmpl.Id,
                                Closed = tmpl.Auto
                            };
                            DBController.DbMoney.Add(trnForMonth);
                        }
                        startdate = startdate.AddMonths(1);
                    }
                }
            }
        }

        internal static List<MonthBalanceItem> GetBalances(MonthlyMoneyTransactionSearchParameters mp)
        {
            List<MonthBalanceItem> lst = new List<MonthBalanceItem>();
            if (!mp.CurrentUser.IsAdmin && mp.CurrentUser.Allowed(ClientPagePermissions.MoneyBalance))
            {
                var toDate = mp.CurrentUser.Preferences.BalanceLogStartDate;
                var curent = DateTime.Now.StartOfMonth();
                if (toDate != DateTime.MinValue)
                {
                    while (curent.StartOfMonth() >= toDate)
                    {
                        var d = new MonthBalanceItem
                        {
                            Month = curent,
                            Active = curent == DateTime.Now.StartOfMonth()
                        };
                        mp.Closed = true;
                        mp.Month = curent;
                        mp.TranType = MonthlyTransactionType.Debit;
                        List<MonthlyMoneyTransaction> monthdebtItems = DBController.DbMoney.GetMoneyTransactions(mp);
                        d.TotalExpenses = monthdebtItems.Sum(x => x.Amount);

                        mp.TranType = MonthlyTransactionType.Credit;
                        List<MonthlyMoneyTransaction> monthincItems = DBController.DbMoney.GetMoneyTransactions(mp);
                        d.TotalIncomes = monthincItems.Sum(x => x.Amount);

                        d.Balance = d.TotalIncomes - d.TotalExpenses;
                        lst.Add(d);
                        curent = curent.AddMonths(-1);
                    }
                }
            }
            return lst;
        }
    }
}