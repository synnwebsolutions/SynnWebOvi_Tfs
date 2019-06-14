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
            //string file = @"C:\Users\AdelaPc\Desktop\124\2 pac - tupac shakur - me against the world.mp3";
            //string file = @"C:\Users\AdelaPc\Desktop\124\Yinyues - Ylang Ylang.mp3";
            //string file = @"D:\SmachData\Andreas B";
            //string file = @"C:\Users\AdelaPc\Desktop\124";
            string file = @"E:\ICAR";
            //string file = @"C:\Users\AdelaPc\Desktop\5832";
            //TestTempo(file);
            TestPlayer2(file);
            //TestPlayer(file);
            //TestMusicServiceProvider(file);
            //TestMusicServiceProviderDirectory(file);
            //Test(file);
            //Console.WriteLine(XMusicLogger.GetLogs());
            Console.ReadLine();
        }

        private static void TestMusicServiceProviderDirectory(string dir)
        {
            var x = new MusicServiceProvider();
         
            try
            {
                x.ProcessDirectoryTempoJob(dir, 14f);
            }
            catch (Exception ex)
            {
                var trace = ex.StackTrace;
                XMusicLogger.AddLog(ex.Message);
                XMusicLogger.AddLog(trace);
            }
        }

        private static void TestMusicServiceProvider(string file)
        {
            var x = new MusicServiceProvider();
            try
            {
                x.ProcessSingleTempoJob(file, 14f);
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
            var lst = file.IsDirectory() ? Directory.GetFiles(file,"*", SearchOption.AllDirectories).ToList() : new List<string> { file };

            var player = new XMediaPlayer(lst);
            player.Play();
            var res = "";
            while (res != "x")
            {
                res = Console.ReadLine();
                float tempo = player.CurrentTempo();
                if (float.TryParse(res, out tempo))
                {
                    player.SetTempo(tempo);
                }
                if (res == "n")
                {
                    player.Next();
                }
                if (res == "p")
                {
                    player.Previous();
                }
                if (res == "s")
                {
                    player.Shuffle();
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

    }
}
