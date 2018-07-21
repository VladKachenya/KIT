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
        private Action<IPingItemViewModel> setItem;
        private Action<IPingItemViewModel> deleteItem;
        #endregion

        #region Citor
        public PingViewModel(ICommandFactory commandFactory, IConfigurationService configurationService, IPingService pingService,
            IPingItemsViewModelFactory pingItemsViewModelFactory)
        {
            setItem = ((x) => SelectedItemm = x);
            deleteItem = ((x) => LastConnections.Remove(x));
            _pingItemsViewModelFactory = pingItemsViewModelFactory;
            SelectedItemm = _pingItemsViewModelFactory.GetPingItemViewModel("1.0.0.0", setItem, deleteItem);
            _pingService = pingService;
            _configurationService = configurationService;
            LastConnections = _pingItemsViewModelFactory.GetPingViewModelCollection(_configurationService.LastIpAddresses, setItem, deleteItem);
            _commandFactory = commandFactory;
            PingCommand = _commandFactory.CreatePresentationCommand(OnPingCommand);
            ClearSelectedIPCommand = _commandFactory.CreatePresentationCommand(OnClearSelectedIPCommand);
            TestCommand = _commandFactory.CreatePresentationCommand(OnDeleteCommand);
        }
        #endregion

        #region private methods
        private async void OnPingCommand()
        {
            SelectedItemm.IsPing = null;
            var toBeRemoved = LastConnections.FirstOrDefault((x) => SelectedItemm.IP == x.IP);
            LastConnections.Remove(toBeRemoved);
            var newItem = _pingItemsViewModelFactory.GetPingItemViewModel(SelectedItemm.IP, setItem, deleteItem);
            LastConnections.Insert(0, newItem);
            SelectedItemm.IsPing = await _pingService.GetPing(SelectedItemm.IP);
            newItem.IsPing = SelectedItemm.IsPing;
            
        }

        private void OnDeleteCommand()
        {
            //DeleteItem.Invoke(LastConnections[0]);
        }

    private void OnClearSelectedIPCommand()
        {
            SelectedItemm.IP = "1.0.0.0";
        }
        #endregion



        #region Implementation of IPingAddingViewModel
        public ObservableCollection<IPingItemViewModel> LastConnections { get; }
        public ICommand PingCommand { get; }

        public ICommand TestCommand { get;}

        public ICommand ClearSelectedIPCommand { get; }
        public IPingItemViewModel SelectedItemm { get; set; }
        #endregion

    }
}
