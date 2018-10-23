using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using SynnWebOvi;

namespace WebSimplify.Data
{

    public static class UiManager
    {
        internal static IDatabaseProvider DBController = SynnDataProvider.DbProvider;

        private static string MainCss_path = @"css/style1.css";
        private static string CssResponsive_path = @"css/sStyle1resp.css";
        private static string JqueryScript_path = @"js/synnJavaScripts.js";
        internal static void Apply(XuiFile xi, string newData)
        {
            ThemeLog tl = new ThemeLog();
            tl.Date = DateTime.Now;
            tl.XiFile = xi;
            tl.NewText = newData;
            var currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            tl.DestinationPath = string.Empty;

            switch (xi)
            {
                case XuiFile.MainCss:
                    tl.DestinationPath = Path.Combine(currentDirectory, MainCss_path);
                    break;
                case XuiFile.CssResponsive:
                    tl.DestinationPath = Path.Combine(currentDirectory, CssResponsive_path);
                    break;
                case XuiFile.BgImage:
                    break;
                case XuiFile.JqueryScript:
                    tl.DestinationPath = Path.Combine(currentDirectory, JqueryScript_path);
                    break;
                default:
                    break;
            }
            tl.PrevText = File.ReadAllText(tl.DestinationPath);
            Perform(tl.DestinationPath, tl.NewText);
            DBController.DbLog.AddThemeLog(tl);
        }

        private static void Perform(string destinationPath,string newText)
        {
            try
            {
                File.WriteAllText(destinationPath, newText);
            }
            catch (Exception ex)
            {
                DBController.DbLog.AddLog(ex);
            }
        }

        internal static void ReverseStyle()
        {
            ThemeLog pl = DBController.DbLog.GetLastItem();
            Perform(pl.DestinationPath, pl.PrevText);
        }
    }

    public class ThemeLog : IDbLoadable
    {
        public DateTime Date { get;  set; }
        public int Id { get; private set; }
        public string NewText { get;  set; }
        public string PrevText { get;  set; }
        public XuiFile XiFile { get;  set; }
        public string DestinationPath { get;  set; }

        public ThemeLog(IDataReader data)
        {
            Load(data);
        }
        public ThemeLog()
        {
            
        }
        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            XiFile = (XuiFile)DataAccessUtility.LoadInt32(reader, "XuiFile");
            PrevText = DataAccessUtility.LoadNullable<string>(reader, "PrevText");
            NewText = DataAccessUtility.LoadNullable<string>(reader, "NewText");
            DestinationPath = DataAccessUtility.LoadNullable<string>(reader, "DestinationPath");
            Date = DataAccessUtility.LoadNullable<DateTime>(reader, "Date");
        }
    }

    public enum XuiFile
    {
        MainCss,
        CssResponsive,
        BgImage,
        JqueryScript
    }
}