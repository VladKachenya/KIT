using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model.Communication;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Model.Elements;

namespace BISC.Modules.InformationModel.Model.Serializers.Model
{
   public class DeviceAccessPointSerializer:DefaultModelElementSerializer<IDeviceAccessPoint>
    {
        public DeviceAccessPointSerializer()
        {
            RegisterProperty(nameof(IDeviceAccessPoint.Name),"name");
            RegisterProperty(nameof(IDeviceAccessPoint.Clock), "clock");
            RegisterProperty(nameof(IDeviceAccessPoint.Router), "router");
       //     RegisterProperty(nameof(IDeviceAccessPoint.DeviceServer), "Server");
        }

        #region Overrides of DefaultModelElementSerializer<IDeviceAccessPoint>

        public override IModelElement GetConcreteObject()
        {
            return new DeviceAccessPoint();
        }

        #endregion
    }
}
