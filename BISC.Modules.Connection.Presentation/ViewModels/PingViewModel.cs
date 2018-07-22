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
        private IPingItemsViewModelFactory _pingItemsViewModelFactory;
        private string _selectedItemm;
        private Action<IPingItemViewModel> setItem;
        private Action<IPingItemViewModel> deleteItem;
        #endregion

        #region Citor
        public PingViewModel(ICommandFactory commandFactory, IConfigurationService configurationService,
            IPingItemsViewModelFactory pingItemsViewModelFactory)
        {
            setItem = ((x) => SelectedItemm = x.IP);
            deleteItem = ((x) => LastConnections.Remove(x));
            _pingItemsViewModelFactory = pingItemsViewModelFactory;
            _configurationService = configurationService;
            LastConnections = _pingItemsViewModelFactory.GetPingViewModelCollection(_configurationService.LastIpAddresses, setItem, deleteItem);
            _commandFactory = commandFactory;
            PingCommand = _commandFactory.CreatePresentationCommand(OnPingCommand);
            ClearSelectedIPCommand = _commandFactory.CreatePresentationCommand(OnClearSelectedIPCommand);
            PingAllCommand = _commandFactory.CreatePresentationCommand(OnPingAllCommand);
            OnClearSelectedIPCommand();
        }
        #endregion

        #region private methods
        private async void OnPingCommand()
        {
            var toBeRemoved = LastConnections.FirstOrDefault((x) => SelectedItemm == x.IP);
            LastConnections.Remove(toBeRemoved);
            var newItem = _pingItemsViewModelFactory.GetPingItemViewModel(SelectedItemm, setItem, deleteItem);
            LastConnections.Insert(0, newItem);
            await newItem.OnPing();
        }

        private async void OnPingAllCommand()
        {
            Task[] tasks = new Task[LastConnections.Count];
            for (int i = 0; i < LastConnections.Count; i++)
                tasks[i] = LastConnections[i].OnPing();
            await Task.WhenAll(tasks);
        }

    private void OnClearSelectedIPCommand()
        {
            SelectedItemm = "1.0.0.0";
        }
        #endregion

        public string SelectedItemm
        {
            get { return _selectedItemm; }
            set
            {
                _selectedItemm = value;
                OnPropertyChanged();
            }
        }

        #region Implementation of IPingAddingViewModel
        public ObservableCollection<IPingItemViewModel> LastConnections { get; }
        public ICommand PingCommand { get; }

        public ICommand PingAllCommand { get;}

        public ICommand ClearSelectedIPCommand { get; }
        
        #endregion

    }
}
