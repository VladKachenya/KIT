using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.InformationModel.Model.Elements
{
   public class LogicalNode:ModelElement,ILogicalNode
    {
        public LogicalNode()
        {
            DoiCollection=new List<IDoi>();
            ElementName = InfoModelKeys.ModelKeys.LogicalNodeKey;
        }
        public string LnClass { get; set; }
        public string Inst { get; set; }
        public string LnType { get; set; }
        public List<IDoi> DoiCollection { get; }
        public string Prefix { get; set; }
    }
}
