namespace Soundboard.Lib.Contracts
{
    public interface IAudioInputForwarder
    {
        void StartForwarding();
        void StopForwarding();
    }
}