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
      
        bool IsOpeningFromFile { get; set; }
     
        ICommand SelectOpenFromFileCommand { get; }
        ICommand SelectConnectCommand { get; }
        ICommand CloseCommand { get; }
    }







}