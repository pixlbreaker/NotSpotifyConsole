using MediaToolkit.Model;
using MediaToolkit;
using System;
using System.IO;
using VideoLibrary;

namespace NotSpotify
{
    public class Program
    {
        string youtubePlaylistURL = "";

        public static void Main(string[] args)
        {
            Console.WriteLine("YouTube API Playlist Download");
            Console.WriteLine("==================================");


            //bool downloaded = DownloadVideo("T6eK-2OQtew");
            string path = Directory.GetCurrentDirectory() + "//Videos//";
            SaveMP3(path, "https://www.youtube.com/watch?v=AF2MqFnPotc", "bbl");

        }

        static bool DownloadVideo(string id)
        {
            try
            {
                var VedioUrl = "https://www.youtube.com/embed/" + id + ".mp4";
                var youTube = YouTube.Default;
                var video = youTube.GetVideo(VedioUrl);
                string path = Directory.GetCurrentDirectory() + "//Videos//" + id + ".mp4";
                File.WriteAllBytes(path, video.GetBytes());
            }
            catch (Exception ex) { return false; }
            return true;
        }

        private static void SaveMP3(string SaveToFolder, string VideoURL, string MP3Name)
        {
            var source = @SaveToFolder;
            var youtube = YouTube.Default;
            var vid = youtube.GetVideo(VideoURL);
            File.WriteAllBytes(source + vid.FullName, vid.GetBytes());

            var inputFile = new MediaFile { Filename = source + vid.FullName };
            var outputFile = new MediaFile { Filename = $"{MP3Name}.mp3" };

            using (var engine = new Engine())
            {
                engine.GetMetadata(inputFile);

                engine.Convert(inputFile, outputFile);
            }
        }
    }
}