using SynnCore.Migration;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace MusicHelper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                var str = SynnCore.Generics.XmlHelper.ToXml(new AppConfiguration { MediaPlayerPath = " ", ProdConnectionString = " ", SyncDirectories = new System.Collections.Generic.List<string> { "dir2", "dir1" }, TempMusicListPath = " ", TestConnectionString = " ", YoutubeDataFolder = " ", RequireAuthentication = false });
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var appcfg = LoadStationConfigurations();

                HandleDbMigration();
#if DEBUG
            GlobalAppData.SetUser(new LoggedUser { Id = 0, Password = "", UserName = "DEBUG - DEBUG" });
            Application.Run(new Form1());
#else
                if (appcfg.RequireAuthentication)
                    Application.Run(new LoginForm());
                else
                {
                    GlobalAppData.SetUser(new LoggedUser { Id = 0, Password = "", UserName = "Generic User" });
                    Application.Run(new Form1());
                }
#endif
            }
            catch (Exception ex)
            {
                var logP = @"C:\temp\musictemp\log.txt";
                var msg = string.Format("{0} - {1} Trace = {2}", DateTime.Now,ex.Message,ex.StackTrace);
                File.AppendAllLines(logP, new string[] { msg });
            }
        }

        private static void HandleDbMigration()
        {
            var methods = typeof(MigrationItems).GetMethods(BindingFlags.Static | BindingFlags.Public).ToList();
            var migrationTableName = ConfigurationSettings.AppSettings["migrationTableName"];
            string connStr = (new object()).GetConnectionString();
            MigrationHandler.Perform(connStr, migrationTableName, methods);
        }

        private static AppConfiguration LoadStationConfigurations()
        {
            var configFilePath = ConfigurationSettings.AppSettings["configFilePath"];
            AppConfiguration appCfg = SynnCore.Generics.XmlHelper.CreateFromXml<AppConfiguration>(File.ReadAllText(configFilePath));
            GlobalAppData.SetConfigs(appCfg);
            return appCfg;
        }
    }
}
