using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xmusic.Players;
using SynnCore.Controls;
using System.IO;

namespace MusicHelper
{
    public partial class XMusicPlayerUi : UserControl
    {
        XMediaPlayer player;
        public List<MusicItem> lstPlaylist { get; set; }

        public void InitList(List<MusicItem> l)
        {
            lstPlaylist = l;
            InitList(lstPlaylist.Select(x => x.FullFileName).ToList());
        }

        private void InitList(List<string> list)
        {
            player = new XMediaPlayer(list);
            InitListUi(list);
        }

        private void InitListUi(List<string> list, string selected = null)
        {
            listBox1.Items.Clear();
        
            foreach (var x in list)
            {
                listBox1.Items.Add(new ListItem { Text = Path.GetFileName(x), Value = x });
            }
            if (selected != null)
                listBox1.SelectedIndex = list.IndexOf(selected);
        }

        public XMusicPlayerUi()
        {
            InitializeComponent();
            InitList(new List<MusicItem>());
        }

        private void trbBalance_Scroll(object sender, EventArgs e)
        {
            var ntp = (decimal)((decimal)trbBalance.Value / (decimal)100) + 1;
            player.SetTempo((float)ntp);
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            player.Pause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            player.Stop();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            player.Previous();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            player.Next();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                var selectedItem = (listBox1.Items[index] as ListItem).Value;
                player.currentIndex = index;
                player.Play();
            }
        }

        private void btnShufflle_Click(object sender, EventArgs e)
        {
            player.Shuffle();
            InitListUi(player.Playlist, player.Current);
        }
    }
}
