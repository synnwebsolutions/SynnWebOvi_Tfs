using HundredMilesSoftware.UltraID3Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicHelper
{
    public class DirectoryHandler
    {
        int cnt = 0;
        public Action<int> ProgressInit { get;  set; }
        public Action<int> ReportProgress { get;  set; }

        public void SyncData()
        {

            try
            {
                List<DirectoryInfo> dList = GetDirectories();
                List<string> fList = new List<string>();

                var DbController = (new object()).InitDataProvider();
                foreach (var directory in dList)
                   fList.AddRange(GetDirectoryFiles(directory.FullName.ToString()));

                if (ProgressInit != null)
                    ProgressInit.Invoke(fList.Count);
                ClearExistingData(DbController);
                SaveFiles(DbController, fList);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }

        private void ClearExistingData(IDatabaseProvider dbController)
        {
            dbController.ClearData(new MusicSearchParameters { });
        }

        private List<string> GetDirectoryFiles(string selectedPath)
        {
            try
            {
                var wmaFiles = Directory.GetFiles(selectedPath, "*.wma", SearchOption.AllDirectories).ToList();
                var mp3Files = Directory.GetFiles(selectedPath, "*.mp3", SearchOption.AllDirectories).ToList();
                wmaFiles.AddRange(mp3Files);
                return wmaFiles;
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        private void ReportProgressOn()
        {
            if (ReportProgress != null)
                ReportProgress.Invoke(cnt);
        }
     
        private static List<DirectoryInfo> GetDirectories()
        {
            var dList = new List<DirectoryInfo>();
            foreach (var dI in GlobalAppData.Configs.SyncDirectories)
            {
                try { dList.Add(new DirectoryInfo(dI)); }
                catch (Exception) { }
            }

            return dList;
        }

        private void SaveFiles(IDatabaseProvider dbController, List<string> files)
        {
            foreach (var fi in files)
            {
                try
                {
                    var ii = TagLib.File.Create(fi);
                    UltraID3 u = new UltraID3();
                    u.Read(fi);
                    MusicItem ti = new MusicItem(ii,u);
                    if (!dbController.Match(ti))
                    {
                        dbController.AddMusicItem(ti);
                        cnt++;
                        ReportProgressOn();
                    }
                    else
                    {
                        // similar file already exists in db
                    }
                }
                catch (Exception ex)
                {
                   // throw;
                }
            }
        }
    }
}
