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
            ElementName = InfrastructureKeys.ModelKeys.SubNetworkKey;
        }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Type { get; set; }
        public ChildModelsList<IConnectedAccessPoint> ConnectedAccessPoints=>new ChildModelsList<IConnectedAccessPoint>(this, InfrastructureKeys.ModelKeys.ConnectedAccessPointKey);
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is ISubNetwork)) return false;
            var element = obj as ISubNetwork;
            if (element.Name != Name) return false;
            if (element.Desc != Desc) return false;
            if (element.Type != Type) return false;
            return true;
        }
    }
}
