using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Connection.Infrastructure.DeviceConnection
{
    public interface IConnections
    {
        ObservableCollection<IDeviceConnection> DeviceConnectionsList { get; }
    }
}
