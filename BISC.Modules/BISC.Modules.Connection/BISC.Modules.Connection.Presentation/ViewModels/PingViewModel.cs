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
        private bool PingAllCanExecute = true;
        private int sizeLastConnectionColection = 20; // Количество IP
        private string[] _iP = new string[4];
        #endregion

        #region Citor
        public PingViewModel(ICommandFactory commandFactory, IConfigurationService configurationService,
            IIpAddressViewModelFactory ipAddressViewModelFactory, IIpValidationService ipValidationService, IGlobalEventsService globalEventsService)
        {

            _ipValidationService = ipValidationService;
            _globalEventsService = globalEventsService;
            _ipAddressViewModelFactory = ipAddressViewModelFactory;
            _configurationService = configurationService;
            LastIpAddresses = _ipAddressViewModelFactory.GetPingViewModelCollection(_configurationService.LastIpAddresses);
            _commandFactory = commandFactory;
            PingAllCommand = _commandFactory.CreatePresentationCommand(OnPingAllCommand, () => PingAllCanExecute);
            _globalEventsService.Subscribe<IpPingedEvent>((ipPinged => OnIpPinged(ipPinged.Ip)));
        }
        #endregion

        #region private methods

        private void OnIpPinged(string ip)
        {

        }

        private async void OnPingAllCommand()
        {
            try
            {
                PingAllCanExecute = false;
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
                PingAllCanExecute = true;
                (PingAllCommand as IPresentationCommand).RaiseCanExecute();
            }
            //Task[] tasks = new Task[LastConnections.Count];
            //for (int i = 0; i < LastConnections.Count; i++)
            //    tasks[i] = LastConnections[i].OnPing();
            //await Task.WhenAll(tasks);
        }


        private void SaveChangesInConfig()
        {
            var lastOpenedFiles = new List<string>();
            foreach (var lastOpenedFile in LastIpAddresses)
            {
                lastOpenedFiles.Add(lastOpenedFile.FullIp);
            }

            _configurationService.LastIpAddresses = lastOpenedFiles;
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
