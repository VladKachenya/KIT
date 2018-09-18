using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Model.Model;

namespace BISC.Modules.Gooses.Model.Serializers
{
   public class SubscriberDeviceSerializer:DefaultModelElementSerializer<ISubscriberDevice>
    {
        public SubscriberDeviceSerializer()
        {
            RegisterValueToProperty(nameof(ISubscriberDevice.DeviceName));
          RegisterProperty(nameof(ISubscriberDevice.ApRef),"apRef");
            RegisterProperty(nameof(ISubscriberDevice.LdInst), "ldInst");
            RegisterProperty(nameof(ISubscriberDevice.LnClass), "lnClass");

        }

        #region Overrides of DefaultModelElementSerializer<ISubscriberDevice>

        public override IModelElement GetConcreteObject()
        {
            return new SubscriberDevice();
        }

        #endregion
    }
}
