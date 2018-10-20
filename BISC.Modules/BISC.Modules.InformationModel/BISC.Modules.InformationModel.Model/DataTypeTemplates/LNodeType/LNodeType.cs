using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;

namespace BISC.Modules.InformationModel.Model.DataTypeTemplates.LNodeType
{
   public class LNodeType:ModelElement,ILNodeType
    {
        public LNodeType()
        {
            ElementName = "LNodeType";
        }
        public string Id { get; set; }
        public string LnClass { get; set; }

        public ChildModelsList<IDo> DoList =>new ChildModelsList<IDo>(this, "DO");
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is ILNodeType)) return false;
            var element = obj as ILNodeType;
            if (element.Id != Id) return false;
            if (element.LnClass != LnClass) return false;
            return true;
        }
    }
}
