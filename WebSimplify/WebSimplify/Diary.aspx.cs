using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSimplify
{
    public partial class Diary : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static CalendarMonthlyData GetCalendarItem()
        {
            var sp = new CalendarSearchParameters { };
            List<MemoItem>  mls = DBController.DbCalendar.Get(sp);
            return new CalendarMonthlyData(mls);
        }

    }
}