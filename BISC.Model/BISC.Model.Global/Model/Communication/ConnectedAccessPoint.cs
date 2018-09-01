using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Project.Communication;

namespace BISC.Model.Global.Model.Communication
{
   public class ConnectedAccessPoint:ModelElement,IConnectedAccessPoint
    {
        
        public ConnectedAccessPoint()
        {
            SclAddresses=new List<ISclAddress>();
            GseList=new List<IGse>();
            ElementName = ModelKeys.ConnectedAccessPointKey;
        }
        public string IedName { get; set; }
        public string ApName { get; set; }
        public List<ISclAddress> SclAddresses { get; }
        public List<IGse> GseList { get; }
    }
}
