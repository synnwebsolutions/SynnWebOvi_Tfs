using HundredMilesSoftware.UltraID3Lib;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoLibrary;

namespace MusicHelper
{
    public static class YouTubeManager
    {
        private static string destinationFolder = GlobalAppData.Configs.YoutubeDataFolder;// @"E:\Music\Music Helper Youtube Downloads";

        public static MusicItem DownloadAndConvert(string videoUrl)
        {
            var newPath = string.Empty;
            GlobalAppData.SetWait();
            try
            {
                var youtube = YouTube.Default;
                var vid = youtube.GetVideo(videoUrl);
                newPath = Path.Combine(destinationFolder, vid.FullName);
                File.WriteAllBytes(newPath, vid.GetBytes());

                var inputFile = new MediaFile { Filename = newPath };
                var outputFile = new MediaFile { Filename = $"{newPath}.mp3" };

                using (var engine = new Engine())
                {
                    engine.GetMetadata(inputFile);
                    engine.Convert(inputFile, outputFile);
                    newPath = $"{newPath}.mp3";
                }
                File.Delete(inputFile.Filename);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                GlobalAppData.EndWait();
            }
            var ii = TagLib.File.Create(newPath);
            UltraID3 u = new UltraID3();
            u.Read(newPath);
            MusicItem ti = new MusicItem(ii, u);

            return ti;
        } 
    }
}
