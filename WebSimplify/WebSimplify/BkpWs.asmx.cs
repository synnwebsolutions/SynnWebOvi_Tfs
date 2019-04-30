using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebSimplify;
using WebSimplify.Data;

namespace WebSimplify
{
    /// <summary>
    /// Summary description for BkpWs
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BkpWs : System.Web.Services.WebService
    {

        private const string rmAppCode = "ftrsdrs@3344****";

        
        [WebMethod(EnableSession = true )]
        public DataProtectionContainer GetData(string appId)
        {
            DataProtectionContainer d = null;
            if (appId == rmAppCode)
            {
                var DBController = Global.DBController;
                try
                {

                    d = new DataProtectionContainer
                    {
                        DictionaryItems = DBController.DbUserDictionary.PerformSearch(new DictionarySearchParameters { FromWs = true }),
                        Users = DBController.DbAuth.GetUsers(new UserSearchParameters { FromWs = true }),
                        DevTaskItems = DBController.DbAuth.Get(new DevTaskItemSearchParameters { FromWs = true }),
                        WeddingGuests = DBController.DbWedd.GetGuests(new WeddSearchParameters { FromWs = true }),
                        ShopItems = DBController.DbShop.Get(new ShopSearchParameters { FromWs = true }),
                        Shifts = DBController.DbShifts.GetShifts(new ShiftsSearchParameters { FromWs = true }),
                        CreditCardMonthlyData = DBController.DbMoney.Get(new CreditSearchParameters { FromWs = true }),
                        CashMonthlyData = DBController.DbMoney.Get(new CashSearchParameters { FromWs = true }),
                        CashItems = DBController.DbMoney.GetCashItems(new CashSearchParameters { FromWs = true }),
                        MonthlyMoneyTransactions = DBController.DbMoney.GetMoneyTransactions(new MonthlyMoneyTransactionSearchParameters { FromWs = true }),
                        MoneyTransactionTemplates = DBController.DbMoney.GetMoneyTransactionTemplates(new MonthlyMoneyTransactionSearchParameters { FromWs = true }),
                        DiaryItems = DBController.DbCalendar.Get(new CalendarSearchParameters { FromWs = true }),
                        QuickTasks = DBController.DbCalendar.Get(new QuickTasksSearchParameters { FromWs = true }),
                        MigrationFinishedSteps = DBController.DbMigration.GetAlreadyFinishedSteps(),
                        PermissionGroups = DBController.DbAuth.GetPermissionGroup(),
                        LottoRows = DBController.DbLotto.Get(new LottoRowsSearchParameters { FromWs = true }),
                        LottoPoles = DBController.DbLotto.Get(new LottoPolesSearchParameters { FromWs = true })
                    };
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
            }
            return d;
        }
    }

    [Serializable]
    public class DataProtectionContainer
    {
        public List<CashMoneyItem> CashItems { get; set; }
        public List<CashMonthlyData> CashMonthlyData { get; set; }
        public List<CreditCardMonthlyData> CreditCardMonthlyData { get; set; }
        public List<DevTaskItem> DevTaskItems { get; set; }
        public List<MemoItem> DiaryItems { get; internal set; }
        public List<DictionaryItem> DictionaryItems { get; set; }
        public List<LottoPole> LottoPoles { get; internal set; }
        public List<LottoRow> LottoRows { get; internal set; }
        public List<string> MigrationFinishedSteps { get;  set; }
        public List<MoneyTransactionTemplate> MoneyTransactionTemplates { get;  set; }
        public List<MonthlyMoneyTransaction> MonthlyMoneyTransactions { get;  set; }
        public List<PermissionGroup> PermissionGroups { get;  set; }
        public List<QuickTask> QuickTasks { get;  set; }
        public List<ShiftDayData> Shifts { get; set; }
        public List<ShopItem> ShopItems { get; set; }
        public List<LoggedUser> Users { get; set; }
        public List<WeddingGuest> WeddingGuests { get; set; }
    }
}
