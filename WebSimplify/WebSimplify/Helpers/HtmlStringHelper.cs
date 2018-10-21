using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace WebSimplify.Helpers
{
    public class HtmlStringHelper
    {
        public static string LineBreak = "<br>";
        public static string GenerateHtmlText(List<string> text)
        {
            StringWriter stringWriter = new StringWriter();
            using (HtmlTextWriter wr = new HtmlTextWriter(stringWriter))
            {
                wr.RenderBeginTag(HtmlTextWriterTag.Table);
                foreach (var str in text)
                    AppendTableRow(wr, str);

                wr.RenderEndTag();//close table
            }
            return stringWriter.ToString();
        }

        private static void AppendTableRow(HtmlTextWriter wr, string text)
        {
            wr.RenderBeginTag(HtmlTextWriterTag.Tr);
            AppendTableCell(wr, text);
            wr.RenderEndTag();
        }

        private static void AppendTableCell(HtmlTextWriter wr, string text)
        {
            wr.RenderBeginTag(HtmlTextWriterTag.Td);
            wr.Write(text);
            wr.RenderEndTag();
        }
    }
}