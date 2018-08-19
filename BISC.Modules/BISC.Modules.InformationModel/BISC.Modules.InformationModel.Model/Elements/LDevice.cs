using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.InformationModel.Model.Elements
{
    public class LDevice:ModelElement,ILDevice
    {
        public LDevice()
        {
            LogicalNodes=new List<ILogicalNode>();
        }
        public string Inst { get; set; }
        public ILogicalNodeZero LogicalNodeZero { get; set; }
        public List<ILogicalNode> LogicalNodes { get;  }
    }
}
