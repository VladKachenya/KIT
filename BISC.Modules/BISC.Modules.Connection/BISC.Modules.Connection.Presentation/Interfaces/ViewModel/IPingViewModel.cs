using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel;

namespace BISC.Modules.Connection.Presentation.Interfaces.Ping
{
    public interface IPingViewModel 
    {            
        IIpAddressViewModel CurrentAddressViewModel { get; }
        ObservableCollection<IIpAddressViewModel> LastIpAddresses  { get; }
        ICommand PingAllCommand { get; }
        ICommand DeleteItemCommand { get; }
        ICommand CloseCommand { get; }

    }
}
