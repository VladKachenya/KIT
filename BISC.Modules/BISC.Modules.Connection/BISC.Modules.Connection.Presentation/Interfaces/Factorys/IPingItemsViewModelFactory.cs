using BISC.Modules.Connection.Presentation.Interfaces.Ping;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Connection.Presentation.Interfaces.Factorys
{
    public interface IPingItemsViewModelFactory
    {
        IPingItemViewModel GetPingItemViewModel(string IP, Action<IPingItemViewModel> ItemClickAction, Action<IPingItemViewModel> RemoveItem);
        ObservableCollection<IPingItemViewModel> GetPingViewModelCollection(List<string> IPs, Action<IPingItemViewModel> ItemClickAction, Action<IPingItemViewModel> RemoveItem);
    }
}
