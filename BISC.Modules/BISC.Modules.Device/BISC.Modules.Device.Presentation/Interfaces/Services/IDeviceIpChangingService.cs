using BISC.Modules.Device.Infrastructure.Model;
using BISC.Presentation.Infrastructure.HelperEntities;

namespace BISC.Modules.Device.Presentation.Interfaces.Services
{
    public interface IDeviceIpChangingService
    {
        bool ChengeDeviceIp(IDevice device, string newIp, UiEntityIdentifier uiEntityIdToUpdate);
    }
}