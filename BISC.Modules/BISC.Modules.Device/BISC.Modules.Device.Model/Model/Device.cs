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

        public override int CompareTo(object obj)
        {
            if (base.CompareTo(obj) == -1) return -1;
            if (!(obj is IDevice)) return -1;
            var element = obj as IDevice;
            if (element.Name != Name) return -1;
            if (element.Ip != Ip) return -1;
            if (element.Description != Description) return -1;
            if (element.Manufacturer != Manufacturer) return -1;
            if (element.Type != Type) return -1;
            if (element.Revision != Revision) return -1;
            return 1;
        }
    }
}
