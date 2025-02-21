import os
import pytubefix


def downloadYoutubeAudio(video_url, path):
    '''Downloads youtube audio'''
    try:
        yt = pytubefix.YouTube(video_url)
        title = yt.title
        output = yt.streams.get_audio_only()
        output.download(filename=f"{title}.mp3", output_path=path)
        print(f'Downloaded {title}')
    except:
        print('Unable to download file')

def checkFile(video_url, path):
    '''Checks if the file exsists'''
    try:
        yt = pytubefix.YouTube(video_url)
        title = yt.title
        file_name = path + "\\" + title +".mp3"
        print(f'Checking: {title}')

        if os.path.exists(file_name):
            return True
        else:
            return False
    except:
        return False

