using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model.Communication;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project.Communication;

namespace BISC.Model.Global.Serializators.Communication
{
   public class SclAddressSerializer:DefaultModelElementSerializer<ISclAddress>
    {
        public SclAddressSerializer()
        {
            //RegisterModelElementCollection(typeof(AddressProperty));
        }
        public override IModelElement GetConcreteObject()
        {
            return new SclAddress();
        }
    }
}
