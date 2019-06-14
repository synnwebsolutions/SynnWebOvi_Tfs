using SoundStretch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xmusic.Extensions;
using TSampleType = System.Single;
using TLongSampleType = System.Double;
using SoundTouch;
using NAudio.Wave;
using System.IO;
using Xmusic.Manipulations;

namespace Xmusic
{
    public class XTempoProcessor : IDisposable
    {
        private const int BUFF_SIZE = 2048;

        internal void Process(MusicService job)
        {
            var xj = new XTempoJob();
            xj.SourceFileName = job.SourceFile;
            xj.AlternativeOutputPath = job.DestinationFile;
            xj.TempoDelta = job.TempoDelta;
            xj.PitchDelta = job.PitchDelta;
            ProcessWav(xj);
        }

        internal void Process(XTempoJob wavTempoJob)
        {
            ProcessWav(wavTempoJob);
        }

        private void Setup(SoundTouch<TSampleType, TLongSampleType> pSoundTouch, int sampleRate, int channels, float TempoDelta, float PitchDelta)
        {
            pSoundTouch.SetSampleRate(sampleRate);
            pSoundTouch.SetChannels(channels);

            pSoundTouch.SetTempoChange(TempoDelta);
            pSoundTouch.SetPitchSemiTones(PitchDelta);
            pSoundTouch.SetRateChange(0);

            pSoundTouch.SetSetting(SettingId.UseQuickseek, 0);
            pSoundTouch.SetSetting(SettingId.UseAntiAliasFilter, 1);
        }

        private void Process(SoundTouch<TSampleType, TLongSampleType> pSoundTouch, WavInFile inFile, WavOutFile outFile)
        {
            int nSamples;
            var sampleBuffer = new TSampleType[BUFF_SIZE];

            if ((inFile == null) || (outFile == null)) return; // nothing to do.

            int nChannels = inFile.GetNumChannels();
            
            int buffSizeSamples = BUFF_SIZE / nChannels;

            // Process samples read from the input file
            while (!inFile.Eof())
            {
                // Read a chunk of samples from the input file
                int num = inFile.Read(sampleBuffer, BUFF_SIZE);
                nSamples = num / inFile.GetNumChannels();

                // Feed the samples into SoundTouch processor
                pSoundTouch.PutSamples(sampleBuffer, nSamples);
                do
                {
                    nSamples = pSoundTouch.ReceiveSamples(sampleBuffer, buffSizeSamples);
                    outFile.Write(sampleBuffer, nSamples * nChannels);
                } while (nSamples != 0);
            }

            // Now the input file is processed, yet 'flush' few last samples that are
            // hiding in the SoundTouch's internal processing pipeline.
            pSoundTouch.Flush();
            do
            {
                nSamples = pSoundTouch.ReceiveSamples(sampleBuffer, buffSizeSamples);
                outFile.Write(sampleBuffer, nSamples * nChannels);
            } while (nSamples != 0);
        }

        private void ProcessWav(XTempoJob param)
        {
            var outFileName = string.IsNullOrEmpty(param.AlternativeOutputPath) ? param.SourceFileName.GenerateOutPutPath() :
                param.AlternativeOutputPath;
            var soundTouch = new SoundTouch<TSampleType, TLongSampleType>();
            if (param.SourceData != null)
            {
                File.WriteAllBytes(param.SourceFileName, param.SourceData);
            }

            using (var inFile = new WavInFile(param.SourceFileName))
            using (var outFile = new WavOutFile(outFileName, (inFile).GetSampleRate(), (inFile).GetNumBits(), (inFile).GetNumChannels()))
            {
                Setup(soundTouch, inFile.GetSampleRate(), inFile.GetNumChannels(), param.TempoDelta,param.PitchDelta);
                Process(soundTouch, inFile, outFile);
            }
            param.ResulFileName = outFileName;
            if (param.ReturnData)
            {
                param.OutputData = File.ReadAllBytes(outFileName);
                File.Delete(outFileName);
            }
        }
        
        public void Dispose()
        {
            
        }
    }
}
