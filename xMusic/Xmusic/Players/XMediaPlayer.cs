using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xmusic.Players
{
    public  class XMediaPlayer : IDisposable
    {
        private  NAudionSoundComponent speedControl;
        private  WaveOutEvent playerComponent;
        private  XAudioReader reader;
        private  XAudioWriter writer;
        public void Play(string file)
        {
            reader = new XAudioReader(file);
            writer = new XAudioWriter(reader);
            speedControl = new NAudionSoundComponent(reader);
            playerComponent = new WaveOutEvent();
                
            playerComponent.Init(speedControl); 
            playerComponent.Play();
        }

        public  void SetTempo(float tempo)
        {
            if(speedControl != null)
                speedControl.PlaybackRate = tempo;
        }

        public void Stop()
        {
            playerComponent?.Stop();
        }

        public  float CurrentTempo()
        {
            if (speedControl != null)
                return speedControl.PlaybackRate;

            return 1;
        }

        public void Dispose()
        {
            reader?.Dispose();
            writer?.Dispose();
        }
    }
}
