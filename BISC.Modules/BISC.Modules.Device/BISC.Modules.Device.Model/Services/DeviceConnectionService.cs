using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;

namespace BISC.Modules.Device.Model.Services
{
    public class DeviceConnectionService : IDeviceConnectionService
    {
        private readonly IBiscProject _biscProject;
        private readonly IConnectionPoolService _connectionPoolService;

        public DeviceConnectionService(IBiscProject biscProject)
        {
            _biscProject = biscProject;
        }
        public async Task<IDevice> 
            ConnectDevice(string ip)
        {
            
            IDevice device = new Model.Device();
            device.Ip = ip;
            return device;
        }

        public Task ConnectExistingDevice()
        {
            throw new NotImplementedException();
        }
    }
}
