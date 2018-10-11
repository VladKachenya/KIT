using System;
using System.Collections.Generic;
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
            ElementName = ModelKeys.GseKey;
        }
        public string VlanId
        {
            get => SclAddress.Value.GetProperty("VLAN-ID");
            set => SclAddress.Value.SetProperty("VLAN-ID", value);
        }

        public string MacAddress
        {
            get => SclAddress.Value.GetProperty("MAC-Address");
            set => SclAddress.Value.SetProperty("MAC-Address", value);
        }
        public int VlanPriority
        {
            get => int.Parse(SclAddress.Value.GetProperty("VLAN-PRIORITY"));
            set => SclAddress.Value.SetProperty("VLAN-PRIORITY", value.ToString());
        }
        public string AppId
        {
            get => SclAddress.Value.GetProperty("APPID");
            set => SclAddress.Value.SetProperty("APPID", value);
        }

        public string LdInst { get; set; }
        public string CbName { get; set; }
        public ChildModelProperty<IDurationInMilliSec> MinTime =>new ChildModelProperty<IDurationInMilliSec>(this, "MinTime");
        public ChildModelProperty<IDurationInMilliSec> MaxTime => new ChildModelProperty<IDurationInMilliSec>(this, "MaxTime");
        public ChildModelProperty<ISclAddress> SclAddress => new ChildModelProperty<ISclAddress>(this, ModelKeys.SclAddressKey);
        public override int CompareTo(object obj)
        {
            if (base.CompareTo(obj) == -1) return -1;
            if (!(obj is IGse)) return -1;
            var element = obj as IGse;
            if (element.VlanId != VlanId) return -1;
            if (element.MacAddress != MacAddress) return -1;
            if (element.VlanId != VlanId) return -1;
            if (element.VlanPriority != VlanPriority) return -1;
            if (element.AppId != AppId) return -1;
            if (element.LdInst != LdInst) return -1;
            if (element.CbName != CbName) return -1;
            return 1;
        }
    }
}
