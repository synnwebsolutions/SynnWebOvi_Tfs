using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebSimplify
{
    public class xCalendarDay
    {
        #region Ctors

        public xCalendarDay(DateTime date)
        {
            Date = date;
        }

        public xCalendarDay()
        {

        }

        #endregion

        public List<XCalendarItem> DateInfos { get; set; }
        public DateTime Date { get; set; }

        public Table GetDay()
        {
            var t = new Table();
            var r = t.AddRowHeaderItem(new TableHeaderCell() { Text = Date.Day.ToString() });

            if (DateInfos.NotEmpty())
                foreach (var di in DateInfos)
                    t.AddRowCellItem(new TableCell { Text = di.Text });
            return t;
        }
    }

}