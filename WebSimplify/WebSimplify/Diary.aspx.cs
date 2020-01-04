using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSimplify.Helpers;
using WebSimplify.Controls;
using CalendarUtilities;

namespace WebSimplify
{
    public partial class Diary : SynnWebFormBase
    {
        protected override List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                var l = new List<ClientPagePermissions> { ClientPagePermissions.Diary };
                return l;
            }
        }
        public DateTime ActionMonth
        {
            get
            {
                return (DateTime)GetFromSession("cMonth");
            }
            set
            {
                StoreInSession("cMonth", value);
            }
        }

        public List<XCalendarItem> GetCalendarItems(DateTime? StartDate = null, DateTime? EndDate = null)
        {
            var d = new List<XCalendarItem>();
            var dbData =  DBController.DbCalendar.Get(new CalendarSearchParameters { FromDate = StartDate.HasValue ? StartDate.Value.Date : DateTime.Now.StartOfMonth().Date,
                ToDate = EndDate.HasValue ? EndDate.Value.Date : DateTime.Now.EndOfMonth().Date }).Select(x => x as ICalendarItem).ToList();
            foreach (var dbitem in dbData)
                d.Add(new XCalendarItem { Date = dbitem.Date, Text = dbitem.Display });
            return d;
        }


        protected void btnadddiary_ServerClick(object sender, EventArgs e)
        {
            if (ValidateInputs(txadddiaryname, txadddiarydesc, txadddiarydate, txadddiaryHour))
            {
                var c = new MemoItem
                {
                    title = txadddiaryname.Value,
                    Description = txadddiarydesc.Value,
                    Date = Convert.ToDateTime(txadddiarydate.Value).AddHours(txadddiaryHour.GetHours()?? 0).AddMinutes(txadddiaryHour.GetMinutes() ?? 0)
                };
                var sp = new CalendarSearchParameters { InsertItem = c };
                DBController.DbCalendar.Add(sp);
                AlertMessage("פעולה זו בוצעה בהצלחה");
                ClearInputs(txadddiaryname, txadddiarydesc, txadddiarydate);
                RefreshView();
            }
            else
            {
                AlertMessage("אחד או יותר מהשדות ריקים");
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            //xCalendar.RefreshView();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ActionMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                //WsCalendar.StartDate = ActionMonth;
                FillEnum(cmbRepeatEvery, typeof(RepeatEvery));
                RefreshView();
            }
        }

        private void RefreshView()
        {
            //WsCalendar.RefreshView();
        }

        List<MemoItem> cm;

        protected void btnSendCalenadr_ServerClick(object sender, EventArgs e)
        {
            var mm = DBController.DbCalendar.Get(new CalendarSearchParameters { FromDate = ActionMonth, ToDate = ActionMonth.AddMonths(1) });
            var mr = GenerateCalendarRequest(mm);
            CalendarEventManager.SendCalendarByMail(mr);
            AlertMessage("פעולה זו בוצעה בהצלחה");
        }

        private CalendarRequest GenerateCalendarRequest(List<MemoItem> mm)
        {
            var prefs = CurrentUser.Preferences.CalendarPrefs;
            return new CalendarRequest
            {
                FromEmail = prefs.SystemName,
                FromName = prefs.SystemEmailAddress,
                HtmlBody = "",
                Subject = prefs.CalendarItemsGenericSubject,
                To = prefs.UserSharingEmails,
                NetworkCredential = new System.Net.NetworkCredential(prefs.SystemName, prefs.SystemEmailPassword),
                CalendarEvents = mm.ToCalendarEvents(),
            };
        }

        protected void btnDownloadCal_ServerClick(object sender, EventArgs e)
        {
            List<MemoItem> mm = DBController.DbCalendar.Get(new CalendarSearchParameters { FromDate = ActionMonth, ToDate = ActionMonth.AddMonths(1) });
            CalendarEventManager.DownloadCalendarFile(HttpContext.Current, GenerateCalendarRequest(mm));
        }
    }
}