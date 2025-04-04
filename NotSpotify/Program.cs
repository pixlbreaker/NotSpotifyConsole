using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using VideoLibrary;

namespace NotSpotify
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var exportPath = @"C:\Users\Michael\Documents\Projects\CSharpProjects\NotSpotifyConsole\NotSpotify\bin\Debug\net8.0\Videos";

            Console.WriteLine("Playlist URL: ");
            var playlistUrl = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(playlistUrl))
            {
                Console.WriteLine("Invalid URL. Exiting...");
                return;
            }

            if (!playlistUrl.ToLower().Contains("youtube.com"))
            {
                Console.WriteLine("Isn't an YouTube URL. Exiting...");
                return;
            }

            if (!playlistUrl.ToLower().Contains("playlist?list="))
            {
                Console.WriteLine("Ins't an YouTube Playlist. Exiting...");
                return;
            }

            var pathPlaylist = playlistUrl
                .Replace("https", "")
                .Replace("http", "")
                .Replace("://", "")
                .Replace("www.", "")
                .Replace("youtube.com/", "").Trim();

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.youtube.com");
            var result = client.GetAsync(pathPlaylist).Result;
            var conteudo = result.Content.ReadAsStringAsync().Result;
            var links = ExtrairLinksPlaylist(conteudo);

            if (links.Count == 0)
            {
                Console.WriteLine("None video found in the playlist. Exiting...");
                return;
            }

            var titulo = ExtrairTitulo(conteudo);
            var baseDir = $@"{exportPath}\{titulo}\";
            if (!Directory.Exists(baseDir))
            {
                Directory.CreateDirectory(baseDir);
            }

            var youtube = YouTube.Default;
            var engine = new Engine();

            var i = 1;
            foreach (var linkVideo in links)
            {
                var vid = youtube.GetVideo(linkVideo);

                Console.WriteLine($"{i}) " + vid.FullName);
                if (File.Exists(baseDir + vid.FullName))
                {
                    Console.WriteLine(" - File already exists. Skipping...");
                    continue;
                }

                File.WriteAllBytes(baseDir + vid.FullName, vid.GetBytes());
                var inputFile = new MediaFile { Filename = baseDir + vid.FullName };
                var outputFile = new MediaFile { Filename = $"{baseDir + vid.FullName}.mp3" };
                engine.GetMetadata(inputFile);
                engine.Convert(inputFile, outputFile);
                i++;
            }

            engine.Dispose();
        }

        private static string ExtrairTitulo(string conteudo)
        {
            try
            {
                var t = "<title>";
                var ini = conteudo.IndexOf(t) + t.Length;
                var fim = conteudo.IndexOf("</title>");
                var titulo = conteudo.Substring(ini, fim - ini);
                titulo = titulo.EndsWith(" - YouTube") ? titulo.Remove(titulo.LastIndexOf(" - YouTube")) : titulo;
                return titulo.Trim();
            }
            catch (Exception)
            {
                return "Youtube";
            }
        }

        private static List<string> ExtrairLinksPlaylist(string html)
        {
            var linksPlaylist = new List<string>();

            var w = "/watch?v=";
            var padraoLink = $"<a class=\"pl-video-title-link yt-uix-tile-link yt-uix-sessionlink  spf-link \" dir=\"ltr\" href=\"{w}";
            var idx = html.IndexOf(padraoLink, 0);

            while (idx > -1)
            {
                var idxInicioLink = idx + padraoLink.Length - w.Length;
                var idxFimLink = html.IndexOf("&", idxInicioLink);
                var pedacoLink = html.Substring(idxInicioLink, idxFimLink - idxInicioLink);
                linksPlaylist.Add(pedacoLink);

                idx = html.IndexOf(padraoLink, idxFimLink);
            }

            return linksPlaylist;
        }
    }
}