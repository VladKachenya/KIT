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
   public class ConnectedAccessPoint : ModelElement,IConnectedAccessPoint
    {
        
        public ConnectedAccessPoint()
        {
        
            ElementName = InfrastructureKeys.ModelKeys.ConnectedAccessPointKey;
        }
        public string IedName { get; set; }
        public string ApName { get; set; }
        public ChildModelsList<ISclAddress> SclAddresses=>new ChildModelsList<ISclAddress>(this,InfrastructureKeys.ModelKeys.SclAddressKey);
        public ChildModelsList<IGse> GseList=>new ChildModelsList<IGse>(this,InfrastructureKeys.ModelKeys.GseKey);
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is IConnectedAccessPoint)) return false;
            var element = obj as IConnectedAccessPoint;
            if (element.IedName != IedName) return false;
            if (element.ApName != ApName) return false;
            return true;
        }
    }
}
