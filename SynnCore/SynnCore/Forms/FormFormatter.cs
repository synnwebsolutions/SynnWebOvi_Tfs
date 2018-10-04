using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynnCore.Forms
{
    public class FormFormatter
    {
        public static string DecimalCurrency = "c";
        public static string DecimalNumberFormat = "#,##0.##;;#";
        internal static bool ValidateString(string input, ref string output)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            output = input;
            return true;
        }

        public static bool ValidateDecimal(string input, ref decimal output)
        {
            return decimal.TryParse(input, out output);
        }

        //public static string GetHtmlStringFromHtmlControl(HtmlControl ctr)
        //{
        //    using (StringWriter writer = new StringWriter())
        //    using (Html32TextWriter htmlWriter = new Html32TextWriter(writer))
        //    {
        //        ctr.RenderControl(htmlWriter);
        //        return writer.ToString();
        //    }
        //}

        //internal static void SetSelectItem(ComboBox cmb)
        //{
        //    cmb.Items.Add(new ListItem { Text = "בחר", Value = selectDefaultValue });
        //}

        static string selectDefaultValue = "-1";
        //internal static bool ValidateCombo(ComboBox cmb, ref int value)
        //{
        //    ListItem li = (ListItem)cmb.SelectedItem;
        //    if (li != null && li.Value != selectDefaultValue)
        //    {
        //        value = Convert.ToInt32(li.Value);
        //        return true;
        //    }
        //    return false;
        //}

        public static bool ValidateInteger(string text, ref int value)
        {
            return text != null && int.TryParse(text, out value);
        }

        public static bool ValidateDateTime(string text, ref DateTime dt)
        {
            return text != null && DateTime.TryParse(text, out dt);
        }

        //internal static bool ValidateCombo(DropDownList cmb, ref int value)
        //{
        //    return cmb.SelectedIndex > 0 && int.TryParse(cmb.SelectedValue, out value);
        //}

        public static int RoundDecimal(decimal val)
        {
            bool isNegativ = false;
            if (val < 0)
            {
                val *= -1;
                isNegativ = true;
            }
            int del = 1;
            int numdec = Convert.ToInt32(val).ToString().Length;
            for (int i = 0; i < numdec; i++)
            {
                del *= 10;
            }
            if (isNegativ)
            {
                val *= -1;
            }
            return Convert.ToInt32(val / del) * del;
        }

    }
}
