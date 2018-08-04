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
    public class AddressPropertySerializer : DefaultModelElementSerializer<IAddressProperty>
    {
        public AddressPropertySerializer()
        {
            RegisterProperty(nameof(IAddressProperty.Type), "type");
            RegisterValueToProperty(nameof(IAddressProperty.Value));

        }

        #region Overrides of DefaultModelElementSerializer<IAddressProperty>

        public override IModelElement GetConcreteObject()
        {
            return new AddressProperty();
        }

        #endregion
    }
}
