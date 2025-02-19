import os
import pytube
import pytube.innertube
from yt_dlp import YoutubeDL

DOWNLOAD_PATH = ''
PLAYLIST_URL = 'https://www.youtube.com/playlist?list=PLJeE3c06PNfzXdsUY1uC93PrkdjKvOiXD'

def downloadYouTube(videourl, path):
    yt = pytube.YouTube(videourl)
    yt = yt.streams.filter(progressive=True, file_extension='mp4').order_by('resolution').desc().first()
    if not os.path.exists(path):
        os.makedirs(path)
    yt.download(path)

def video_to_audio(file_name):
    file_without_ext = file_name.split['.'][0]
    command = f"ffmpeg -i {file_name} {file_without_ext}.mp3"
    os.system(command)
    os.remove(file_name)

# Gets the URLS from the playlist
playlist = pytube.Playlist(PLAYLIST_URL)
print('Number Of Videos In playlist: %s' % len(playlist.video_urls))

# Downloads each video
for video in playlist:
    # with YoutubeDL() as ydl:
    #     ydl.download(video)
    downloadYouTube(video, '')



# URLS = ['https://www.youtube.com/watch?v=AF2MqFnPotc']
# with YoutubeDL() as ydl:
#     ydl.download(URLS)




