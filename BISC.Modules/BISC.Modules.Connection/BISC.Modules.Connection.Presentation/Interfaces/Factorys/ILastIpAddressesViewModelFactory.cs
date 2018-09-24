using BISC.Modules.Connection.Presentation.Interfaces.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Connection.Presentation.Interfaces.Factorys
{
    public interface ILastIpAddressesViewModelFactory
    {
        void BuildLastPingIpAdresses(out IIpAddressViewModel selectidAddressViewModel, out ILastIpAddressesViewModel IpViewModelCollection);
        void BuildLastConnectedIpAdresses(out IIpAddressViewModel selectidAddressViewModel, out ILastIpAddressesViewModel IpViewModelCollection);

    }
}
