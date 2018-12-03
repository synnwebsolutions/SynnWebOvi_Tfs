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

        public List<ICalendarItem> GetCalendarItems(DateTime StartDate, DateTime EndDate)
        {
            return DBController.DbCalendar.Get(new CalendarSearchParameters { FromDate = StartDate.StartOfMonth().Date, ToDate = EndDate.EndOfMonth() }).Select(x => x as ICalendarItem).ToList(); ;
        }


        protected void btnadddiary_ServerClick(object sender, EventArgs e)
        {
            if (ValidateInputs(txadddiaryname, txadddiarydesc, txadddiarydate))
            {
                var c = new MemoItem
                {
                    title = txadddiaryname.Value,
                    Description = txadddiarydesc.Value,
                    Date = Convert.ToDateTime(txadddiarydate.Value)
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ActionMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                WsCalendar.StartDate = ActionMonth;
                RefreshView();
            }
        }

        private void RefreshView()
        {
           
            WsCalendar.RefreshView();
        }

        List<MemoItem> cm;
        
    }
}