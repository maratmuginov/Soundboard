using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Soundboard.Lib.Contracts;

namespace Soundboard.Client.ViewModels
{
    public class ShellViewModel : ObservableObject
    {
        private readonly IAudioInputForwarder _audioInputForwarder;
        private bool _canForward = true;
        public RelayCommand StartForwardingCommand { get; }
        public RelayCommand StopForwardingCommand { get; }
        public SoundboardViewModel SoundboardViewModel { get; }
        public bool CanForward
        {
            get => _canForward;
            set
            {
                SetProperty(ref _canForward, value);
                StartForwardingCommand.NotifyCanExecuteChanged();
            }
        }

        public ShellViewModel(IAudioInputForwarder audioInputForwarder, SoundboardViewModel soundboardViewModel)
        {
            _audioInputForwarder = audioInputForwarder;
            SoundboardViewModel = soundboardViewModel;
            StartForwardingCommand = new RelayCommand(StartForwarding, () => CanForward);
            StopForwardingCommand = new RelayCommand(StopForwarding);
        }

        private void StopForwarding()
        {
            _audioInputForwarder.StopForwarding();
            CanForward = true;
        }

        private void StartForwarding()
        {
            CanForward = false;
            _audioInputForwarder.StartForwarding();
        }
    }
}
