using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace xWebControls.Calendar
{
    public static class xCalendarExtensions
    {
        public static DateTime StartOfTheMonth(this DateTime d)
        {
            return new DateTime(d.Year, d.Month, 1);
        }

        public static bool IsToday(this DateTime d)
        {
            return d.Date == DateTime.Now.Date;
        }

        public static bool IsSameMonth(this DateTime d, int month)
        {
            return d.Month == month;
        }

        private static System.Globalization.CultureInfo hebCulture = new System.Globalization.CultureInfo("he-IL");
        public static DateTime StartOfWeek(this DateTime d)
        {
            while (d.DayOfWeek > DayOfWeek.Sunday)
            {
                d = d.AddDays(-1);
            }
            return d;
        }

        public static string HebrewDay(this DateTime d)
        {
            var day = hebCulture.DateTimeFormat.GetDayName(d.DayOfWeek);
            return day;
        }

        public static string HebrewDay(this DayOfWeek d)
        {
            var day = hebCulture.DateTimeFormat.GetDayName(d);
            return day;
        }


        public static string HebrewDayWithNumber(this DateTime d)
        {
            return string.Format("{0} {1}", d.HebrewDay(), d.Day.ToString());
        }

        public static void AddWeekDay(this TableRow r, Table innerTable)
        {
            var c = new TableCell();
            c.Controls.Add(innerTable);
            r.Controls.Add(c);
        }

        public static void SetStyles(this Table t, string cssDayItemHeaderClass, string cssDayItemClass)
        {
            foreach (TableRow row in t.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    if (cell is TableHeaderCell)
                        cell.CssClass = cssDayItemHeaderClass;
                    else
                        cell.CssClass = cssDayItemClass;
                }
            }
        }

        public static TableRow AddRowHeaderItem(this Table t, TableHeaderCell c)
        {
            var r = new TableRow();
            return AddCell(t, c, r);
        }

        public static TableRow AddRowCellItem(this Table t, TableCell c)
        {
            var r = new TableRow();
            return AddCell(t, c, r);
        }

        private static TableRow AddCell(Table t, TableCell c, TableRow r)
        {
            r.Cells.Add(c);
            t.Rows.Add(r);
            return r;
        }

        //public static TableRow AddRowItem(this Table t, TableRow r, string text, bool header = false)
        //{
        //    r.Cells.Add(header ? new TableHeaderCell() { Text = text, CssClass = Consts.cssDayItemHeaderClass } : new TableCell() { Text = text, CssClass = Consts.cssDayItemClass });
        //    t.Rows.Add(r);
        //    return r;
        //}

        public static bool IsEmptyOrNull(this IList lst)
        {
            return lst == null || lst.Count == 0;
        }

        public static bool NotEmpty(this IList lst)
        {
            return lst != null && lst.Count > 0;
        }

        public static bool NotEmpty(this IDictionary lst)
        {
            return lst != null && lst.Count > 0;
        }
    }
}
