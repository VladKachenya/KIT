using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Infrastructure.Loading
{
    public interface IDeviceLoadingService:IDisposable
    {   
        Task<OperationResult> LoadElements(IDevice deviceToLoad);
    }
}