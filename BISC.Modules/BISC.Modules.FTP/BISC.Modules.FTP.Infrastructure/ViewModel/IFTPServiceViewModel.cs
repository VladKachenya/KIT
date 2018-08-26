using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.FTP.Infrastructure.ViewModel
{
    public interface IFTPServiceViewModel
    {
        string FtpIpAddress { get; set; }
        string FtpPassword { get; set; }
        string FtpLogin { get; set; }
        bool IsFtpConnected { get; set; }
        ICommand ConnectToDeviceCommand { get; }
        ICommand ResetDeviceCommand { get; }
    }
}
