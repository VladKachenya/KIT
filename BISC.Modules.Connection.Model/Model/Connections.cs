using BISC.Modules.Connection.Infrastructure.DeviceConnection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Connection.Model.Model
{
    public class Connections : IConnections
    {
        public Connections()
        {
            DeviceConnectionsList = new ObservableCollection<IDeviceConnection>();
        }
        public ObservableCollection<IDeviceConnection> DeviceConnectionsList { get; }
    }
}
