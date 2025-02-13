﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel;

namespace BISC.Modules.Device.Presentation.Interfaces
{
public interface IDeviceConnectingViewModel
    {
        IIpAddressViewModel SelectedIpAddressViewModel { get; set; }
        ILastIpAddressesViewModel LastConnectedIps { get; }
        ICommand ConnectDeviceCommand { get; }
        bool IsDeviceConnectionFailed { get; }
        bool IsConnectionProcess { get; set; }
    }
}