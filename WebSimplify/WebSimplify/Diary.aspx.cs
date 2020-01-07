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

        public List<MemoItem> GetCalendarItems(DateTime? StartDate = null, DateTime? EndDate = null)
        {
            var dbData =  DBController.DbCalendar.Get(new CalendarSearchParameters { FromDate = StartDate.HasValue ? StartDate.Value.Date : DateTime.Now.StartOfMonth().Date,
                ToDate = EndDate.HasValue ? EndDate.Value.Date : DateTime.Now.EndOfMonth().Date });
            return dbData;
        }


        protected void btnadddiary_ServerClick(object sender, EventArgs e)
        {
            if (ValidateInputs(txadddiaryname, txadddiarydesc, txadddiarydate, txadddiaryHour))
            {
                var c = new MemoItem
                {
                    UserId = CurrentUser.Id,
                    CreationDate = DateTime.Now,
                    title = txadddiaryname.Value,
                    Description = txadddiarydesc.Value,
                    Date = Convert.ToDateTime(txadddiarydate.Value).AddHours(txadddiaryHour.GetHours()?? 0).AddMinutes(txadddiaryHour.GetMinutes() ?? 0),
                    RepeatEvery = (RepeatEvery)cmbRepeatEvery.SelectedValue.ToInteger(),
                    Shared = cmbShareVals.SelectedValue.ToInteger() == (int)MemoSharingEnum.YES
                };
                DBController.DbCalendar.Add(c);
                AlertMessage("פעולה זו בוצעה בהצלחה");
                ClearInputs(txadddiaryname, txadddiarydesc, txadddiarydate);
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
                FillEnum(cmbRepeatEvery, typeof(RepeatEvery),false);
                FillEnum(cmbShareVals, typeof(MemoSharingEnum),false);
                btnadddiary.Disabled = CurrentUser.IsAdmin;
            }
        }
    }
}