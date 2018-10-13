using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

    public interface IDbUserDictionary
    {
        void Add(string key, string value);
        List<DictionaryItem> PerformSearch(string searchText);
    }

    public class LogSearchParameters
    {
        public string Text { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

}