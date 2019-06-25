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
        private float currentTempo = 1;
        public Form1()
        {
            InitializeComponent();
            txTempo.Text = currentTempo.ToString();
            //dgvPlaylist.Dock = DockStyle.Fill;
            CenterToScreen();
            DbController = this.InitDataProvider();
            RefreshView();
        }

        private void RefreshView()
        {
            plst = DbController.GetMusicItems(new MusicSearchParameters {/* SearchText = txFilter.Text */});
            player = new XMediaPlayer(plst.Select(x => x.FullFileName).ToList());
            xPlaylist1.Plist = plst;
            xPlaylist1.CurrentIndex = 1;
            //dgvPlaylist.RefreshGrid(plst);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            player.Dispose();
            this.Dispose();
            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            player.Stop();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            ClearGridSelection();
            player.Previous();
            SetGridSelection();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
           Play();
        }

        private void Play()
        {
            ClearGridSelection();
            player.Play();
            SetGridSelection();
        }


        private void ClearGridSelection()
        {
            //dgvPlaylist.Rows[player.currentIndex].Selected = false;
        }


        private void SetGridSelection()
        {
            //dgvPlaylist.Rows[dgvPlaylist.SelectedRow.Index].Selected = false;
            //dgvPlaylist.Rows[player.currentIndex].Selected = true;
            this.Name = $" X Tempo : Playing - { plst[player.currentIndex].FileName}";
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            ClearGridSelection();
            player.Next();
            SetGridSelection();
        }

        private void btnShuffle_Click(object sender, EventArgs e)
        {
            plst.Shuffle();
            //dgvPlaylist.RefreshGrid(plst);
            player.Stop();
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
            if (float.TryParse(txTempo.Text, out currentTempo))
            {
                player.SetTempo(currentTempo);
            }
        }

        private void dgvPlaylist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //var si = dgvPlaylist.Rows[e.RowIndex].DataBoundItem as MusicItem;
            //if (si != null)
            //{
            //    player.currentIndex = e.RowIndex;
            //    player.Play();
            //}
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void btnSetTempo_Click(object sender, EventArgs e)
        {
            if (float.TryParse(txTempo.Text, out currentTempo))
            {
                player.SetTempo(currentTempo);
            }
        }
    }
}
