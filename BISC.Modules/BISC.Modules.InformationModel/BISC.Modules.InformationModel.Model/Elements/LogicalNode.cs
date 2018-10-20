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
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is ILogicalNode)) return false;
            var element = obj as ILogicalNode;
            if (element.Name != Name) return false;
            if (element.LnType != LnType) return false;
            return true;
        }
    }
}
