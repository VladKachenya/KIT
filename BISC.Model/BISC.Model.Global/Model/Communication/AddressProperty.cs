using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project.Communication;

namespace BISC.Model.Global.Model.Communication
{
  public  class AddressProperty:ModelElement, IAddressProperty
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }


}
