using Microsoft.Win32;
using Soundboard.Lib.Contracts;

namespace Soundboard.Client.Services
{
    public class Win32FilePicker : IFilePicker
    {
        public string PickFile()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            return openFileDialog.FileName;
        }
    }
}
