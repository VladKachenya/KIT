﻿using System.Windows.Input;
using BISC.Presentation.Infrastructure.Tree;

namespace BISC.Modules.Device.Presentation.Interfaces
{
    public interface IDeviceTreeItemViewModel
    {
        ICommand DeleteDeviceCommand { get; }
        ICommand NavigateToDetailsCommand { get;  }
        ICommand ResetDeviceViaFtpCommand { get; }

    }
}