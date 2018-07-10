using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using BISC.Modules.Device.Presentation.Interfaces.Factories;
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Modules.Device.Presentation.Interfaces
{
    public interface IDeviceAddingViewModel
    {
        ICommand OpenFileWithDevices { get; }
         ICommand DeleteFileFromView { get; }
        ICommand AddSelectedDevices { get; }
        ICommand LoadDevicesFromFile { get; }

        ObservableCollection<IFileViewModel> LastOpenedFiles { get; }
        ObservableCollection<IDeviceViewModel> CurrentDevicesToAdd { get; }
    }







}