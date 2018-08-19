using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public interface IDeviceConnectionService
    {
        Task<IDevice> ConnectDevice(string ip);
        Task ConnectExistingDevice();
    }
}