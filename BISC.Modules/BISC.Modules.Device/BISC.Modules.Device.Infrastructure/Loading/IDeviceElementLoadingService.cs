using System;
using System.Threading;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Infrastructure.Loading
{
    public interface IDeviceElementLoadingService:IDisposable
    {
        Task<int> EstimateProgress(IDevice device);
        Task Load(IDevice device, IProgress<object> deviceLoadingProgress, ISclModel sclModel,CancellationToken cancellationToken);
        
        int Priority { get; }
    }
}