using BISC.Modules.Device.Infrastructure.Model;
using BISC.Presentation.Infrastructure.ViewModel;

namespace BISC.Modules.Device.Presentation.Interfaces
{
    public interface IDeviceViewModel:ISelectableViewModel
    {
        string DeviceName { get; set; }
        IDevice Device { get; set; }
    }
}