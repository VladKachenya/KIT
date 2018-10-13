using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Project.Communication;

namespace BISC.Model.Global.Model.Communication
{
  public  class AddressProperty:ModelElement, IAddressProperty
    {
        public AddressProperty()
        {
            ElementName = ModelKeys.AddressPropertyKey;
        }
        public string Type { get; set; }
        public string Value { get; set; }

        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if(base.Equals(obj)) return false;
            if (!(obj is IAddressProperty)) return false;
            var element = obj as IAddressProperty;
            if (element.Type != Type) return false;
            if (element.Value != Type) return false;
            return true;
        }
    }


}
