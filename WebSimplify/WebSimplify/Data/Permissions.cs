using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public enum ClientPagePermissions
    {
        [PageLink("navdic")]
        [Description("מילון משתמש")]
        Dictionary,
        [PageLink("navdiary")]
        [Description("יומן")]
        Diary,
        [PageLink("navshifts")]
        [Description("משמרות")]
        Shifts,
        [PageLink("navwed")]
        [Description("חתונה")]
        Wedding,
        [PageLink("navshop")]
        [Description("קניות")]
        Shopping,
        [PageLink("navtask")]
        [Description("משימות")]
        QuickTasks,
        [PageLink("navcredit")]
        [Description("אשראי")]
        CreditData,
        [PageLink("navusers")]
        [Description("מנהל משתמשים")]
        SysUsers,
        [PageLink("navmonbal")]
        [Description(@" מעקב הוצאות\הכנסות")]
        MoneyBalance,
        [Description("מנהל משימות פיתוח")]
        SysDev,
        [PageLink("navlog")]
        [Description("יומן מערכת")]
        SysLog,
        [PageLink("navcash")]
        [Description(" מעקב מזומן")]
        CashLog,
        [Description("  מעקב שעות עבודה")]
        WorkHours,
        [PageLink("navlot")]
        [Description("הגרלות לוטו")]
        Lotto,
        [PageLink("navlotro")]
        [Description("טפסי לוטו")]
        LottoRows,
    }

    public class PageLinkAttribute : Attribute
    {
        public List<string> NavLinks { get;  set; }

        public PageLinkAttribute(params string[] linkIs)
        {
            NavLinks = new List<string>();
            if (!linkIs.IsEmpty())
                NavLinks.AddRange(linkIs.ToList());
        }
    }
}