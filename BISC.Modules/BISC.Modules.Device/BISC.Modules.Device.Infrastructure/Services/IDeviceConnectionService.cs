using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public interface IDeviceConnectionService
    {
        Task<OperationResult<IDevice>> ConnectDevice(string ip,int tryNumber=1);
        Task ConnectExistingDevice(IDevice existingDevice);
        Task<OperationResult<bool>> DisconnectDevice(string ip);

    }
}