using HundredMilesSoftware.UltraID3Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicHelper
{
    public partial class Form1 : Form
    {
        IDatabaseProvider DbController;
        List<DriveInfo> Drives;
        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
            this.ApplyTheme();
            DbController = this.InitDataProvider();
            Drives = DriveInfo.GetDrives().Where(drive => drive.IsReady && drive.DriveType == DriveType.Removable).ToList();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
        }

        private void ReportProgress(int val)
        {
            progressBar2.Value = val;
        }

        private void InitProgressBar(int totalItems)
        {
            progressBar2.Maximum = totalItems;
            progressBar2.Step = 1;
            progressBar2.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dh = new DirectoryHandler();
            dh.ProgressInit = InitProgressBar;
            dh.ReportProgress = ReportProgress;
            dh.SyncData();
            MessageBox.Show("Done");
        }
    }
}
