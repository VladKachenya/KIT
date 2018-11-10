using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public interface IDeviceRestartService
    {
        Task RestartDevice(IDevice existingDevice,TreeItemIdentifier treeItemIdOfExistingDevice);
    }
}