using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSimplify;
using WebSimplify.Data;

namespace SynnWebOvi
{
    public class IDb
    {
    }

    public interface IDbAuth
    {
        bool ValidateUserCredentials(string userName, string passwword);
        LoggedUser LoadUserSettings(string userName, string passwword);
    }

    public interface IDbLog
    {
        string AddLog(Exception ex);
        void AddLog(string message);
        List<LogItem> GetLogs(LogSearchParameters lsp);
    }

    public interface IDbWedd
    {
        List<WeddingGuest> GetGuests(string searchText);
    }

    public interface IDbShop
    {
        ShoppingData GetData();
        void Update(ShoppingData sd);
    }

    public interface IDbCalendar
    {
        void Add(CalendarSearchParameters sp);
        List<MemoItem> Get(CalendarSearchParameters sp);
    }

    public interface IDbUserDictionary
    {
        void Add(DictionarySearchParameters p);
        List<DictionaryItem> PerformSearch(DictionarySearchParameters p);
    }

    public class BaseSearchParameters
    {
        public LoggedUser CurrentUser { get; set; }
        public int UserGroupId { get; set; }
        public BaseSearchParameters()
        {
            CurrentUser = AccountData.GetCurrentUser();
            UserGroupId = CurrentUser.UserGroupId;
        }
    }
    public class LogSearchParameters: BaseSearchParameters
    {
        public LogSearchParameters() : base()
        {
        }

        public string Text { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class DictionarySearchParameters : BaseSearchParameters
    {
        public DictionarySearchParameters() : base()
        {
        }

        public string Key { get; set; }
        public string Value { get; set; }
        public string SearchText { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class CalendarSearchParameters : BaseSearchParameters
    {
        public CalendarSearchParameters() : base()
        {
        }

        public DateTime? FromDate { get;  set; }
        public DateTime? ToDate { get; set; }
        
        internal MemoItem InsertItem { get; set; }
    }

}