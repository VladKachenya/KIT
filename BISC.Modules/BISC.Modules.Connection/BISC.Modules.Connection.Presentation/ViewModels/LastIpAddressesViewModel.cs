using BISC.Infrastructure.Global.Constants;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Presentation.Events;
using BISC.Modules.Connection.Presentation.Interfaces.Factorys;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.Connection.Presentation.ViewModels
{
    public class LastIpAddressesViewModel : ComplexViewModelBase, ILastIpAddressesViewModel 
    {
        #region private filds
        private IGlobalEventsService _globalEventsService;
        private IConfigurationService _configurationService;
        private IIpAddressViewModelFactory _ipAddressViewModelFactory;
        ObservableCollection<IIpAddressViewModel> _lastIpAddresses;
        private string _configurationCollectionName;

        private int _sizeLastConnectionCollection = 100;
        #endregion

        #region C-tor
        public LastIpAddressesViewModel(IGlobalEventsService globalEventsService, IConfigurationService configurationService, 
            IIpAddressViewModelFactory ipAddressViewModelFactory, ICommandFactory commandFactory)
        {
            _globalEventsService = globalEventsService;
            _configurationService = configurationService;
            _ipAddressViewModelFactory = ipAddressViewModelFactory;
            _globalEventsService.Subscribe<IpPingedEvent>((ipPinged => OnIpPinged(ipPinged.Ip, ipPinged.PingResult)));
            _globalEventsService.Subscribe<IpSelectedEvent>(OnIpSelected);
            DeleteItemCommand = commandFactory.CreatePresentationCommand<object>(OnDeleteIpExecute);
        }


        #endregion

        #region private methods

        private void OnDeleteIpExecute(object obj)
        {
            if (obj is IIpAddressViewModel ipAddressViewModel)
            {
                LastIpAddresses.Remove(ipAddressViewModel);
                _configurationService.SetIpsCollection(ConfigurationCollectionName, LastIpAddresses.Select((model => model.FullIp)).ToList());
            }

        }

        private void OnIpSelected(IpSelectedEvent ipSelectedEvent)
        {
            if (CurrentAddressViewModel == null) return;
            CurrentAddressViewModel.FullIp = ipSelectedEvent.Ip;
            CurrentAddressViewModel.IsPingSuccess = null;

        }

        private void OnIpPinged(string ip, bool result)
        {
            if (CurrentAddressViewModel == null) return;
            if (CurrentAddressViewModel.FullIp != ip) return;
            var lastIps = _configurationService.GetIpsCollection(ConfigurationCollectionName).ToList();
            if (lastIps.Contains(ip))
            {
                lastIps.Remove(ip);
                lastIps.Insert(0, ip);
            }
            else
            {
                if (lastIps.Count >= _sizeLastConnectionCollection)
                {
                    lastIps.Remove(lastIps.Last());
                }

                lastIps.Add(ip);

            }

            _configurationService.SetIpsCollection(ConfigurationCollectionName, lastIps);

            var existing = LastIpAddresses.FirstOrDefault((model => model.FullIp == ip));
            if (existing != null)
            {
                LastIpAddresses.Remove(existing);
                existing.IsPingSuccess = result;
                LastIpAddresses.Insert(0, existing);

            }
            else
            {
                if (LastIpAddresses.Count >= _sizeLastConnectionCollection)
                {
                    LastIpAddresses.Remove(LastIpAddresses.Last());
                }
                LastIpAddresses.Insert(0, _ipAddressViewModelFactory.GetPingItemViewModel(ip, true, result));
            }


        }
        #endregion

        #region Implementation of ILastIpAddressesViewModel
        public ObservableCollection<IIpAddressViewModel> LastIpAddresses
        {
            get => _lastIpAddresses;
            private set => SetProperty(ref _lastIpAddresses, value);
        }

        public ICommand DeleteItemCommand { get; }
        public IIpAddressViewModel CurrentAddressViewModel { get; set; }
        public string ConfigurationCollectionName
        {
            set
            {
                _configurationCollectionName = value;
                LastIpAddresses = _ipAddressViewModelFactory.GetPingViewModelReadonlyCollection(_configurationService.GetIpsCollection(ConfigurationCollectionName));
            }
            get
            {
                return _configurationCollectionName;
            }
        }
        #endregion

        #region Overrides of ViewModelBase

        protected override void OnDisposing()
        {
            _globalEventsService.Unsubscribe<IpSelectedEvent>(OnIpSelected);
            _globalEventsService.Unsubscribe<IpPingedEvent>((ipPinged => OnIpPinged(ipPinged.Ip, ipPinged.PingResult)));
            base.OnDisposing();
        }

        #endregion
    }
}
