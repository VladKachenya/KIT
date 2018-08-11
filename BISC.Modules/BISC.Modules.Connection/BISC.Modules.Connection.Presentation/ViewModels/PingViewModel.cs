using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Infrastructure.DeviceConnection;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Connection.Presentation.Interfaces.Factorys;
using BISC.Modules.Connection.Presentation.Interfaces.Ping;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Modules.Connection.Presentation.Events;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Modules.Connection.Presentation.ViewModels
{
    public class PingViewModel : ComplexViewModelBase, IPingViewModel
    {
        #region private filds
        private ICommandFactory _commandFactory;
        private IConfigurationService _configurationService;
        private IIpAddressViewModelFactory _ipAddressViewModelFactory;
        private IIpValidationService _ipValidationService;
        private readonly IGlobalEventsService _globalEventsService;
        private bool _pingAllCanExecute = true;
        private int _sizeLastConnectionCollection = 20;
        #endregion

        #region C-tor
        public PingViewModel(ICommandFactory commandFactory, IConfigurationService configurationService,
            IIpAddressViewModelFactory ipAddressViewModelFactory, IIpValidationService ipValidationService, IGlobalEventsService globalEventsService)
        {

            _ipValidationService = ipValidationService;
            _globalEventsService = globalEventsService;
            _ipAddressViewModelFactory = ipAddressViewModelFactory;
            _configurationService = configurationService;
            LastIpAddresses = _ipAddressViewModelFactory.GetPingViewModelReadonlyCollection(_configurationService.LastIpAddresses);
            CurrentAddressViewModel = _ipAddressViewModelFactory.GetPingItemViewModel("", false);

            _commandFactory = commandFactory;
            PingAllCommand = _commandFactory.CreatePresentationCommand(OnPingAllCommand, () => _pingAllCanExecute);
            DeleteItemCommand = commandFactory.CreatePresentationCommand<object>(OnDeleteIpExecute);
            _globalEventsService.Subscribe<IpPingedEvent>((ipPinged => OnIpPinged(ipPinged.Ip,ipPinged.PingResult)));
            _globalEventsService.Subscribe<IpSelectedEvent>(OnIpSelected);
        }

        private void OnIpSelected(IpSelectedEvent ipSelectedEvent)
        {
            CurrentAddressViewModel.FullIp = ipSelectedEvent.Ip;
            CurrentAddressViewModel.IsPingSuccess = null;
            
        }

        private void OnDeleteIpExecute(object obj)
        {
            if (obj is IIpAddressViewModel ipAddressViewModel)
            {
                LastIpAddresses.Remove(ipAddressViewModel);
                _configurationService.LastIpAddresses = LastIpAddresses.Select((model => model.FullIp)).ToList();
            }
        }

        #endregion

        #region private methods

        private void OnIpPinged(string ip,bool result)
        {
            if (CurrentAddressViewModel.FullIp != ip) return;
            var lastIps = _configurationService.LastIpAddresses.ToList();
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

            _configurationService.LastIpAddresses = lastIps;
            
            var existing = LastIpAddresses.FirstOrDefault((model => model.FullIp == ip));
            if (existing != null)
            {
                LastIpAddresses.Remove(existing);
                LastIpAddresses.Insert(0, existing);
            }
            else
            {
                if (LastIpAddresses.Count >= _sizeLastConnectionCollection)
                {
                    LastIpAddresses.Remove(LastIpAddresses.Last());
                }
                LastIpAddresses.Insert(0,_ipAddressViewModelFactory.GetPingItemViewModel(ip, true,result));
            }


        }



        private async void OnPingAllCommand()
        {
            try
            {
                _pingAllCanExecute = false;
                (PingAllCommand as IPresentationCommand).RaiseCanExecute();
                List<IIpAddressViewModel> items = new List<IIpAddressViewModel>(LastIpAddresses);

                foreach (var connection in items)
                {
                    await connection.PingAsync();
                }
                items.Clear();
            }
            finally
            {
                _pingAllCanExecute = true;
                (PingAllCommand as IPresentationCommand).RaiseCanExecute();
            }
            //Task[] tasks = new Task[LastConnections.Count];
            //for (int i = 0; i < LastConnections.Count; i++)
            //    tasks[i] = LastConnections[i].OnPing();
            //await Task.WhenAll(tasks);
        }




        #endregion

        #region Implementation of IPingViewModel


        public IIpAddressViewModel CurrentAddressViewModel { get; }
        public ObservableCollection<IIpAddressViewModel> LastIpAddresses { get; }
        public ICommand PingAllCommand { get; }
        public ICommand DeleteItemCommand { get; }

        #endregion

        #region Overrides of ViewModelBase

        protected override void OnDisposing()
        {
            _globalEventsService.Unsubscribe<IpPingedEvent>((ipPinged => OnIpPinged(ipPinged.Ip,ipPinged.PingResult)));
            base.OnDisposing();
        }

        #endregion
    }
}
