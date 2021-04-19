using NAudio.Wave;
using Soundboard.Lib.Contracts;

namespace Soundboard.Client.Services
{
    public class NAudioFilePlayer : IAudioFilePlayer
    {
        public int OutputDeviceNumber { get; set; }
        public event PlaybackStopped PlaybackStopped;

        private AudioFileReader _audioFileReader;
        private WaveOut _waveOut;

        public void StartPlaying(string fileName)
        {
            _audioFileReader = new AudioFileReader(fileName);
            _waveOut = new WaveOut
            {
                DeviceNumber = OutputDeviceNumber
            };
            _waveOut.PlaybackStopped += OnPlayBackStopped;
            _waveOut.Init(_audioFileReader);
            _waveOut.Play();
        }

        private void OnPlayBackStopped(object sender, StoppedEventArgs e)
        {
            PlaybackStopped?.Invoke();
            _waveOut?.Dispose();
            _audioFileReader?.Dispose();
        }

        public void StopPlaying()
        {
            _waveOut?.Stop();
        }
    }
}
