using BISC.Infrastructure.Global.IoC;
using BISC.Modules.Connection.Presentation.Interfaces.Factorys;
using BISC.Modules.Connection.Presentation.Interfaces.Ping;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Connection.Presentation.Factorys
{
    public class PingItemsViewModelFactory : IPingItemsViewModelFactory
    {
        #region private filds
        private IInjectionContainer _injectionContainer;

        #endregion

        #region Citor
        public PingItemsViewModelFactory(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }
        #endregion

        #region Implementation of IPingItemsViewModelFactory
        public IPingItemViewModel GetPingItemViewModel(string IP, Action<string> ItemClickAction)
        {
            IPingItemViewModel newItem = _injectionContainer.ResolveType<IPingItemViewModel>();
            newItem.IP = IP;
            newItem.SetAsSelectedIP = ItemClickAction;
            return newItem;
        }

        public ObservableCollection<IPingItemViewModel> GetPingViewModelCollection(List<string> IPs, Action<string> ItemClickAction)
        {
            ObservableCollection<IPingItemViewModel> collection = new ObservableCollection<IPingItemViewModel>();
            foreach (string ip in IPs)
                collection.Add(GetPingItemViewModel(ip, ItemClickAction));
            return collection;
        }
        #endregion
    }
}
