using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
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
        public override int CompareTo(object obj)
        {
            if (base.CompareTo(obj) == -1) return -1;
            if (!(obj is ISubscriberDevice)) return -1;
            var element = obj as ISubscriberDevice;
            if (element.LdInst != LdInst) return -1;
            if (element.ApRef != ApRef) return -1;
            if (element.LnClass != LnClass) return -1;
            if (element.DeviceName != DeviceName) return -1;
            return 1;
        }
    }
}
