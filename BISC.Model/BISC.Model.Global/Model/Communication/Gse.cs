using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Project.Communication;

namespace BISC.Model.Global.Model.Communication
{
    public class Gse : ModelElement, IGse
    {
        public Gse()
        {
            ElementName = InfrastructureKeys.ModelKeys.GseKey;
        }
        public string VlanId
        {
            get => SclAddress.Value.GetProperty("VLAN-ID");
            set => SclAddress.Value.SetProperty("VLAN-ID", value);
        }

        public string MacAddress
        {
            get
            {
                var val = SclAddress.Value.GetProperty("MAC-Address");
                return val.Replace(':', '-');
            }
            set
            {
                var val = value.Replace(':', '-');
                SclAddress.Value.SetProperty("MAC-Address", val);
            }
        }
        public int VlanPriority
        {
            get
            {
                if ((SclAddress.Value != null) && (SclAddress.Value.GetProperty("VLAN-PRIORITY") != null)) return int.Parse(SclAddress.Value.GetProperty("VLAN-PRIORITY"));
                return 0;
            }
            set => SclAddress.Value.SetProperty("VLAN-PRIORITY", value.ToString());
        }

        public string AppId
        {
            get => SclAddress.Value.GetProperty("APPID");
            set => SclAddress.Value.SetProperty("APPID", value);
        }

        public string AppIdDec
        {
            get =>
                uint.Parse(SclAddress.Value.GetProperty("APPID"), NumberStyles.HexNumber).ToString();
            set =>
                SclAddress.Value.SetProperty("APPID", uint.Parse(value).ToString("X4"));
        }

        public string LdInst { get; set; }
        public string CbName { get; set; }
        public ChildModelProperty<IDurationInMilliSec> MinTime => new ChildModelProperty<IDurationInMilliSec>(this, "MinTime");
        public ChildModelProperty<IDurationInMilliSec> MaxTime => new ChildModelProperty<IDurationInMilliSec>(this, "MaxTime");
        public ChildModelProperty<ISclAddress> SclAddress => new ChildModelProperty<ISclAddress>(this, InfrastructureKeys.ModelKeys.SclAddressKey);
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is IGse)) return false;
            var element = obj as IGse;
            //if (element.VlanId != VlanId) return false;
            //if (element.MacAddress != MacAddress) return false;
            //if (element.VlanPriority != VlanPriority) return false;
            //if (element.AppId != AppId) return false;
            if (element.LdInst != LdInst) return false;
            if (element.CbName != CbName) return false;
            return true;
        }
    }
}
