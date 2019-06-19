using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelper
{
    interface IMusicPlayer
    {
        bool Paused { get; set; }
        bool Shuffle { get; set; }

        void SetVolume(int value);
        void SetTempo(int value);
        void SetPosition(int value);
        void Pause();
        bool IsPlaying();
        void Stop();
        int GetCurentMilisecond();
        int GetSongLenght();
        bool IsStopped();
    }
}
