using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xmusic
{
    public interface IInput
    {
        string FileName { get; }

        string OutFileName { get; }
        WaveFormat IWaveFormat { get; }
        bool Eof();
        short GetBitsSample();
        int GetNumChannels();
        int GetSampleRate();
        int Read(float[] sampleBuffer, int bUFF_SIZE);
    }
}
