using System;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public interface IDeviceReconnectionService
    {
        Task<bool> ReconnectDevice(IDevice existingDevice, TreeItemIdentifier treeItemIdToRemove);
        Task RestartDevice(IDevice existingDevice,TreeItemIdentifier treeItemIdToRemove = null);
        Task RebootOnly(IDevice existingDevice);
    }
}