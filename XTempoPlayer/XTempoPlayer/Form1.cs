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

namespace XTempoPlayer
{
    public partial class Form1 : Form
    {
        XMediaPlayer player;
        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
            player = new XMediaPlayer(list);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            player.Stop();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            player.Previous();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            player.Play();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            player.Next();
        }

        private void btnShuffle_Click(object sender, EventArgs e)
        {
            player.Shuffle();
            RefreshList();
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

        private void RefreshList()
        {
            throw new NotImplementedException();
        }
    }
}
