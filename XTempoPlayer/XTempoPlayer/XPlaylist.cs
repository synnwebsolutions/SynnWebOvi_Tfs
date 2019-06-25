using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmusicCore;

namespace XTempoPlayer
{
    public partial class XPlaylist : UserControl
    {
        public List<MusicItem> Plist { get; set; }
        public int CurrentIndex { get; set; }
        public XPlaylist()
        {
            InitializeComponent();
            Plist = new List<MusicItem>();
            CurrentIndex = 0;
        }

        private void XPlaylist_Load(object sender, EventArgs e)
        {
     
        }

        private void PanelSlide_Paint(object sender, PaintEventArgs e)
        {
            if (Plist.Count > 0)
            {
                xCurrentSongItem.Init(Plist[CurrentIndex]);
                xNext1SongItem.Init(Plist[CurrentIndex + 1]);
                if (CurrentIndex > 0)
                {
                    xPrevSongItem.Init(Plist[CurrentIndex - 1]);
                }
            }
        }
    }
}
