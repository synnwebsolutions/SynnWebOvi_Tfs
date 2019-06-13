using NAudio.Utils;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xmusic;
using Xmusic.Players;
using Xmusic.Extensions;
using Microsoft.Expression.Encoder;
using Microsoft.Expression.Encoder.Profiles;
using Microsoft.Win32;
using Xmusic.Manipulations;
//using NAudio.WindowsMediaFormat;

namespace XMusicTester
{
    class Program
    {
        static void Main(string[] args)
        {
            XMusicLogger.Init();
            //string file = @"C:\Users\adelasm\Desktop\M-Overhaul\tests\Aster Aweke - 05. Ayzoh.wma";
            //string file = @"C:\Users\adelasm\Desktop\M-Overhaul\tests\r. kelly - down low feat ronald and ernie isley.mp3";
            //string file = @"C:\Users\AdelaPc\Desktop\124\Fkeraddis - Track 10.wma";
            //string file = @"C:\Users\AdelaPc\Desktop\124\Yinyues - Ylang Ylang.mp3";
            string file = @"D:\SmachData\Andreas B";
            //TestTempo(file);
            //TestPlayer(file);
            //TestMusicServiceProvider(file);
            TestMusicServiceProviderDirectory(file);
            //Test(file);
            //Console.WriteLine(XMusicLogger.GetLogs());
            Console.ReadLine();
        }

        private static void TestMusicServiceProviderDirectory(string dir)
        {
            var x = new MusicServiceProvider();
            var allFiles = Directory.GetFiles(dir);
            foreach (var file in allFiles)
            {
                try
                {
                    x.ProcessTempoJob(file, 14f);
                    //Console.WriteLine($"Start : { p.StartTime.ToShortTimeString()}  End At : { p.EndTime.ToShortTimeString()}");
                }
                catch (Exception ex)
                {
                    var trace = ex.StackTrace;
                    XMusicLogger.AddLog(ex.Message);
                    XMusicLogger.AddLog(trace);
                }
            }
        }

        private static void TestMusicServiceProvider(string file)
        {
            var x = new MusicServiceProvider();
            try
            {
                x.ProcessTempoJob(file,14f);
                //Console.WriteLine($"Start : { p.StartTime.ToShortTimeString()}  End At : { p.EndTime.ToShortTimeString()}");
            }
            catch (Exception ex)
            {
                var trace = ex.StackTrace;
                XMusicLogger.AddLog(ex.Message);
                XMusicLogger.AddLog(trace);
            }
        }

        private static void TestPlayer2(string file)
        {
            var player = new XMediaPlayer();
            player.Play(file);
            var res = "";
            while (res != "x")
            {
                res = Console.ReadLine();
                float tempo = player.CurrentTempo();
                if (float.TryParse(res, out tempo))
                {
                    player.SetTempo(tempo);
                }
            }
            player.Stop();
        }

        private static void TestPlayer(string file)
        {
            using (var reader = new XAudioReader(file))
            using (var writer = new XAudioWriter(reader))
            {
                var speedControl = new NAudionSoundComponent(reader);
                var wPlayer = new WaveOutEvent();
              
                wPlayer.Init(speedControl);
                wPlayer.Play();
             
                var res = "";
                while (res != "x")
                {
                    res = Console.ReadLine();
                    float tempo = speedControl.PlaybackRate;
                    if (float.TryParse(res, out tempo))
                    {
                        speedControl.PlaybackRate = tempo;
                    }
                }
                wPlayer?.Stop();
            }
        }

        private static void TestTempo(string file)
        {
            var p = new XJob { SourceFileName = file };
            p.ExecutionParameters.TempoDelta = 22;
            p.ExecutionParameters.PitchDelta = p.ExecutionParameters.TempoDelta / 10;
            try
            {
                var x = new XSoundProcessor();
                x.ProcessTempoJob(p);
                Console.WriteLine($"Start : { p.StartTime.ToShortTimeString()}  End At : { p.EndTime.ToShortTimeString()}");
            }
            catch (Exception ex)
            {
                var trace = ex.StackTrace;
                XMusicLogger.AddLog(ex.Message);
                XMusicLogger.AddLog(trace);
            }
        }

        private static void test5(string file)
        {
            var targetFilename = file.GenerateOutPutPath(XFileType.Mp3);
            ConvertToMP3(file, targetFilename);
        }

        static void ConvertToMP3(string sourceFilename, string targetFilename)
        {
            using (var reader = new NAudio.Wave.AudioFileReader(sourceFilename))
            using (var writer = new NAudio.Lame.LameMP3FileWriter(targetFilename, reader.WaveFormat, NAudio.Lame.LAMEPreset.STANDARD))
            {
                reader.CopyTo(writer);
            }
        }


        private static void TesT4(string file)
        {
            //XTempoProcessor.DoWorkEx(file);
        }

        //private static void Test3(string file)
        //{
        //    XSoundActionParameters p = new XSoundActionParameters
        //    {
        //        Action = XActionType.Convertion,
        //        DestinationFileType = XFileType.Mp3,
        //        SourceFileName = file,
        //    };
        //    XSoundProcessor.Process(p);
        //    if (p.LastActionResult == XActionResult.Success)
        //    {
        //        p.SourceFileName = p.LastActionResult;
        //        p.DestinationFileType = XFileType.Wav;
        //        XSoundProcessor.Process(p);
        //        if (p.LastActionResult == XActionResult.Success)
        //        {
        //            using (var reader = new XAudioReader(p.Output.ToString()))
        //            using (var writer = new XAudioWriter(reader))
        //            {
        //                //XSoundTempoConverter.ConvertAndSave(reader, writer);
        //                Console.WriteLine("Done");
        //                Console.ReadLine();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Error !!");
        //    }
        //}

        private static void Test2(string file)
        {
            using (var reader = new XAudioReader(file))
            using (var waveFileWriter = new WaveFileWriter(reader.OutFileName,  reader.WaveFormat))
            {
                byte[] buffer = new byte[reader.WaveFormat.AverageBytesPerSecond];
                int read;
                while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    waveFileWriter.Write(buffer, 0, read);
                }
            }
        }

        private static void Test(string file)
        {
            NAudioWavePlayer palyer = new NAudioWavePlayer();
            using (var reader = new XAudioReader(file))
            using (var writer = new XAudioWriter(reader))
            {

                var speedControl = new NAudionSoundComponent(reader);
                palyer.Init(speedControl);

                palyer.Play();
                var src = AppDomain.CurrentDomain.RelativeSearchPath;
                speedControl.PlaybackRate = 1.20f;

                //XSoundTempoConverter.ConvertAndSave(reader, writer);
                Console.WriteLine("Done");
                Console.ReadLine();
            }
        }
    }
}
