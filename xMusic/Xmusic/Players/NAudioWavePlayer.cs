using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
