using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Project.Communication;

namespace BISC.Model.Global.Model.Communication
{
  public  class Gse:ModelElement,IGse
    {
        public Gse()
        {
            ElementName = ModelKeys.GseKey;
        }
        public string VlanId
        {
            get => SclAddress.GetProperty("VLAN-ID");
            set => SclAddress.SetProperty("VLAN-ID", value);
        }

        public string MacAddress
        {
            get => SclAddress.GetProperty("MAC-Address");
            set => SclAddress.SetProperty("MAC-Address", value);
        }
        public int VlanPriority
        {
            get =>int.Parse(SclAddress.GetProperty("VLAN-PRIORITY"));
            set => SclAddress.SetProperty("VLAN-PRIORITY", value.ToString());
        }
        public string AppId
        {
            get => SclAddress.GetProperty("APPID");
            set => SclAddress.SetProperty("APPID", value);
        }

        public string LdInst { get; set; }
        public string CbName { get; set; }
        public IDurationInMilliSec MinTime { get; set; }
        public IDurationInMilliSec MaxTime { get; set; }
        public ISclAddress SclAddress { get; set; }
    }
}
