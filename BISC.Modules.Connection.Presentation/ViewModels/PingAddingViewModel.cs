using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Infrastructure.DeviceConnection;
using BISC.Modules.Connection.Infrastructure.Services;
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
    public class PingAddingViewModel : DisposableViewModelBase, IPingAddingViewModel
    {

        private ICommandFactory _commandFactory;
        private IConfigurationService _configurationService;
        private IPingService _pingService;
        private string ip;
        private bool isPing;

        public PingAddingViewModel( ICommandFactory commandFactory, IConfigurationService configurationService, IPingService pingService )
        {
            _pingService = pingService;
            _configurationService = configurationService;
            LastConnections = new ObservableCollection<string>(_configurationService.LastIpAddresses);
            _commandFactory = commandFactory;
            PingCommand = _commandFactory.CreateDelegateCommand(OnPingCommand);
        }

        private async void OnPingCommand()
        {
            IsPing = await _pingService.GetPing(ip);

        }


        #region Implementation of IPingAddingViewModel
        public ICommand AddIpCommand { get; }

        public ICommand PingCommand { get; }

        public ObservableCollection<string> LastConnections { get; }
        public string IP
        {
            get { return ip; }
            set
            {
                ip = value;
                OnPropertyChanged();
            }
        }

        public bool IsPing
        {
            get { return isPing; }
            set
            {
                isPing = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
