import os
import pytubefix
from yt_dlp import YoutubeDL
import utils

DOWNLOAD_PATH = "E:\\Music\\NotSpotify"
PLAYLIST_URL = 'https://www.youtube.com/playlist?list=PLJeE3c06PNfzXdsUY1uC93PrkdjKvOiXD'

# Gets the URLS from the playlist
playlist = pytubefix.Playlist(PLAYLIST_URL)
print('Number Of Videos In playlist: %s' % len(playlist.video_urls))

# Downloads each video
for video in playlist:

    if(not utils.checkFile(video, DOWNLOAD_PATH)):
        utils.downloadYoutubeAudio(video, DOWNLOAD_PATH)