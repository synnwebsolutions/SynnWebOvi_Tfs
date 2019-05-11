using PlaylistsNET.Content;
using PlaylistsNET.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelper
{
    public static class MusicListManager
    {
        internal static void PlayUsbList(List<MusicItem> m)
        {
            M3uContent content = new M3uContent();
            M3uPlaylist lst = Generate(m);
            string created = content.ToText(lst);
            SaveToFile(created);
        }

        private static void SaveToFile(string created)
        {
            string pt = $@"C:\temp\musictemp\temp" + $"{ (new Random()).Next(0, 15214) }.m3u";
            using (var fs = new FileStream(pt, FileMode.Create))
            {

            }
            File.WriteAllText(pt, created);

            Play(pt);
        }

        private static void Play(string pt)
        {
            if (File.Exists(pt))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = @"E:\SanProgramFiles\Winamp\winamp.exe";
                startInfo.Arguments = pt;
                Process.Start(startInfo);
            }
        }

        private static M3uPlaylist Generate(List<MusicItem> m)
        {
            var l = new M3uPlaylist();
            l.IsExtended = false;
            l.PlaylistEntries = new List<M3uPlaylistEntry>();
            foreach (var mi in m)
            {
                l.PlaylistEntries.Add(new M3uPlaylistEntry()
                {
                    Album = string.Empty,
                    AlbumArtist = mi.Artist,
                    Duration = TimeSpan.Zero,
                    Path = mi.FullFileName,
                    Title = mi.Title,
                });
            }
            return l; 
        }

        internal static void PlayPlayList(List<MusicItem> m)
        {
            M3uContent content = new M3uContent();
            M3uPlaylist lst = Generate(m);
            string created = content.ToText(lst);
        }

        internal static void PlaySingle(MusicItem mclickedMusicItem)
        {
            Play(mclickedMusicItem.FullFileName);
        }
    }
}
