using System;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Infrastructure.Loading
{
    public interface IDeviceLoadingService:IDisposable
    {   
        Task LoadElements(IDevice device, IProgress<DeviceLoadingEvent> deviceLoadingProgress);
    }
}