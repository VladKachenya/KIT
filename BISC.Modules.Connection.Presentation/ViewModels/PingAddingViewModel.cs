using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Infrastructure.DeviceConnection;
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

        public PingAddingViewModel( ICommandFactory commandFactory, IConfigurationService configurationService )
        {
            _configurationService = configurationService;
            LastConnections = new ObservableCollection<string>(_configurationService.LastIpAddresses);
            _commandFactory = commandFactory;
            AddIpCommand = _commandFactory.CreateDelegateCommand(OnTestCommand);
        }

        private void OnTestCommand()
        {
           
        }


        #region Implementation of IPingAddingViewModel
        public ICommand AddIpCommand { get; }

        public ICommand PingCommand { get; }

        public ObservableCollection<string> LastConnections { get; }
        #endregion
    }
}
