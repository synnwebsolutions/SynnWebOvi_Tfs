﻿using System;
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
        List<PermissionGroup> GetPermissionGroup();
        void Add(PermissionGroup g);
        void Add(LoggedUser u);
        List<LoggedUser> GetUsers(UserSearchParameters lp);
    }

    public interface IDbLog
    {
        string AddLog(Exception ex);
        void AddLog(string message);
        List<LogItem> GetLogs(LogSearchParameters lsp);
    }

    public interface IDbWedd
    {
        List<WeddingGuest> GetGuests(WeddSearchParameters sp);
    }

    public interface IDbShop
    {
        ShoppingData GetData(ShopSearchParameters sp);
        void Update(ShopSearchParameters sp);
    }

    public interface IDbShifts
    {
        UserShiftsContainer GetShiftsData(ShiftsSearchParameters shiftsSearchParameters);
        void Save(ShiftsSearchParameters sp);
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

        public bool RequirePrivateKeyOnly { get; set; }
        public BaseSearchParameters()
        {
            CurrentUser = AccountData.GetCurrentUser();
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

    public class ShiftsSearchParameters : BaseSearchParameters
    {
        public ShiftsSearchParameters() : base()
        {
        }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public UserShiftsContainer ItemForAction { get; set; }
    }

    public class ShopSearchParameters : BaseSearchParameters
    {
        public ShopSearchParameters() : base()
        {
        }

        public ShoppingData ItemForAction { get; set; }
    }

    public class WeddSearchParameters : BaseSearchParameters
    {
        public WeddSearchParameters() : base()
        {
        }

        public string SearchText { get; set; }
    }

    public class UserSearchParameters : BaseSearchParameters
    {
        public UserSearchParameters() : base()
        {
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public int? Id { get; set; }
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