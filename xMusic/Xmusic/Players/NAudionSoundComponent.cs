using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xmusic
{
    public class NAudionSoundComponent : IXSoundComponent, ISampleProvider, IDisposable
    {
        private readonly ISampleProvider sourceProvider;
        private readonly SoundTouch soundTouch;
        private readonly float[] sourceReadBuffer;
        private readonly float[] soundTouchReadBuffer;
        private readonly int channelCount;
        private float playbackRate = 1.0f;
        private readonly int readDurationMilliseconds = 100;
        private bool repositionRequested;
        private bool endNotified;
        public event EventHandler<StoppedEventArgs> PlaybackEnded;
        
        public NAudionSoundComponent(ISampleProvider sourceProvider)
        {
            this.sourceProvider = sourceProvider;
            soundTouch = new  SoundTouch();

            // Settings
            soundTouch.SetRate(1.0f);
            soundTouch.SetPitchOctaves(playbackRate / 10);
            soundTouch.SetTempo(playbackRate);
            soundTouch.SetUseAntiAliasing(false);
            soundTouch.SetUseQuickSeek(false);
            soundTouch.SetSampleRate(WaveFormat.SampleRate);
            channelCount = WaveFormat.Channels;
            soundTouch.SetChannels(channelCount);
            sourceReadBuffer = new float[(WaveFormat.SampleRate * channelCount * (long)readDurationMilliseconds) / 1000];
            soundTouchReadBuffer = new float[sourceReadBuffer.Length * 10]; // support down to 0.1 speed
            endNotified = false;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            if (playbackRate == 0) // play silence
            {
                for (int n = 0; n < count; n++)
                {
                    buffer[offset++] = 0;
                }
                return count;
            }

            if (repositionRequested)
            {
                soundTouch.Clear();
                repositionRequested = false;
            }

            int samplesRead = 0;
            bool reachedEndOfSource = false;
            while (samplesRead < count)
            {
                if (soundTouch.NumberOfSamplesAvailable == 0)
                {
                    var readFromSource = sourceProvider.Read(sourceReadBuffer, 0, sourceReadBuffer.Length);
                    if (readFromSource > 0)
                    {
                        soundTouch.PutSamples(sourceReadBuffer, readFromSource / channelCount);
                    }
                    else
                    {
                        reachedEndOfSource = true;
                        // we've reached the end, tell SoundTouch we're done
                        soundTouch.Flush();
                       
                    }
                }
                var desiredSampleFrames = (count - samplesRead) / channelCount;

                var received = soundTouch.ReceiveSamples(soundTouchReadBuffer, desiredSampleFrames) * channelCount;
                // use loop instead of Array.Copy due to WaveBuffer
                for (int n = 0; n < received; n++)
                {
                    buffer[offset + samplesRead++] = soundTouchReadBuffer[n];
                }
                if (received == 0 && reachedEndOfSource) break;
            }
            if (reachedEndOfSource && !endNotified)
            {
                PlaybackEnded.Invoke(this, new StoppedEventArgs());
                endNotified = true;
            }
            return samplesRead;
        }

        public float PlaybackRate
        {
            get
            {
                return playbackRate;
            }
            set
            {
                if (playbackRate != value)
                {
                    UpdatePlaybackRate(value);
                    playbackRate = value;
                }
            }
        }

        private void UpdatePlaybackRate(float value)
        {
            if (value != 0)
            {
                soundTouch.SetTempo(value);
                soundTouch.SetPitchOctaves(value - 1);
            }
        }

        public void Dispose()
        {
            soundTouch.Dispose();
        }


        public WaveFormat WaveFormat => sourceProvider.WaveFormat;

        public void Reposition()
        {
            repositionRequested = true;
        }
    }
}
