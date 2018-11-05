using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public enum ClientPagePermissions
    {
        [Description("מילון משתמש")]
        Dictionary,
        [Description("יומן")]
        Diary,
        [Description("משמרות")]
        Shifts,
        [Description("חתונה")]
        Wedding,
        [Description("קניות")]
        Shopping,
        [Description("משימות")]
        QuickTasks,
        [Description("אשראי")]
        CreditData,
        [Description("מנהל מערכת")]
        SysAdmin,
        [Description(" מעקב מזומן")]
        CashLog
    }
    
}