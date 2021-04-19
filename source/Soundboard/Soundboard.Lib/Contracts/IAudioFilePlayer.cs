namespace Soundboard.Lib.Contracts
{
    public delegate void PlaybackStopped();
    public interface IAudioFilePlayer
    {
        void StartPlaying(string fileName);
        void StopPlaying();
        event PlaybackStopped PlaybackStopped;
    }
}
