using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.Device.Infrastructure.Keys;

namespace BISC.Modules.Device.Model.Model
{
    public class Device:ModelElement,IDevice
    {
        public Device()
        {
            ElementName = DeviceKeys.DeviceModelKey;
        }

        #region Implementation of IDevice

        public string Name { get; set; }
        public string Ip { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string Type { get; set; }
        public string Revision { get; set; }

        #endregion

        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is IDevice)) return false;
            var element = obj as IDevice;
            if (element.Name != Name) return false;
            if (element.Ip != Ip) return false;
            if (element.Description != Description) return false;
            if (element.Manufacturer != Manufacturer) return false;
            if (element.Type != Type) return false;
            if (element.Revision != Revision) return false;
            return true;
        }
    }
}
