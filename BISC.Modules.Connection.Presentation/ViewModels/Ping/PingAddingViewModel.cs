using BISC.Modules.Connection.Infrastructure.DeviceConnection;
using BISC.Modules.Connection.Infrastructure.Factorys;
using BISC.Modules.Connection.Model.Model;
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

namespace BISC.Modules.Connection.Presentation.ViewModels.Ping
{
    public class PingAddingViewModel : DisposableViewModelBase, IPingAddingViewModel
    {

        private IIpAddress _ipAddress;
        private IDeviceConnectionFactory _deviceConnectionFactory;
        private ICommandFactory _commandFactory;
        private IConnections _connections;

        public PingAddingViewModel(IIpAddress ipAddress, IConnections connections, IDeviceConnectionFactory deviceConnactionFactory, ICommandFactory commandFactory )
        {
            _ipAddress = ipAddress;
            _connections = connections;
            _deviceConnectionFactory = deviceConnactionFactory;
            _commandFactory = commandFactory;
            TestCommand = _commandFactory.CreateDelegateCommand(OnTestCommand);
        }

        private void OnTestCommand()
        {
            CurentConnections.Add(_deviceConnectionFactory.GetDeviceConnection());
        }


        #region Implementation of IPingAddingViewModel
        public IIpAddress IpAddress
        {
            get { return _ipAddress; }
        }

        public ICommand TestCommand { get; }

        public ObservableCollection<IDeviceConnection> CurentConnections { get { return _connections.DeviceConnectionsList; } }
        #endregion
    }
}
