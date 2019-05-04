using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Model.Global.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;

namespace BISC.Modules.Device.Model.Services
{
    public class DeviceConnectionService : IDeviceConnectionService
    {
        private readonly IBiscProject _biscProject;
        private readonly IConnectionPoolService _connectionPoolService;

        public DeviceConnectionService(IBiscProject biscProject, IConnectionPoolService connectionPoolService)
        {
            _biscProject = biscProject;
            _connectionPoolService = connectionPoolService;
        }

        public async Task<OperationResult<IDevice>> ConnectDevice(string ip,int tryNumber= 1)
        {
            var connection = _connectionPoolService.GetConnection(ip);

            await connection.OpenConnection(tryNumber);
            if (!connection.IsConnected)
            {
                return new OperationResult<IDevice>("Не удалось подключить устройство");
            }

            var identValues = await connection.MmsConnection.IdentifyAsync();
            if (!identValues.IsSucceed)
            {
                return new OperationResult<IDevice>("Identify запрос прошел с ошибкой");
            }
            
            IDevice device = new Model.Device();
            device.Manufacturer = identValues.Item.Count >= 1 ? identValues.Item[0] : DeviceKeys.DeviceManufacturer.UnknowManufacturer;
            device.Type = identValues.Item.Count >= 2 ? identValues.Item[1] : DeviceKeys.DeviceTypes.Unknow;
            device.Revision = identValues.Item.Count >= 3 ? identValues.Item[2] : DeviceKeys.DeviceRevisions.Unknow;

            device.Name = "New Device";
            device.Ip = ip;
            return new OperationResult<IDevice>(device);
        }

        public Task ConnectExistingDevice(IDevice existingDevice)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult<bool>> DisconnectDevice(string ip)
        {
            try
            {
                _connectionPoolService.GetConnection(ip).StopConnection();
            }
            catch (Exception e)
            {
               return new OperationResult<bool>(e.Message);
            }
            return new OperationResult<bool>(true);
        }
    }
}