using BISC.Modules.Connection.Presentation.Interfaces.Ping;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel;

namespace BISC.Modules.Connection.Presentation.Interfaces.Factorys
{
    public interface IIpAddressViewModelFactory
    {
        IIpAddressViewModel GetPingItemViewModel(string ip = "...", bool isReadonly = false, bool? initialState = null);
        ObservableCollection<IIpAddressViewModel> GetPingViewModelReadonlyCollection(List<string> ips);
    }
}
