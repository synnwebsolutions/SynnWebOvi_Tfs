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


        public List<MusicItem> UsbList()
        {
                return DbController.GetMusicItems(new MusicSearchParameters { InUsbList = true });
        }

        public List<MusicItem> PlayList()
        {
            return DbController.GetMusicItems(new MusicSearchParameters { InPlayList = true }); 
        }

        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
            ApplyTags();
            this.ApplyTheme();
            DbController = this.InitDataProvider();
            SetDisplay();
            HandleUsbs();
            HandleGrids();
        }

        private void HandleGrids()
        {
            dgvUsb.CellDoubleClick += dgv_CellDoubleClick;
            dgvYoutube.CellDoubleClick += dgv_CellDoubleClick;
            dgvPlaylist.CellDoubleClick += dgv_CellDoubleClick;


            dgvUsb.MouseClick += dgv_MouseClick;
            dgvYoutube.MouseClick += dgv_MouseClick;
            dgvPlaylist.MouseClick += dgv_MouseClick;

            
        }

        private void ApplyTags()
        {
            this.Tag = ThemeTag.MainBg;
            btnSync.Tag = ThemeTag.SyncAll;
            pnDrives.Tag = ThemeTag.Search;
            btnClip.Tag = ThemeTag.Search;
            btnClear.Tag = ThemeTag.SearchClear;
            btnRefreshGrid.Tag = ThemeTag.SearchDo;
            btnExit.Tag = ThemeTag.Exit;
            btnYoutubeDownload.Tag = ThemeTag.Youtube;
            btnSyncUsb.Tag = btnPlayUsbLst.Tag = btnClearUsb.Tag = ThemeTag.USB;
            btnPlayPlaylist.Tag = ThemeTag.Playlist;
            dgv.Tag = ThemeTag.MainBg;
            gbYoutube.Tag = ThemeTag.YoutubeContainer;
        }

        private void SetDisplay()
        {
            btnSyncUsb.Enabled = CurrentDrive != null;
            lblUser.Text = $"Logged as : { GlobalAppData.CurrentUser.UserName}";
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
            GlobalAppData.SetWait();
            progressBar2.Visible = true;
            var dh = new DirectoryHandler();
            dh.ProgressInit = InitProgressBar;
            dh.ReportProgress = ReportProgress;
            dh.SyncData();
            AlertSuccess();
            progressBar2.Visible = false;
            GlobalAppData.EndWait();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty( txYoutubeUrl.Text))
            {
                TimeSpan? begin = txYoutubeFromSeconds.GetTimeSpan() ;
                TimeSpan? endAt = txYoutubeToSeconds.GetTimeSpan();
                var vidPath = YouTubeManager.DownloadAndConvert(txYoutubeUrl.Text,begin,endAt);
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
            var cg = GetCurrentGrid();
            List<MusicItem> musicitems = GetCurrentGridItems(); // 
            cg.RefreshGrid(musicitems);
            AfterGridRefreshed();

            string syncText = "Sync USB";
            btnSyncUsb.Text = string.Format("{0} ({1} Songs)",syncText, UsbList().Count);
            lblGridSummary.Text = $"{ String.Format("{0:n0}", musicitems.Count)} Items ";
            synnMPlayer1.InitList(musicitems);
        }

        private List<MusicItem> GetCurrentGridItems()
        {
            List<MusicItem> g = null;
            if (tabControl1.SelectedTab == tbPlaylist)
            {
                return PlayList();
            }
            if (tabControl1.SelectedTab == tbUsb)
            {
                return UsbList();
            }
            if (tabControl1.SelectedTab == tbYoutube)
            {
                return DbController.GetMusicItems(new MusicSearchParameters { SearchText = "youtube" }); ;
            }
            return DbController.GetMusicItems(new MusicSearchParameters { SearchText = txSearchText.Text });
        }

        private void AfterGridRefreshed()
        {
            var cg = GetCurrentGrid();
            if (cg.RowCount > 0 )
            {
                var usbList = UsbList();
                var playList = PlayList();
                foreach (DataGridViewRow row in cg.Rows)
                {
                    var i = (MusicItem)row.DataBoundItem;
                    if (playList.NotEmpty() &&  playList.Any(x => x.Id == i.Id) && usbList.Any(x => x.Id == i.Id))
                        row.DefaultCellStyle.BackColor = Color.DarkOrange;
                    else if (usbList.NotEmpty() && usbList.Any(x => x.Id == i.Id))
                        row.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    else if (playList.NotEmpty() && playList.Any(x => x.Id == i.Id))
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
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
                GlobalAppData.SetWait();
                UsbHandler.Sync(CurrentDrive, UsbList());
                GlobalAppData.EndWait();
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
                    Point pt = (sender as DataGridView).PointToScreen(e.Location);
                    contextMenuStrip1.Show(pt);
                }
            }
            else
            {
                currentIndex = (sender as DataGridView).CurrentRow?.Index;
                // e.RowIndex;
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var ai = e.ClickedItem;
            var menuAction = ai.Tag.ToString();
            var mclickedMusicItem = (MusicItem)(GetCurrentGrid()).CurrentRow.DataBoundItem;
            if (menuAction == "usb")
            {
                DbController.AddToUsbList(mclickedMusicItem);
            }
            if (menuAction == "plst")
            {
                DbController.AddToPlayList(mclickedMusicItem);
            }
            if (menuAction == "dir")
            {
                var dir = Path.GetDirectoryName(mclickedMusicItem.FullFileName);
                DirectoryHandler.OpenContaingFolder(dir);
                return;
            }
            //DbController.Update(mclickedMusicItem);

            currentIndex = null;
            RefreshGrid();
        }

        private void btnPlayUsbLst_Click(object sender, EventArgs e)
        {
            GlobalAppData.SetWait();
            MusicListManager.PlayUsbList(UsbList());
            GlobalAppData.EndWait();
        }

        private void btnPlayPlaylist_Click(object sender, EventArgs e)
        {
            GlobalAppData.SetWait();
            MusicListManager.PlayPlayList(PlayList());
            GlobalAppData.EndWait();
        }

        private DataGridView  GetCurrentGrid()
        {
            DataGridView g = dgv;
            if (tabControl1.SelectedTab == tbPlaylist)
                return dgvPlaylist;
            if (tabControl1.SelectedTab == tbUsb)
                return dgvUsb;
            if (tabControl1.SelectedTab == tbYoutube)
                return dgvYoutube;
            return dgv;
        }
        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var mclickedMusicItem = (MusicItem)(sender as DataGridView).CurrentRow.DataBoundItem;
            //MusicListManager.PlaySingle(mclickedMusicItem);
            //using (SynnPlayerUi player = new SynnPlayerUi())
            //{
            //    player.Init(new List<MusicItem> { mclickedMusicItem });
            //    player.ShowDialog();
            //}
            synnMPlayer1.Play(e.RowIndex);
        }

        private void chkUsbs_SelectedValueChanged(object sender, EventArgs e)
        {
            if(chkUsbs.SelectedItem != null)
                CurrentDrive = Drives.Where(x => x.VolumeLabel == chkUsbs.SelectedItem.ToString()).First();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Control  && e.KeyCode == Keys.B)
                MessageBox.Show("My message");
        }

        private void btnClearUsb_Click(object sender, EventArgs e)
        {
            DbController.ClearUsbList();
            AlertSuccess();
        }

        private void AlertSuccess()
        {
            var dr = MessageBox.Show("Action Commited Successfully !");
            RefreshGrid();
        }

        private void btnClip_Click(object sender, EventArgs e)
        {
            txSearchText.Text = Clipboard.GetText();
            RefreshGrid();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }
    }
}
