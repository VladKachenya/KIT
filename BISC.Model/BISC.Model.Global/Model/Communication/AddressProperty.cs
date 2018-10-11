using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public override int CompareTo(object obj)
        {
            if(base.CompareTo(obj) == -1) return -1;
            if (!(obj is IAddressProperty)) return -1;
            var element = obj as IAddressProperty;
            if (element.Type != Type) return -1;
            if (element.Value != Type) return -1;
            return 1;
        }
    }


}
