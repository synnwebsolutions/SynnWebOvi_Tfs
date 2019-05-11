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
        DriveInfo CurrentDrive;
        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
            this.ApplyTheme();
            DbController = this.InitDataProvider();
            
            HandleUsbs();
        }

        private void HandleUsbs()
        {
            Drives = DriveInfo.GetDrives().Where(drive => drive.IsReady && drive.DriveType == DriveType.Removable).ToList();
            foreach (var d in Drives)
            {
                chkUsbs.Items.Add(d.VolumeLabel);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshGrid();
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
            AlertSuccess();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty( txYoutubeUrl.Text))
            {
                var vidPath = YouTubeManager.DownloadAndConvert(txYoutubeUrl.Text);
                DbController.AddMusicItem(vidPath);
                
                txYoutubeUrl.Text = string.Empty;
                MessageBox.Show(string.Format("{0} Successfully Downloaded !", Path.GetFileName(vidPath.FullFileName)));
            }

        }

        private void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            List<MusicItem> musicitems = DbController.GetMusicItems(new MusicSearchParameters { SearchText =  txSearchText.Text });
            dgv.RefreshGrid(musicitems);
            AfterGridRefreshed();
        }

        private void AfterGridRefreshed()
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                var i = (MusicItem)row.DataBoundItem;
                if (i.ToUsb == true && i.ToPlaylist == true)
                    row.DefaultCellStyle.BackColor = Color.DarkOrange;
                else if (i.ToUsb == true)
                    row.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                else if (i.ToPlaylist == true)
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txSearchText.Text = string.Empty;
            RefreshGrid();
        }

        private void btnSyncUsb_Click(object sender, EventArgs e)
        {
            if (CurrentDrive != null)
            {
                List<MusicItem> musicitems = DbController.GetMusicItems(new MusicSearchParameters { InUsbList = true });
                UsbHandler.Sync(CurrentDrive, musicitems);
            }
            else
            {
                MessageBox.Show("Please Select Usb Drive");
            }
            AlertSuccess();
        }

        private int? currentIndex;
        private void dgv_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (currentIndex.HasValue)
                {
                    Point pt = dgv.PointToScreen(e.Location);
                    contextMenuStrip1.Show(pt);
                }
            }
            else
            {
                currentIndex = (sender as DataGridView).CurrentRow.Index;
                // e.RowIndex;
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var ai = e.ClickedItem;
            var menuAction = ai.Tag.ToString();
            var mclickedMusicItem = (MusicItem)dgv.CurrentRow.DataBoundItem;
            if (menuAction == "usb")
            {
                mclickedMusicItem.ToUsb = true;
            }
            if (menuAction == "plst")
            {
                mclickedMusicItem.ToPlaylist = true;
            }
            DbController.Update(mclickedMusicItem);

            currentIndex = null;
            RefreshGrid();
        }

        private void btnPlayUsbLst_Click(object sender, EventArgs e)
        {
            List<MusicItem> musicitems = DbController.GetMusicItems(new MusicSearchParameters { InUsbList = true});
            MusicListManager.PlayUsbList(musicitems);
        }

        private void btnPlayPlaylist_Click(object sender, EventArgs e)
        {
            List<MusicItem> musicitems = DbController.GetMusicItems(new MusicSearchParameters { InPlayList = true });
            MusicListManager.PlayPlayList(musicitems);
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var mclickedMusicItem = (MusicItem)dgv.CurrentRow.DataBoundItem;
            MusicListManager.PlaySingle(mclickedMusicItem);
        }

        private void chkUsbs_SelectedValueChanged(object sender, EventArgs e)
        {
            CurrentDrive = Drives.Where(x => x.VolumeLabel == chkUsbs.SelectedItem.ToString()).First();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Control  && e.KeyCode == Keys.B)
                MessageBox.Show("My message");
        }

        private void btnClearUsb_Click(object sender, EventArgs e)
        {
            List<MusicItem> musicitems = DbController.GetMusicItems(new MusicSearchParameters { InUsbList = true });
            foreach (var m in musicitems)
            {
                m.ToUsb = false;
                DbController.Update(m);
            }
            AlertSuccess();
        }

        private void AlertSuccess()
        {
            var dr = MessageBox.Show("Action Commited Successfully !");
            RefreshGrid();
        }
    }
}
