using BISC.Infrastructure.Global.Constants;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Presentation.Interfaces.Factorys;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Connection.Presentation.Factorys
{
    public class LastIpAddressesViewModelFactory : ILastIpAddressesViewModelFactory
    {
        private IInjectionContainer _injectionContainer;
        private readonly IConfigurationService _configurationService;
        private readonly IIpAddressViewModelFactory _ipAddressViewModelFactory;

        public LastIpAddressesViewModelFactory(IInjectionContainer injectionContainer, IConfigurationService configurationService,
        IIpAddressViewModelFactory ipAddressViewModelFactory)
        {
            _injectionContainer = injectionContainer;
            _configurationService = configurationService;
            _ipAddressViewModelFactory = ipAddressViewModelFactory;
        }
        

        public void BuildLastConnectedIpAdresses(out IIpAddressViewModel selectidAddressViewModel, out ILastIpAddressesViewModel IpViewModelCollection)
        {
            var objIp = _ipAddressViewModelFactory.GetPingItemViewModel();
            var lastConnectedIps = _configurationService.GetIpsCollection(Constants.ConfigurationServiceConstants.LastConnectedIpAddresses);
            ILastIpAddressesViewModel obj = _injectionContainer.ResolveType<ILastIpAddressesViewModel>();
            obj.ConfigurationCollectionName = Constants.ConfigurationServiceConstants.LastConnectedIpAddresses;
            var lastConnectedIp = lastConnectedIps.Count > 0 ? lastConnectedIps[0] : "...";
            objIp.FullIp = lastConnectedIp;
            obj.CurrentAddressViewModel = objIp;

            selectidAddressViewModel = objIp;
            IpViewModelCollection = obj;
        }

        public void BuildLastPingIpAdresses(out IIpAddressViewModel selectidAddressViewModel, out ILastIpAddressesViewModel IpViewModelCollection)
        {
            var objIp = _ipAddressViewModelFactory.GetPingItemViewModel();
            var lastConnectedIps = _configurationService.GetIpsCollection(Constants.ConfigurationServiceConstants.LastIpAddresses);
            ILastIpAddressesViewModel obj = _injectionContainer.ResolveType<ILastIpAddressesViewModel>();
            obj.ConfigurationCollectionName = Constants.ConfigurationServiceConstants.LastConnectedIpAddresses;
            var lastConnectedIp = lastConnectedIps.Count > 0 ? lastConnectedIps[0] : "...";
            objIp.FullIp = lastConnectedIp;
            obj.CurrentAddressViewModel = objIp;

            selectidAddressViewModel = objIp;
            IpViewModelCollection = obj;
        }
    }
}
