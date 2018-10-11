using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.InformationModel.Model.Elements
{
    public class DeviceAccessPoint : ModelElement, IDeviceAccessPoint
    {
        public DeviceAccessPoint()
        {
            ElementName = InfoModelKeys.ModelKeys.AccessPointKey;
        }

        #region Implementation of IDeviceAccessPoint

        public string Name { get; set; }
        public bool? Router { get; set; }
        public bool? Clock { get; set; }
        public ChildModelProperty<IDeviceServer> DeviceServer =>new ChildModelProperty<IDeviceServer>(this, InfoModelKeys.ModelKeys.ServerKey);
        #endregion
        public override int CompareTo(object obj)
        {
            if (base.CompareTo(obj) == -1) return -1;
            if (!(obj is IDeviceAccessPoint)) return -1;
            var element = obj as IDeviceAccessPoint;
            if (element.Name != Name) return -1;
            if (element.Router != Router) return -1;
            if (element.Clock != Clock) return -1;

            return 1;
        }
    }
}
