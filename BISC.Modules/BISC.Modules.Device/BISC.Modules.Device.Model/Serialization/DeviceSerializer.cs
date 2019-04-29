using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Model.Serialization
{
    public class DeviceSerializer : DefaultModelElementSerializer<IDevice>
    {
        public DeviceSerializer()
        {
            RegisterProperty(nameof(IDevice.Name), "name");
            RegisterProperty(nameof(IDevice.Type), "type");
            RegisterProperty(nameof(IDevice.Description), "desc");
            RegisterProperty(nameof(IDevice.Manufacturer), "manufacturer");
            RegisterProperty(nameof(IDevice.Revision), "configVersion");

        }
        public override IModelElement GetConcreteObject()
        {
            return new Model.Device();

        }

    }
}
