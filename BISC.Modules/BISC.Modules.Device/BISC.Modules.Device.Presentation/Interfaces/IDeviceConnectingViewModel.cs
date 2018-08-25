using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel;

namespace BISC.Modules.Device.Presentation.Interfaces
{
    public interface IDeviceConnectingViewModel
    {
        IIpAddressViewModel SelectedIpAddressViewModel { get; set; }
        ObservableCollection<IIpAddressViewModel> LastConnectedIps { get; }
        ICommand ConnectDeviceCommand { get; }
        bool IsDeviceConnectionSucceed { get; }
    }
}