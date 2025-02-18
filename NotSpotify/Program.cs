using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.IO;
using VideoLibrary;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;

namespace NotSpotify
{
    public class Program
    {
        string youtubePlaylistURL = "";

        public static void Main(string[] args)
        {
            Console.WriteLine("YouTube API Playlist Download");
            Console.WriteLine("==================================");

            DownloadVideo("this");

            //string path = Directory.GetCurrentDirectory() + "\\Videos\\";
            //SaveMP3(path, "https://www.youtube.com/watch?v=AF2MqFnPotc", "bbl");

        }

        static async void DownloadVideo(string id)
        {
            var youtube = new YoutubeClient();
            var videoUrl = "https://www.youtube.com/watch?v=AF2MqFnPotc";

            //string path = Directory.GetCurrentDirectory() + "\\Videos\\" + "video.mp4";
            //await youtube.Videos.DownloadAsync(videoUrl, "video.mp4");

            var streamManifest = await youtube.Videos.
            var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

            // Get the actual stream
            var stream = await youtube.Videos.Streams.GetAsync(streamInfo);

            // Download the stream to a file
            await youtube.Videos.Streams.DownloadAsync(streamInfo, $"video.{streamInfo.Container}");
        }

        private static void SaveMP3(string SaveToFolder, string VideoURL, string MP3Name)
        {
            var source = @SaveToFolder;
            var youtube = YouTube.Default;
            var vid = youtube.GetVideo(VideoURL);
            File.WriteAllBytes(source + vid.FullName, vid.GetBytes());

            var inputFile = new MediaFile { Filename = source + vid.FullName };
            var outputFile = new MediaFile { Filename = $"{MP3Name}.mp3" };

            Engine engine = new Engine();
            engine.GetMetadata(inputFile);
            engine.Convert(inputFile, outputFile);
        }
    }
}