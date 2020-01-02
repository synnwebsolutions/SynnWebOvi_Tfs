using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace WebSimplify
{
    public static  class HtmlExtensions
    {
        public static double? GetHours(this HtmlInputGenericControl htmlInput)
        {
            if (htmlInput.Value.NotEmpty())
            {
                var vals = htmlInput.Value.Split(':').Where(x => x.Length > 0).ToList();
                if (vals.Count == 2)
                    return vals[0].ToInteger();
            }
            return null;
        }

        public static double? GetMinutes(this HtmlInputGenericControl htmlInput)
        {
            if (htmlInput.Value.NotEmpty())
            {
                var vals = htmlInput.Value.Split(':').Where(x => x.Length > 0).ToList();
                if (vals.Count == 2)
                    return vals[1].ToInteger();
            }
            return null;
        }
    }
}