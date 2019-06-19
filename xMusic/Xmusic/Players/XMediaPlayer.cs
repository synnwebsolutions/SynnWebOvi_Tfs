using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xmusic.Extensions;

namespace Xmusic.Players
{
    public  class XMediaPlayer : IDisposable 
    {
        private  NAudionSoundComponent speedControl;
        private  WaveOutEvent playerComponent;
        //private XAudioWriter writer;
        private  XAudioReader reader;
        
        public List<string> Playlist;
        
        public int currentIndex;
        private bool initiatedStop;
        private TimeSpan position;

        public string Current
        {
            get
            {
                return Playlist[currentIndex];
            }
        }

        public Thread Thread { get; private set; }

        public XMediaPlayer(List<string> playlist)
        {
            Playlist = playlist.FilterMusicItems();
            currentIndex = 0;
        }

        private void PlayerComponent_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (!initiatedStop)
            {
                Next();
            }
        }

        public void Next()
        {
            if (Playlist.NotContaineIndex(currentIndex + 1))
                return;
            Stop();
            currentIndex++;
            Play();
        }

        public void Previous()
        {
            if (Playlist.NotContaineIndex(currentIndex - 1))
                return;
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

        float? lastTempo;
        public void Play()
        {
            position = new TimeSpan(0, 0, 0);

            initiatedStop = false;
            var file = Current;
#if DEBUG
            Console.WriteLine($"PLaying - {file} ...");
# endif
            reader = new XAudioReader(file);
            //writer = new XAudioWriter(reader);
            speedControl = new NAudionSoundComponent(reader);
            playerComponent = new WaveOutEvent();
            speedControl.PlaybackEnded += PlayerComponent_PlaybackStopped;
            playerComponent.Init(speedControl);
            if(lastTempo.HasValue)
                SetTempo(lastTempo.Value); 
            playerComponent.Play();
            this.Thread = new Thread(new ThreadStart(Run));
            this.Thread.Start();
        }

        private void Run()
        {
            while (true)
            { // Create an infinite loop
                //System.Diagnostics.Debug.WriteLine("PlaybackState: " + this.playerComponent.PlaybackState);

                if (this.playerComponent.PlaybackState == PlaybackState.Playing)
                {
                    double ms = playerComponent.GetPosition() * 1000.0 / playerComponent.OutputWaveFormat.BitsPerSample / playerComponent.OutputWaveFormat.Channels * 8 / playerComponent.OutputWaveFormat.SampleRate;
                    position = TimeSpan.FromMilliseconds(ms);
                }
                Thread.Sleep(1000); // Sleep for 1 second
            }
        }
        public  void SetTempo(float tempo)
        {
            if (speedControl != null)
            {
                speedControl.PlaybackRate = tempo;
                lastTempo = tempo;
            }
        }

        public void Stop()
        {
            initiatedStop = true;
            playerComponent?.Stop();
        }

        public void Pause()
        {
            playerComponent?.Pause();
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
            //writer?.Dispose();
            playerComponent?.Dispose();
            speedControl?.Dispose();
        }
    }
}
