using BISC.Presentation.Infrastructure.ViewModel;

namespace BISC.Modules.Device.Presentation.Interfaces
{
    public interface IDeviceViewModel:ISelectableViewModel
    {
        string DeviceName { get; set; }
    }
}