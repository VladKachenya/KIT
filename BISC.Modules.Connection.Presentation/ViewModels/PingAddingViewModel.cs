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

namespace BISC.Modules.Connection.Presentation.ViewModels
{
    public class PingAddingViewModel : DisposableViewModelBase, IPingAddingViewModel
    {

        private IDeviceConnectionFactory _deviceConnectionFactory;
        private ICommandFactory _commandFactory;
        private IConnectionsModel _connectionsModel;

        public PingAddingViewModel(IConnectionsModel connectionsModel, IDeviceConnectionFactory deviceConnactionFactory, ICommandFactory commandFactory )
        {
            _connectionsModel = connectionsModel;
            _deviceConnectionFactory = deviceConnactionFactory;
            _commandFactory = commandFactory;
            TestCommand = _commandFactory.CreateDelegateCommand(OnTestCommand);
        }

        private void OnTestCommand()
        {
            CurentConnections.Add(_deviceConnectionFactory.GetDeviceConnection());
        }


        #region Implementation of IPingAddingViewModel
        public ICommand TestCommand { get; }

        public ObservableCollection<IDeviceConnection> CurentConnections { get; }
        #endregion
    }
}
