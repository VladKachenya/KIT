using System;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public interface IDeviceReconnectionService
    {
        Task<bool> ReconnectDevice(IDevice existingDevice, UiEntityIdentifier uiEntityIdToRemove);
        Task RestartDevice(IDevice existingDevice,UiEntityIdentifier uiEntityIdToRemove = null);
        Task RebootOnly(IDevice existingDevice);
    }
}