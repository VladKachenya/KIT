using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Infrastructure.Loading
{
    public interface IDeviceLoadingService:IDisposable
    {   
        Task LoadElements(List<IDevice> devicesToLoad);
    }
}