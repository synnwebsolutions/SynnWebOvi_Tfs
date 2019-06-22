using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmusicCore;

namespace XTempoPlayer
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
            var appcfg = LoadStationConfigurations();
            //if (appcfg.RequireAuthentication)
            //{
            //    Application.Run(new LoginForm());
            //}
            //else
            //{
            //    GlobalAppData.SetUser(new LoggedUser { Id = 0, Password = "", UserName = "Generic User" });
            //    Application.Run(new Form1());
            //}
            Application.Run(new Form1());
          
            if (Application.MessageLoop)
                Application.Exit();
            else
                Environment.Exit(1);
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
