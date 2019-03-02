using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Model.Model;

namespace BISC.Modules.Gooses.Model.Serializers.FtpMatrix
{
   public class MacAddressEntitySerializer : DefaultModelElementSerializer<MacAddressEntity>
    {
        public MacAddressEntitySerializer()
        {
            RegisterProperty(nameof(MacAddressEntity.MacAddress), "macAddress");
        }

        public override IModelElement GetConcreteObject()
        {
            return new MacAddressEntity();
        }
    }
}
