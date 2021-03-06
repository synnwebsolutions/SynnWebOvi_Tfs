﻿using CalendarUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;

namespace WebSimplify
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

        public static List<DateTime> DaysInMonth(this DateTime d)
        {
            var ld = new List<DateTime>();
            for (int i = 1; i <= d.NumberOfDays(); i++)
                ld.Add(new DateTime(d.Year, d.Month, i));
            return ld;
        }

        //public static int NumberOfWeeksInMonth(this DateTime d)
        //{
        //    var intNum = d.NumberOfDays() / 7;
        //    decimal decNum =  decimal.Divide(d.NumberOfDays(), 7);
        //    int modval = d.NumberOfDays() % 7;
        //    return d.DaysInMonth().Count(x => x.DayOfWeek == DayOfWeek.Sunday);
        //}

        public static bool IsSameMonth(this DateTime d, int month)
        {
            return d.Month == month;
        }

        private static System.Globalization.CultureInfo hebCulture = new System.Globalization.CultureInfo("he-IL");
        //public static DateTime StartOfWeek(this DateTime d)
        //{
        //    while (d.DayOfWeek > DayOfWeek.Sunday)
        //    {
        //        d = d.AddDays(-1);
        //    }
        //    return d;
        //}

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

        public static DataTable ToDataTable(this IList items)
        {
            if (!items.NotEmpty())
                return new DataTable();
            var typee = items[0].GetType();
            var tb = new DataTable(typee.Name);

            PropertyInfo[] props = typee.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => !(x.PropertyType.IsGenericType &&
        x.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))).ToArray();

            foreach (var prop in props)
            {
                tb.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        public static bool NotEmpty(this IDictionary lst)
        {
            return lst != null && lst.Count > 0;
        }

        public static List<MyCalendarEvent> ToCalendarEvents(this List<MemoItem> lst)
        {
            var li = new List<MyCalendarEvent>();
            foreach (var item in lst)
                li.Add(item.ToCalendarEvent());
            return li;
        }

        public static MyCalendarEvent ToCalendarEvent(this MemoItem memo)
        {
            return new MyCalendarEvent
            {
                BeginDate = memo.Date,
                Details = memo.Description,
                EndDate = memo.Date.AddHours(1),
                SummaryText = memo.title,
                LocationText = memo.Description
            };
        }
    }

}