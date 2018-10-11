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
   public class ConnectedAccessPoint:ModelElement,IConnectedAccessPoint
    {
        
        public ConnectedAccessPoint()
        {
        
            ElementName = ModelKeys.ConnectedAccessPointKey;
        }
        public string IedName { get; set; }
        public string ApName { get; set; }
        public ChildModelsList<ISclAddress> SclAddresses=>new ChildModelsList<ISclAddress>(this,ModelKeys.SclAddressKey);
        public ChildModelsList<IGse> GseList=>new ChildModelsList<IGse>(this,ModelKeys.GseKey);
        public override int CompareTo(object obj)
        {
            if (base.CompareTo(obj) == -1) return -1;
            if (!(obj is IConnectedAccessPoint)) return -1;
            var element = obj as IConnectedAccessPoint;
            if (element.IedName != IedName) return -1;
            if (element.ApName != ApName) return -1;
            return 1;
        }
    }
}
