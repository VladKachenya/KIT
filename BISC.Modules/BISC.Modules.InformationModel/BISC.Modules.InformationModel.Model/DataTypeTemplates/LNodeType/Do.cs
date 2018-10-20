using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;

namespace BISC.Modules.InformationModel.Model.DataTypeTemplates.LNodeType
{
    public class Do:ModelElement,IDo
    {
        public Do()
        {
            ElementName = "DO";
        }
        public string Name { get; set; }
        public string Type { get; set; }
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is IDo)) return false;
            var element = obj as IDo;
            if (element.Name != Name) return false;
            if (element.Type != Type) return false;
            return true;
        }
    }
}
