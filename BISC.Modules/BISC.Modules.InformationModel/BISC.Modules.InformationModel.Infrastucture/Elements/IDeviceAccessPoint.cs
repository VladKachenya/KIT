using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.Elements
{
    public interface IDeviceAccessPoint:IModelElement
    {



        string Name { get; set; }
        bool? Router { get; set; }
        bool? Clock { get; set; }
        IDeviceServer DeviceServer { get; set; }
    }
}