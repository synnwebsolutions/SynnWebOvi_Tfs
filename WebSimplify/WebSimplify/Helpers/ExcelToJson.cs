using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace WebSimplify.Helpers
{
    public static class ExcelToJson
    {
        internal static string ParseXLSX(string pathToExcel, string sheetName)
        {

            //This connection string works if you have Office 2007+ installed and your 
            //data is saved in a .xlsx file
            var connectionString = String.Format(@" Provider=Microsoft.ACE.OLEDB.12.0; Data Source={0}; Extended Properties=""Excel 12.0 Xml;HDR=YES"" ", pathToExcel);

            //Creating and opening a data connection to the Excel sheet 
            using (var conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = String.Format(@"SELECT * FROM [{0}$]", sheetName);

                using (var rdr = cmd.ExecuteReader())
                {
                    //LINQ query - when executed will create anonymous objects for each row
                    var query =
                        (from DbDataRecord row in rdr
                         select row).Select(x =>
                         {

                             //dynamic item = new ExpandoObject();
                             Dictionary<string, object> item = new Dictionary<string, object>();
                             for (int i = 0; i < rdr.FieldCount; i++)
                             {
                                 item.Add(rdr.GetName(i), x[i]);
                             }

                             return item;
                         });
                    var json = JsonConvert.SerializeObject(query);
                    return json;
                }
            }
        }
    }
}