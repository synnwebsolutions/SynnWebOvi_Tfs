using SynnCore.Migration;
using System;
using System.Configuration;
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
            HandleDbMigration();
            Application.Run(new Form1());
        }

        private static void HandleDbMigration()
        {
            var methods = typeof(MigrationItems).GetMethods(BindingFlags.Static | BindingFlags.Public).ToList();
            var migrationTableName = ConfigurationSettings.AppSettings["migrationTableName"];
            MigrationHandler.Perform(ExtensionsHandler.GetConnectionString(), migrationTableName, methods);
        }
    }
}
