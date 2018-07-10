using System.Collections.Generic;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public interface IDeviceModelService
    {
        List<IDevice> GetDevicesFromModel(ISclModel sclModel);
    }
}