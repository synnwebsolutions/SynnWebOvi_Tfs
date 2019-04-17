using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    [Serializable]
    public class XCalendarItem
    {
        public DateTime Date { get; set; }
        public string Text { get; set; }
    }
}