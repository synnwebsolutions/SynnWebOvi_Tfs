using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelper
{
    public static class GlobalAppData
    {
        private static LoggedUser cu;
        public static LoggedUser CurrentUser
        {
            get { return cu; }
        }

        private static AppConfiguration ap;
        public static AppConfiguration Configs
        {
            get { return ap; }
        }

        public static void SetConfigs(AppConfiguration cfg)
        {
            ap = cfg;
        }

        public static void SetUser(LoggedUser u)
        {
            cu = u;
        }
    }

    [Serializable]
    public class AppConfiguration
    {
        public string TestConnectionString { get; set; } // station
        public string ProdConnectionString { get; set; }// station
        public List<string> SyncDirectories { get; set; } // station
        public string YoutubeDataFolder { get; set; }// station
        public string TempMusicListPath { get;  set; }// station
        public string MediaPlayerPath { get;  set; } // station
    }


    public enum MediaPlayer
    {
        WindowsMediaPlayer,
        Winamp,
        Vlc
    }


    public class LoggedUser : IDbLoadable
    {
        public LoggedUser()
        {

        }
        public LoggedUser(IDataReader data)
        {
            Load(data);
        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }

        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            Password = DataAccessUtility.LoadNullable<string>(reader, "Password");
            UserName = DataAccessUtility.LoadNullable<string>(reader, "UserName");
        }
    }
}
