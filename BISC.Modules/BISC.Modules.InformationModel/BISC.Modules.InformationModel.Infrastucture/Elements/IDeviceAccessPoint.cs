using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.Elements
{
    public interface IDeviceAccessPoint:IModelElement
    {



        string Name { get; set; }
        bool? Router { get; set; }
        bool? Clock { get; set; }
        ChildModelProperty<IDeviceServer> DeviceServer { get;  }
    }
}