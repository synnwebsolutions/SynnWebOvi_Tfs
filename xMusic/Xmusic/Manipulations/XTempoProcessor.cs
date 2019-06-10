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

        public void Process(XConverter xConverter, XJob mainParameters)
        {
            switch (mainParameters.SourceFileType)
            {
                case XFileType.Wav:
                    //mainParameters.InitParameters(XFileType.Wav, mainParameters.SourceFileName);
                    //ProcessWav(mainParameters.InnerParameters);
                    //mainParameters.AppendTempParameters();

                    //mainParameters.InitParameters(XFileType.Mp3, mainParameters.InnerParameters.OutputResultFilePath);
                    //xConverter.DoWork(mainParameters.InnerParameters);
                    //mainParameters.AppendTempParameters();
                    break;
                case XFileType.Mp3:

                    //mainParameters.InitParameters(XFileType.Wav, mainParameters.SourceFileName);
                    //xConverter.DoWork(mainParameters.InnerParameters);
                    //mainParameters.AppendTempParameters();

                    //mainParameters.InitParameters(XFileType.Wav, mainParameters.InnerParameters.OutputResultFilePath);
                    //ProcessWav(mainParameters.InnerParameters);
                    //mainParameters.AppendTempParameters();

                    //mainParameters.InitParameters(XFileType.Mp3, mainParameters.InnerParameters.OutputResultFilePath);
                    //xConverter.DoWork(mainParameters.InnerParameters);
                    //mainParameters.AppendTempParameters();
                    break;
                case XFileType.Wma:

                    //mainParameters.InitParameters(XFileType.Mp3, mainParameters.SourceFileName);
                    //xConverter.DoWork(mainParameters.InnerParameters);
                    //mainParameters.AppendTempParameters();

                    //mainParameters.InitParameters(XFileType.Wav, mainParameters.InnerParameters.OutputResultFilePath);
                    //xConverter.DoWork(mainParameters.InnerParameters);
                    //mainParameters.AppendTempParameters();

                    //mainParameters.InitParameters(XFileType.Wav, mainParameters.InnerParameters.OutputResultFilePath);
                    //ProcessWav(mainParameters.InnerParameters);
                    //mainParameters.AppendTempParameters();

                    //mainParameters.InitParameters(XFileType.Mp3, mainParameters.InnerParameters.OutputResultFilePath);
                    //xConverter.DoWork(mainParameters.InnerParameters);
                    //mainParameters.AppendTempParameters();

                    // convert to Wav
                    // delete Source File
                    // process Wav
                    // Convert to mp3
                    // delete source file
                    break;
                default:
                    break;
            }
            //HandleTemporaryFile(mainParameters);
        } 
        private void Setup(SoundTouch<TSampleType, TLongSampleType> pSoundTouch, int sampleRate, int channels, RunParameters parameters)
        {
            pSoundTouch.SetSampleRate(sampleRate);
            pSoundTouch.SetChannels(channels);

            pSoundTouch.SetTempoChange(parameters.TempoDelta);
            pSoundTouch.SetPitchSemiTones(parameters.PitchDelta);
            pSoundTouch.SetRateChange(parameters.RateDelta);

            pSoundTouch.SetSetting(SettingId.UseQuickseek, parameters.Quick);
            pSoundTouch.SetSetting(SettingId.UseAntiAliasFilter, (parameters.NoAntiAlias == 1) ? 0 : 1);
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

        private void ProcessWav(XJob param)
        {
            string fileName = param.SourceFileName;
            var outFileName = fileName.GenerateOutPutPath();
            var soundTouch = new SoundTouch<TSampleType, TLongSampleType>();
            using (var inFile = new WavInFile(fileName))
            using (var outFile = new WavOutFile(outFileName, (inFile).GetSampleRate(), (inFile).GetNumBits(), (inFile).GetNumChannels()))
            {
                Setup(soundTouch, inFile.GetSampleRate(), inFile.GetNumChannels(), param.ExecutionParameters);

                Process(soundTouch, inFile, outFile);
            }
        }
        
        public void Dispose()
        {
            
        }
    }
}
