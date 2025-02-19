import os
import pytube
import pytube.innertube
from yt_dlp import YoutubeDL
import utils

DOWNLOAD_PATH = ''
PLAYLIST_URL = 'https://www.youtube.com/playlist?list=PLJeE3c06PNfzXdsUY1uC93PrkdjKvOiXD'

# Gets the URLS from the playlist
playlist = pytube.Playlist(PLAYLIST_URL)
print('Number Of Videos In playlist: %s' % len(playlist.video_urls))

# Downloads each video
for video in playlist:
    # with YoutubeDL() as ydl:
    #     ydl.download(video)
    utils.downloadYouTube(video, '')




