from yt_dlp import YoutubeDL

URLS = ['https://www.youtube.com/watch?v=AF2MqFnPotc']
with YoutubeDL() as ydl:
    ydl.download(URLS)