using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Presentation.Interfaces.Factories
{
    public interface IDeviceViewModelFactory
    {
        IDeviceViewModel CreateDeviceViewModel(IDevice device);
    }
}