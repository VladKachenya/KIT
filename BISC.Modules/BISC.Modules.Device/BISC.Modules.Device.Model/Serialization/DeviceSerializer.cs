using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Global.Common;
using BISC.Model.Global.Model;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Model.Serialization
{
   public class DeviceSerializer: DefaultModelElementSerializer<IDevice>
    {
        public DeviceSerializer()
        {
            RegisterProperty(nameof(IDevice.Name),"name");
        }
        public override IModelElement GetConcreteObject()
        {
            return new Model.Device();

        }

    }
}
