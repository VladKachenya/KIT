using System.Collections.Generic;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Presentation.Interfaces.Services
{
    public interface IDeviceAddingService
    {

        void OpenDeviceAddingView();
        void AddDevicesInProject(List<IDevice> devicesToAdd);
    }
}