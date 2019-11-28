using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BISC.Modules.Device.Presentation.Interfaces
{
    public interface IDeviceFromFileAddingViewModel
    {
        ICommand OpenFileWithDevices { get; }
        ICommand DeleteFileFromView { get; }
        ICommand AddSelectedDevices { get; }
        ICommand LoadDevicesFromFile { get; }
        ObservableCollection<IFileViewModel> LastOpenedFiles { get; }
        ObservableCollection<IDeviceViewModel> CurrentDevicesToAdd { get; }
        bool IsAddingEnable { get; set; }
    }
}