using System.Collections.Generic;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Presentation.Infrastructure.UiFromModel;

namespace BISC.Modules.Device.Presentation.Interfaces.Services
{
    public interface IDeviceAddingService:IUiFromModelElementService
    {

        void OpenDeviceAddingView();
        void AddDevicesInProject(List<IDevice> devicesToAdd);
    }
}