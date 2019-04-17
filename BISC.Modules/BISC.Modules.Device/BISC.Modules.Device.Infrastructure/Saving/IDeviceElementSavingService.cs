using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Infrastructure.Saving
{
    public interface IDeviceElementSavingService
    {
        int Priority { get; }
        Task<OperationResult> Save(IDevice device);
    }
}