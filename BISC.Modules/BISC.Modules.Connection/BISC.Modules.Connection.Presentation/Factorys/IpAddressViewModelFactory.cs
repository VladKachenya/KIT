using BISC.Infrastructure.Global.IoC;
using BISC.Modules.Connection.Presentation.Interfaces.Factorys;
using BISC.Modules.Connection.Presentation.Interfaces.Ping;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.Connection.Presentation.Factorys
{
    public class IpAddressViewModelFactory : IIpAddressViewModelFactory
    {
        private readonly Func<IIpAddressViewModel> _ipAddressViewModelFactory;


        #region Citor
        public IpAddressViewModelFactory(Func<IIpAddressViewModel> _ipAddressViewModelFactory)
        {
            this._ipAddressViewModelFactory = _ipAddressViewModelFactory;
        }
        #endregion


        #region Implementation of IPingItemsViewModelFactory

        public IIpAddressViewModel GetPingItemViewModel(string ip = "...", bool isReadonly = false, bool? initialState = null)
        {
            IIpAddressViewModel ipAddressViewModel = _ipAddressViewModelFactory();
            (ipAddressViewModel as ComplexViewModelBase)?.SetIsEditable(!isReadonly);
            ipAddressViewModel.FullIp = ip;
            if (initialState != null) { ipAddressViewModel.IsPingSuccess = initialState; }
            return ipAddressViewModel;
        }

        public ObservableCollection<IIpAddressViewModel> GetPingViewModelReadonlyCollection(List<string> ips)
        {
            ObservableCollection<IIpAddressViewModel> ipAddressViewModels = new ObservableCollection<IIpAddressViewModel>();
            foreach (var ip in ips)
            {
                var newIpAddressViewModel = GetPingItemViewModel(ip, true);
                ipAddressViewModels.Add(newIpAddressViewModel);
            }
            return ipAddressViewModels;
        }

        #endregion
    }
}
