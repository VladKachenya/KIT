using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

namespace BISC.Modules.Gooses.Model.Model
{
   public class MacAddressEntity:ModelElement,IMacAddressEntity
    {

        public MacAddressEntity()
        {
            ElementName = GooseKeys.GooseModelKeys.MacAddressEntityKey;
        }
        #region Implementation of IMacAddressEntity

        public string MacAddress { get; set; }

        #endregion
    }
}
