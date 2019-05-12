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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoadStationConfigurations();

            HandleDbMigration();
            Application.Run(new LoginForm());
        }

        private static void HandleDbMigration()
        {
            var methods = typeof(MigrationItems).GetMethods(BindingFlags.Static | BindingFlags.Public).ToList();
            var migrationTableName = ConfigurationSettings.AppSettings["migrationTableName"];
            string connStr = (new object()).GetConnectionString();
            MigrationHandler.Perform(connStr, migrationTableName, methods);
        }

        private static void LoadStationConfigurations()
        {
            var configFilePath = ConfigurationSettings.AppSettings["configFilePath"];
            AppConfiguration appCfg = SynnCore.Generics.XmlHelper.CreateFromXml<AppConfiguration>(File.ReadAllText(configFilePath));
            GlobalAppData.SetConfigs(appCfg);
        }
    }
}
