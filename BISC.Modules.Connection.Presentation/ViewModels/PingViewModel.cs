using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Infrastructure.DeviceConnection;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Connection.Presentation.Interfaces.Factorys;
using BISC.Modules.Connection.Presentation.Interfaces.Ping;
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
    public class PingViewModel : DisposableViewModelBase, IPingViewModel
    {
        #region private filds
        private ICommandFactory _commandFactory;
        private IConfigurationService _configurationService;
        private IPingService _pingService;
        private IPingItemsViewModelFactory _pingItemsViewModelFactory;
        private string _selectedIP;
        private bool isPing;
        #endregion

        #region Citor
        public PingViewModel( ICommandFactory commandFactory, IConfigurationService configurationService, IPingService pingService,
            IPingItemsViewModelFactory pingItemsViewModelFactory)
        {
            _pingService = pingService;
            _configurationService = configurationService;
            _pingItemsViewModelFactory = pingItemsViewModelFactory;
            LastConnections = _pingItemsViewModelFactory.GetPingViewModelCollection(_configurationService.LastIpAddresses, (x) => SelectedIP = x);
            _commandFactory = commandFactory;
            PingCommand = _commandFactory.CreatePresentationCommand(OnPingCommand);
            ClearSelectedIPCommand = _commandFactory.CreatePresentationCommand(OnClearSelectedIPCommand);

            OnClearSelectedIPCommand();
        }
        #endregion

        #region private methods
        private async void OnPingCommand()
        {
            var newItem = _pingItemsViewModelFactory.GetPingItemViewModel(SelectedIP, (x) => SelectedIP = x);
            IsPing = await _pingService.GetPing(SelectedIP);
            newItem.IsPing = IsPing;
            LastConnections.Add(newItem);
        }

        private void OnClearSelectedIPCommand()
        {
            SelectedIP = "0.0.0.0";
        }
        #endregion



        #region Implementation of IPingAddingViewModel
        public string SelectedIP
        {
            get { return _selectedIP; }
            set
            {
                _selectedIP = value;
                OnPropertyChanged();
            }
        }  

        public ObservableCollection<IPingItemViewModel> LastConnections { get; }

        public bool IsPing
        {
            get { return isPing; }
            set
            {
                isPing = value;
                OnPropertyChanged();
            }
        }

        public ICommand PingCommand { get; }

        public ICommand ClearSelectedIPCommand { get; }
        #endregion

    }
}
