using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;

namespace BISC.Modules.InformationModel.Model.DataTypeTemplates.DoType
{
   public class Da:ModelElement,IDa
    {
        public Da()
        {
            ElementName = "DA";
        }
        public string Name { get; set; }
        public string BType { get; set; }
        public string Fc { get; set; }
        public string Type { get; set; }
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (base.Equals(obj)) return false;
            if (!(obj is IDa)) return false;
            var element = obj as IDa;
            if (element.Name != Name) return false;
            if (element.BType != BType) return false;
            if (element.Type != Type) return false;
            if (element.Fc != Fc) return false;
            return true;
        }
    }
}
