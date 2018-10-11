using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.InformationModel.Model.Elements
{
   public class LogicalNode:ModelElement,ILogicalNode
    {
        public LogicalNode()
        {
            ElementName = InfoModelKeys.ModelKeys.LogicalNodeKey;
        }
        public string LnClass { get; set; }
        public string Inst { get; set; }
        public string LnType { get; set; }
        public ChildModelsList<IDoi> DoiCollection=>new ChildModelsList<IDoi>(this, InfoModelKeys.ModelKeys.DoiKey);
        public string Prefix { get; set; }
        public string Name => Prefix + LnClass + Inst;
        public override int CompareTo(object obj)
        {
            if (base.CompareTo(obj) == -1) return -1;
            if (!(obj is ILogicalNode)) return -1;
            var element = obj as ILogicalNode;
            if (element.Name != Name) return -1;
            if (element.LnType != LnType) return -1;
            return 1;
        }
    }
}
