import os
import pytube
import pytube.innertube
from yt_dlp import YoutubeDL

class Utils():

    def __init__(self):
        pass

    def downloadYouTube(videourl, path):
        '''Downloads the Youtube video and puts it to the specific path'''
        yt = pytube.YouTube(videourl)
        yt = yt.streams.filter(progressive=True, file_extension='mp4').order_by('resolution').desc().first()
        if not os.path.exists(path):
            os.makedirs(path)
        yt.download(path)

    def video_to_audio(file_name):
        '''Converts the video file to an mp3 file using ffmpeg'''
        file_without_ext = file_name.split['.'][0]
        command = f"ffmpeg -i {file_name} {file_without_ext}.mp3"
        os.system(command)
        os.remove(file_name)
