using System;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using MediaToolkit;
using MediaToolkit.Model;
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

            ////Youtube Client
            //var youtube = new YoutubeClient();
            //var url = "https://www.youtube.com/watch?v=T6eK-2OQtew";
            //var streamManifest = await youtube.Videos.Streams.GetManifestAsync(url);
            //var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

            //// Downloads the video
            //await youtube.Videos.Streams.DownloadAsync(streamInfo, $"video.{streamInfo.Container}");
            DownloadFile();


        }

        static async void DownloadFile()
        {
            var youtube = new YoutubeClient();
            var url = "https://www.youtube.com/watch?v=T6eK-2OQtew";
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(url);
            var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

            await youtube.Videos.Streams.DownloadAsync(streamInfo, $"video.{streamInfo.Container}");
        }
    }
}