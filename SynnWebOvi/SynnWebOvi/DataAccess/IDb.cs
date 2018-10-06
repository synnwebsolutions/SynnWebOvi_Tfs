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
    }

    public interface IDbUserDictionary
    {
        void Add(string key, string value);
        List<DictionaryItem> PerformSearch(string searchText);
    }

}