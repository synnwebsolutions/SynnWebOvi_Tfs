using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynnCore.DataAccess
{
    public class DataAccessUtility
    {
        internal static int LoadInt32(DataRow data, string fieldname)
        {
            return Convert.ToInt32(data[fieldname]);
        }

        public static int LoadInt32(IDataReader data, string fieldname)
        {
            return data.GetInt32(data.GetOrdinal(fieldname));
        }

        internal static DateTime LoadDateTime(DataRow data, string fieldname)
        {
            return Convert.ToDateTime(data[fieldname]);
        }

        public static DateTime LoadDateTime(IDataReader data, string fieldname)
        {
            return data.GetDateTime(data.GetOrdinal(fieldname));
        }

        public static T LoadNullable<T>(IDataReader data, string fieldname)
        {
            int ord = data.GetOrdinal(fieldname);
            if (data.IsDBNull(ord))
                return (T)(object)null;
            else
                return (T)(object)data.GetValue(ord);
        }

        public static T LoadNullable<T>(DataRow data, string fieldname)
        {
            object ord = data[fieldname];
            if (ord == null)
                return (T)(object)null;
            else
                return (T)(object)(ord);
        }

        internal static T LoadNullable<T>(object ord)
        {
            if (ord == null || ord.ToString() == string.Empty)
                return (T)(object)null;
            else
                return (T)(object)(ord);
        }
    }
}
