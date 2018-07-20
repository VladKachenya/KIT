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
    public class PingViewModel : ViewModelBase, IPingViewModel
    {
        #region private filds
        private ICommandFactory _commandFactory;
        private IConfigurationService _configurationService;
        private IPingService _pingService;
        private IPingItemsViewModelFactory _pingItemsViewModelFactory;
        private string _selectedIP;
        private bool isPing;
        private Action<string> setItem;
        private Action<IPingItemViewModel> DeleteItem;
        #endregion

        #region Citor
        public PingViewModel(ICommandFactory commandFactory, IConfigurationService configurationService, IPingService pingService,
            IPingItemsViewModelFactory pingItemsViewModelFactory)
        {
            setItem = (x) => SelectedIP = x;
            DeleteItem = ((x) => LastConnections.Remove(x));
            _pingService = pingService;
            _configurationService = configurationService;
            _pingItemsViewModelFactory = pingItemsViewModelFactory;
            LastConnections = _pingItemsViewModelFactory.GetPingViewModelCollection(_configurationService.LastIpAddresses, setItem, DeleteItem);
            _commandFactory = commandFactory;
            PingCommand = _commandFactory.CreatePresentationCommand(OnPingCommand);
            ClearSelectedIPCommand = _commandFactory.CreatePresentationCommand(OnClearSelectedIPCommand);
            TestCommand = _commandFactory.CreatePresentationCommand(OnDeleteCommand);

            OnClearSelectedIPCommand();
        }
        #endregion

        #region private methods
        private async void OnPingCommand()
        {
            var newItem = _pingItemsViewModelFactory.GetPingItemViewModel(SelectedIP, (x) => SelectedIP = x, DeleteItem);
            IsPing = await _pingService.GetPing(SelectedIP);
            newItem.IsPing = IsPing;
            LastConnections.Add(newItem);
        }

        private void OnDeleteCommand()
        {
            DeleteItem.Invoke(LastConnections[0]);
        }

    private void OnClearSelectedIPCommand()
        {
            SelectedIP = "1.0.0.0";
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

        public ICommand TestCommand { get;}

        public ICommand ClearSelectedIPCommand { get; }
        #endregion

    }
}
