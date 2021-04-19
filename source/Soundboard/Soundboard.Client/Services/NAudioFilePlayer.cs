using NAudio.Wave;
using Soundboard.Lib.Contracts;

namespace Soundboard.Client.Services
{
    public class NAudioFilePlayer : IAudioFilePlayer
    {
        public event PlaybackStopped PlaybackStopped;
        private AudioFileReader _audioFileReader;
        private WasapiOut _player;

        public void StartPlaying(string fileName)
        {
            _audioFileReader = new AudioFileReader(fileName);
            _player = new WasapiOut();
            _player.PlaybackStopped += OnPlayBackStopped;
            _player.Init(_audioFileReader);
            _player.Play();
        }

        private void OnPlayBackStopped(object sender, StoppedEventArgs e)
        {
            PlaybackStopped?.Invoke();
            _player?.Dispose();
            _audioFileReader?.Dispose();
        }

        public void StopPlaying()
        {
            _player?.Stop();
        }
    }
}
