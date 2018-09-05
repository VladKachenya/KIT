using BISC.Modules.Connection.Presentation.Interfaces.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.FTP.Infrastructure.ViewModels
{
    public interface IFTPServiceViewModel
    {
        IIpAddressViewModel FtpIpAddressViewModel { get; set; }
        ILastIpAddressesViewModel LastIpAddressesViewModel { get; }

        string FtpPassword { get; set; }
        string FtpLogin { get; set; }
        ICommand ConnectToDeviceCommand { get; }
        ICommand ResetDeviceCommand { get; }
        ObservableCollection<IFTPActionMessage> FTPActionMessageList  { get; }
    }
}
