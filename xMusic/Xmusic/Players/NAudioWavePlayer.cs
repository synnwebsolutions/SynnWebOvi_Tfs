using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xmusic.Extensions;

namespace Xmusic.Players
{
    /// <summary>
    ///     wPlayer = new WaveOutEvent();
    ///     wPlayer.PlaybackStopped += WavePlayerOnPlaybackStopped;
    ///     wavePlayer.Init(speedControl); --> speedcontrol = IXSoundComponent
    ///     wavePlayer.Play();
    /// 
    ///     wavePlayer?.Stop();
    ///     speedControl.PlaybackRate = 0.5f + trackBarPlaybackRate.Value*0.1f; -> chang speed
    /// 
    ///     speedControl = new NAudionSoundComponent(IXMusicFileProvider);
    /// 
    /// 
    /// </summary>
    public class NAudioWavePlayer : WaveOutEvent
    {
        private List<string> Playlist;
        //private string current;
        private int currentIndex;

        public string Current
        {
            get
            {
                return Playlist[currentIndex];
            }
        }

        public NAudioWavePlayer(List<string> playlist)
        {
            Playlist = playlist;
            currentIndex = 0;
        }

        public void Next()
        {
            Stop();
            currentIndex++;
            Play();
        }

        public void Previous()
        {
            Stop();
            currentIndex--;
            Play();
        }

        public void Shuffle()
        {
            var ci = Current;
            Playlist.Shuffle();
            currentIndex = Playlist.IndexOf(ci);
        }
    }
}
