using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmusicCore
{
    [Serializable]
    public class UserTheme
    {
        public Color CheckedListBoxBackColor { get; set; }
        public Color DefaultColor { get; set; }
        public Color Exit { get; set; }
        public Color Grid { get; set; }
        public Color LoginAction { get; internal set; }
        public Color MainBg { get; set; }
        public Color MainBgLogin { get; internal set; }
        public Color MediaPlayer { get; set; }
        public Color MusicPlayerAction { get; internal set; }
        public Color MusicPlayerMain { get; internal set; }
        public Color Playlist { get; internal set; }
        public Color Search { get; set; }
        public Color SearchClear { get; set; }
        public Color SearchDo { get; set; }
        public Color SideBar { get; set; }
        public Color SyncAll { get; set; }
        public Color USB { get; internal set; }
        public bool UseShapedButtons { get; set; }
        public Color YouTube { get; internal set; }
        public Color YoutubeContainer { get; internal set; }
    }

    public class ThemeTag
    {
        public const string MainBg = "mb";
        public const string SyncAll = "sa";
        public const string Search = "src";
        public const string SearchClear = "srcc";
        public const string SearchDo = "srcd";
        public const string Grid = "grd";
        public const string SideBar = "sb";
        public const string MediaPlayer = "mp";
        public const string Exit = "ex";
        public const string Youtube = "yt";
        public const string USB = "usb";
        public const string Playlist = "pls";
        public const string MusicPlayerMain = "mpm";
        public const string MusicPlayerAction = "mpac";
        public const string YoutubeContainer = "ytc";
        public const string MainBgLogin = "lbg";
        public const string LoginAction = "lac";
    }

}
