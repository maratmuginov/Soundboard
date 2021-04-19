using System.IO;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Soundboard.Lib.Contracts;

namespace Soundboard.Client.ViewModels
{
    public class SoundboardViewModel : ObservableObject
    {
        private readonly IAudioFilePlayer _audioFilePlayer;
        private readonly IFilePicker _filePicker;
        private string _fileName;
        private bool _isPlayerFree = true;

        public string FileName
        {
            get => _fileName;
            set
            {
                SetProperty(ref _fileName, value);
                StartPlayingCommand.NotifyCanExecuteChanged();
            }
        }
        public bool IsPlayerFree
        {
            get => _isPlayerFree;
            private set
            {
                SetProperty(ref _isPlayerFree, value);
                StartPlayingCommand.NotifyCanExecuteChanged();
            }
        }

        public RelayCommand StartPlayingCommand { get; }
        public RelayCommand StopPlayingCommand { get; }
        public RelayCommand PickFileCommand { get; }
        public SoundboardViewModel(IAudioFilePlayer audioFilePlayer, IFilePicker filePicker)
        {
            _audioFilePlayer = audioFilePlayer;
            _filePicker = filePicker;
            StartPlayingCommand = new RelayCommand(StartPlaying, CanStartPlaying);
            StopPlayingCommand = new RelayCommand(StopPlaying);
            PickFileCommand = new RelayCommand(PickFile);
        }

        private bool CanStartPlaying()
        {
            return _isPlayerFree && File.Exists(_fileName);
        }

        private void StopPlaying()
        {
            _audioFilePlayer.StopPlaying();
            IsPlayerFree = true;
        }

        private void StartPlaying()
        {
            IsPlayerFree = false;
            _audioFilePlayer.StartPlaying(_fileName);
        }

        private void PickFile()
        {
            string fileName = _filePicker.PickFile();

            if (string.IsNullOrEmpty(fileName))
                return;

            FileName = fileName;
        }

    }
}
