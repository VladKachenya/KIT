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
  public  class SubNetwork:ModelElement,ISubNetwork
    {
        public SubNetwork()
        {
            ElementName = ModelKeys.SubNetworkKey;
        }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Type { get; set; }
        public ChildModelsList<IConnectedAccessPoint> ConnectedAccessPoints=>new ChildModelsList<IConnectedAccessPoint>(this, ModelKeys.ConnectedAccessPointKey);
        public override int CompareTo(object obj)
        {
            if (base.CompareTo(obj) == -1) return -1;
            if (!(obj is ISubNetwork)) return -1;
            var element = obj as ISubNetwork;
            if (element.Name != Name) return -1;
            if (element.Desc != Desc) return -1;
            if (element.Type != Type) return -1;
            return 1;
        }
    }
}
