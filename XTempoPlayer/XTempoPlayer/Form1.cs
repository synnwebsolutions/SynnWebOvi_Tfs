using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xmusic.Players;
using XmusicCore;

namespace XTempoPlayer
{
    public partial class Form1 : Form
    {
        XMediaPlayer player;
        IDatabaseProvider DbController;
        List<MusicItem> plst;
        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
            DbController = this.InitDataProvider();
            plst = DbController.GetMusicItems(new MusicSearchParameters { });
            player = new XMediaPlayer(plst.Select(x => x.FullFileName).ToList());
            dgvPlaylist.RefreshGrid(plst);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            player.Dispose();
            this.Dispose();
            Application.Exit();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            player.Stop();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            player.Previous();
            SetGridSelection();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
           Play();
        }

        private void Play()
        {
            player.Play();
            SetGridSelection();
        }

        private void SetGridSelection()
        {
            //dgvPlaylist.Rows[dgvPlaylist.SelectedRow.Index].Selected = false;
            dgvPlaylist.Rows[player.currentIndex].Selected = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            player.Next();
            SetGridSelection();
        }

        private void btnShuffle_Click(object sender, EventArgs e)
        {
            plst.Shuffle();
            dgvPlaylist.RefreshGrid(plst);
            player.Playlist = plst.Select(x => x.FullFileName).ToList();
            //Play();
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {

        }

        private void btnSycDataBase_Click(object sender, EventArgs e)
        {

        }

        private void tempoTrackBar_Scroll(object sender, EventArgs e)
        {
            var val = ((float)tempoTrackBar.Value / (float)10);
            player.SetTempo(val);
        }

        private void dgvPlaylist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var si = dgvPlaylist.Rows[e.RowIndex].DataBoundItem as MusicItem;
            if (si != null)
            {
                player.currentIndex = e.RowIndex;
                player.Play();
            }
        }
    }
}
