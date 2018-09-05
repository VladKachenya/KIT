using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.Connection.Presentation.Interfaces.ViewModel
{
    public interface ILastIpAddressesViewModel
    {
        ObservableCollection<IIpAddressViewModel> LastIpAddresses { get; }
        ICommand DeleteItemCommand { get; }
        IIpAddressViewModel CurrentAddressViewModel { get; set; }
    }
}
