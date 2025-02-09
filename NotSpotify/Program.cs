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

namespace NotSpotify
{
    class Program
    {
        string youtubePlaylistURL = "";

        static void Main(string[] args)
        {
            Console.WriteLine("YouTube API Playlist Download");
            Console.WriteLine("==================================");

            // Gets the youtube playlist

        }

        /// <summary>
        /// Saves a video url to an mp3 in a specific folder
        /// </summary>
        /// <param name="SaveToFolder"></param>
        /// <param name="VideoURL"></param>
        /// <param name="MP3Name"></param>
        private void SaveMP3(string SaveToFolder, string VideoURL, string MP3Name)
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