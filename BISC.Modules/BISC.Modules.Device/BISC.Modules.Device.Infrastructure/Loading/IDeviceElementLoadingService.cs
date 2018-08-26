using System;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Infrastructure.Loading
{
    public interface IDeviceElementLoadingService:IDisposable
    {
        Task<int> EstimateProgress(IDevice device);
        Task Load(IDevice device, IProgress<DeviceLoadingEvent> deviceLoadingProgress);
        int Priority { get; }
    }
}