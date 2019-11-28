using System.Windows.Input;

namespace BISC.Modules.Device.Presentation.Interfaces
{
    public interface IFileViewModel
    {
        string FullPath { get; set; }
        string ShortPath { get; set; }
        ICommand OpenFile { get; }
        bool IsFileExists { get; set; }
        bool IsAddingFileProcess { get; set; }
        bool IsAddingDevicesProcess { get; set; }
    }
}