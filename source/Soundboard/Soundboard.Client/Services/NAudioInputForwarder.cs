using NAudio.CoreAudioApi;
using NAudio.Wave;
using Soundboard.Lib.Contracts;

namespace Soundboard.Client.Services
{
    public class NAudioInputForwarder : IAudioInputForwarder
    {
        private BufferedWaveProvider _bufferedWaveProvider;
        private WasapiCapture _recorder;
        private WasapiOut _player;
        public void StartForwarding()
        {
            _recorder = new WasapiCapture();
            _bufferedWaveProvider = new BufferedWaveProvider(_recorder.WaveFormat);
            _recorder.DataAvailable += OnRecorderDataAvailable;

            _player = new WasapiOut(AudioClientShareMode.Shared, true, 0);
            _player.Init(_bufferedWaveProvider);

            _player.Play();
            _recorder.StartRecording();
        }

        private void OnRecorderDataAvailable(object sender, WaveInEventArgs e)
        {
            _bufferedWaveProvider?.AddSamples(e.Buffer, 0, e.BytesRecorded);
        }

        public void StopForwarding()
        {
            _recorder?.StopRecording();
            _player?.Stop();

            _recorder?.Dispose();
            _player?.Dispose();
        }
    }
}
