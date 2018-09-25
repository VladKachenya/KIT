using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Model;

namespace BISC.Modules.Gooses.Model.Model
{
   public class GooseControl:ModelElement, IGooseControl
    {
        public GooseControl()
        {
            ElementName = GooseKeys.GooseModelKeys.GooseControlKey;
        }


        #region Implementation of IGooseControl

        public string Name { get; set; }
        public string DataSet { get; set; }
        public int ConfRev { get; set; }
        public string AppId { get; set; }
        public ChildModelsList<ISubscriberDevice> SubscriberDevice=>new ChildModelsList<ISubscriberDevice>(this, GooseKeys.GooseModelKeys.SubscriberDeviceKey);

        #endregion
    }
}
