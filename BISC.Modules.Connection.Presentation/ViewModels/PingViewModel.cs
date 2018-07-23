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
using System.Collections.Specialized;
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
        private int sizeLastConnectionColection = 10; // Количество отображаемых IP
        private int[] _iP = new int[4];
        #endregion

        #region Citor
        public PingViewModel(ICommandFactory commandFactory, IConfigurationService configurationService,
            IPingItemsViewModelFactory pingItemsViewModelFactory)
        {
            setItem = ((x) => SelectedItemm = x.IP);
            deleteItem = (
                (x) =>
                {
                    LastConnections.Remove(x);
                    SaveChangesInConfig();
                });
            _pingItemsViewModelFactory = pingItemsViewModelFactory;
            _configurationService = configurationService;
            LastConnections = _pingItemsViewModelFactory.GetPingViewModelCollection(_configurationService.LastIpAddresses, setItem, deleteItem);
            _commandFactory = commandFactory;
            PingCommand = _commandFactory.CreatePresentationCommand(OnPingCommand);
            ClearSelectedIPCommand = _commandFactory.CreatePresentationCommand(OnClearSelectedIPCommand);
            PingAllCommand = _commandFactory.CreatePresentationCommand(OnPingAllCommand);
            OnClearSelectedIPCommand();
            ChengCollection();
        }
        #endregion

        #region private methods
        private async void OnPingCommand()
        {
            var toBeRemoved = LastConnections.FirstOrDefault((x) => SelectedItemm == x.IP);
            LastConnections.Remove(toBeRemoved);
            var newItem = _pingItemsViewModelFactory.GetPingItemViewModel(SelectedItemm, setItem, deleteItem);
            LastConnections.Insert(0, newItem);
            ChengCollection();
            SaveChangesInConfig();
            await newItem.OnPing();

        }

        private void SaveChangesInConfig()
        {
            var lastOpenedFiles = new List<string>();
            foreach (var lastOpenedFile in LastConnections)
            {
                lastOpenedFiles.Add(lastOpenedFile.IP);
            }

            _configurationService.LastIpAddresses = lastOpenedFiles;
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

        private void SetStringIP( )
        {
            SelectedItemm = _iP[0].ToString() + '.' + _iP[1].ToString() + '.' + _iP[2].ToString() + '.' + _iP[3].ToString();
        }

        private void SetIntIP()
        {
            string [] ips =  SelectedItemm.Split('.');
            for (int i = 0; i < 4; i++)
                _iP[i] = Convert.ToInt32(ips[i]); 
        }

        private void ChengCollection()
        {
            if (LastConnections.Count <= sizeLastConnectionColection)
                return;
            for (int i = sizeLastConnectionColection; i < LastConnections.Count; i++)
                LastConnections.RemoveAt(i);
        }
        #endregion

        #region Implementation of IPingViewModel
        
        public ObservableCollection<IPingItemViewModel> LastConnections { get; }
        public ICommand PingCommand { get; }

        public ICommand PingAllCommand { get; }

        public ICommand ClearSelectedIPCommand { get; }
        public string SelectedItemm
        {
            get { return _selectedItemm; }
            set
            {
                _selectedItemm = value;
                SetIntIP();
                OnPropertyChanged();
            }
        }

        public int IP0
        {
            get
            {
                return _iP[0];
            }
            set
            {
                _iP[0] = value;
                SetStringIP();
                OnPropertyChanged();
            }
        }

        public int IP1
        {
            get
            {
                return _iP[1];
            }
            set
            {
                _iP[1] = value;
                SetStringIP();
                OnPropertyChanged();
            }
        }

        public int IP2
        {
            get
            {
                return _iP[2];
            }
            set
            {
                _iP[2] = value;
                SetStringIP();
                OnPropertyChanged();
            }
        }

        public int IP3
        {
            get
            {
                return _iP[3];
            }
            set
            {
                _iP[3] = value;
                SetStringIP();
                OnPropertyChanged();
            }
        }

        #endregion

    }
}
