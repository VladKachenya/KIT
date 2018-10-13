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
    public class SubscriberDevice:ModelElement, ISubscriberDevice
    {
        public SubscriberDevice()
        {
            ElementName = GooseKeys.GooseModelKeys.SubscriberDeviceKey;
        }
        #region Implementation of ISubscriberDevice

        public string LdInst { get; set; }
        public string ApRef { get; set; }
        public string LnClass { get; set; }
        public string DeviceName { get; set; }

        #endregion
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (base.Equals(obj)) return false;
            if (!(obj is ISubscriberDevice)) return false;
            var element = obj as ISubscriberDevice;
            if (element.LdInst != LdInst) return false;
            if (element.ApRef != ApRef) return false;
            if (element.LnClass != LnClass) return false;
            if (element.DeviceName != DeviceName) return false;
            return true;
        }
    }
}
