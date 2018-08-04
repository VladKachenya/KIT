using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project.Communication;

namespace BISC.Model.Global.Model.Communication
{
  public  class SubNetwork:ModelElement,ISubNetwork
    {
        public SubNetwork()
        {
            ConnectedAccessPoints=new List<IConnectedAccessPoint>();
        }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Type { get; set; }
        public List<IConnectedAccessPoint> ConnectedAccessPoints { get; }
    }
}
