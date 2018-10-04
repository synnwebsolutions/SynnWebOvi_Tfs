using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SynnCore.Generics
{
    public class GenericFormatter
    {
        public static string GetEnumDescription(Enum en)
        {
            FieldInfo field = en.GetType().GetField(en.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? en.ToString() : attribute.Description;
        }

        public static string GetHebrewYearName(int year)
        {
            if (year > 1900)
            {
                var dt = new DateTime(year, 1, 1, 4, 5, 6);
                CultureInfo jewishCulture = CultureInfo.CreateSpecificCulture("he-IL");
                jewishCulture.DateTimeFormat.Calendar = new HebrewCalendar();
                return dt.ToString("yyyy", jewishCulture);
            }
            return string.Empty;
        }

        public static string FixIllegalCharacters(string value)
        {
            foreach (char c in illegalChars)
            {
                value = value.Replace(c, ' ');
            }
            return value;
        }

        static char[] illegalChars = { '(', ')', '+', '*', '^', '"', '{', '}', '[', ']', '&', '<', '>', '`', '\'', ',' };

    }
}
