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

namespace BISC.Modules.Connection.Presentation.ViewModels
{
    public class PingViewModel : ViewModelBase, IPingViewModel
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
            CurrentAddressViewModel = _ipAddressViewModelFactory.GetPingItemViewModel("");
           
            _commandFactory = commandFactory;
            PingAllCommand = _commandFactory.CreatePresentationCommand(OnPingAllCommand, () => _pingAllCanExecute);
            _globalEventsService.Subscribe<IpPingedEvent>((ipPinged => OnIpPinged(ipPinged.Ip)));
        }
        #endregion

        #region private methods

        private void OnIpPinged(string ip)
        {
            if (_configurationService.LastIpAddresses.Contains(ip))
            {
                _configurationService.LastIpAddresses.Remove(ip);
                _configurationService.LastIpAddresses.Insert(0, ip);
            }
            else
            {
                if (_configurationService.LastIpAddresses.Count >= _sizeLastConnectionCollection)
                {
                    _configurationService.LastIpAddresses.Remove(_configurationService.LastIpAddresses.Last());
                }
                _configurationService.LastIpAddresses.Add(ip);

            }
            _configurationService.LastIpAddresses = _configurationService.LastIpAddresses;

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
                LastIpAddresses.Add(existing);
            }
            _configurationService.LastIpAddresses = _configurationService.LastIpAddresses;
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
                    connection.PingCommand.Execute(null);
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



        #endregion

        #region Overrides of ViewModelBase

        protected override void OnDisposing()
        {
            _globalEventsService.Unsubscribe<IpPingedEvent>((ipPinged => OnIpPinged(ipPinged.Ip)));
            base.OnDisposing();
        }

        #endregion
    }
}
