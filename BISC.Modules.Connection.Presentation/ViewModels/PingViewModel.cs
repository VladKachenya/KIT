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

namespace BISC.Modules.Connection.Presentation.ViewModels
{
    public class PingViewModel : ViewModelBase, IPingViewModel
    {
        #region private filds
        private ICommandFactory _commandFactory;
        private IConfigurationService _configurationService;
        private IPingItemsViewModelFactory _pingItemsViewModelFactory;
        private IIpValidationService _ipValidationService;
        private Action<IPingItemViewModel> setItem;
        private Action<IPingItemViewModel> deleteItem;
        private int sizeLastConnectionColection = 20; // Количество IP
        private string[] _iP = new string[4];
        #endregion

        #region Citor
        public PingViewModel(ICommandFactory commandFactory, IConfigurationService configurationService,
            IPingItemsViewModelFactory pingItemsViewModelFactory, IIpValidationService ipValidationService)
        {
            setItem = (
                (x) => 
                {
                    SetIntIP(x.IP);
                });

            deleteItem = (
                (x) =>
                {
                    LastConnections.Remove(x);
                    SaveChangesInConfig();
                });

            _ipValidationService = ipValidationService;
            _pingItemsViewModelFactory = pingItemsViewModelFactory;
            _configurationService = configurationService;
            LastConnections = _pingItemsViewModelFactory.GetPingViewModelCollection(_configurationService.LastIpAddresses, setItem, deleteItem);
            _commandFactory = commandFactory;
            PingCommand = _commandFactory.CreatePresentationCommand(OnPingCommand, OnPingCanExecute);
            ClearSelectedIPCommand = _commandFactory.CreatePresentationCommand(OnClearSelectedIPCommand);
            PingAllCommand = _commandFactory.CreatePresentationCommand(OnPingAllCommand);
            OnClearSelectedIPCommand();
            ChengCollection();
        }
        #endregion

        #region private methods
        private async void OnPingCommand()
        {
            var toBeRemoved = LastConnections.FirstOrDefault((x) => FullIp == x.IP);     
            var newItem = _pingItemsViewModelFactory.GetPingItemViewModel(FullIp, setItem, deleteItem);
            await newItem.OnPing();
            LastConnections.Remove(toBeRemoved);
            LastConnections.Insert(0, newItem);
            ChengCollection();
            SaveChangesInConfig();
        }

        private bool OnPingCanExecute()
        {
            return _ipValidationService.IsSimplifiedIpAddress(FullIp);
        }

        private async void OnPingAllCommand()
        {
            Task[] tasks = new Task[LastConnections.Count];
            for (int i = 0; i < LastConnections.Count; i++)
                tasks[i] = LastConnections[i].OnPing();
            await Task.WhenAll(tasks);
        }

        /// Ту необходимо обновлять кнопку при доступности.
        private void OnClearSelectedIPCommand()
        {
            IP0 = String.Empty;
            IP1 = String.Empty;
            IP2 = String.Empty;
            IP3 = String.Empty;
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

       

        private void SetIntIP(string IP)
        {
            string [] ips = IP.Split('.');
            IP0 = ips[0];
            IP1 = ips[1];
            IP2 = ips[2];
            IP3 = ips[3];

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
        public string FullIp
        {
            get { return IP0 + '.' + IP1 + '.' + IP2 + '.' + IP3; }
        }

        public string IP0
        {
            get
            {
                return _iP[0];
            }
            set
            {
                int intVal = 0;
                if (int.TryParse(value, out intVal))
                {
                    if ((intVal <= 255) && (intVal >= 1)) _iP[0] = intVal.ToString();
                    else if (intVal > 255) _iP[0] = "255";
                    else if (intVal < 1) _iP[0] = "1";
                }
                else if (String.IsNullOrEmpty(value))
                {
                    _iP[0] = string.Empty;
                }
                (PingCommand as IPresentationCommand).RaiseCanExecute();
                OnPropertyChanged();
            }
        }

        public string IP1
        {
            get
            {
                return _iP[1];
            }
            set
            {
                int intVal = 0;
                if (int.TryParse(value, out intVal))
                {
                    if ((intVal <= 255) && (intVal >= 0)) _iP[1] = intVal.ToString();
                    else if (intVal > 255) _iP[1] = "255";
                    else if (intVal < 0) _iP[1] = "0";
                }
                else if (String.IsNullOrEmpty(value))
                {
                    _iP[1] = string.Empty;
                }
                (PingCommand as IPresentationCommand).RaiseCanExecute();
                OnPropertyChanged();
            }
        }

        public string IP2
        {
            get
            {
                return _iP[2];
            }
            set
            {
                int intVal = 0;
                if (int.TryParse(value, out intVal))
                {
                    if ((intVal <= 255) && (intVal >= 0)) _iP[2] = intVal.ToString();
                    else if (intVal > 255) _iP[2] = "255";
                    else if (intVal < 0) _iP[2] = "0";
                }
                else if (String.IsNullOrEmpty(value))
                {
                    _iP[2] = string.Empty;
                }
                (PingCommand as IPresentationCommand).RaiseCanExecute();
                OnPropertyChanged();
            }
        }

        public string IP3
        {
            get
            {
                return _iP[3];
            }
            set
            {
                int intVal = 0;
                if (int.TryParse(value, out intVal))
                {
                    if ((intVal <= 255) && (intVal >= 0)) _iP[3] = intVal.ToString();
                    else if (intVal > 255) _iP[3] = "255";
                    else if (intVal < 0) _iP[3] = "0";
                }
                else if (String.IsNullOrEmpty(value))
                {
                    _iP[3] = string.Empty;
                }
                (PingCommand as IPresentationCommand).RaiseCanExecute();
                OnPropertyChanged();
            }
        }

        #endregion

    }
}
