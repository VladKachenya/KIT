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
   public class Sdo:ModelElement,ISdo
    {
        public Sdo()
        {
            ElementName = "SDO";
        }
        public string Name { get; set; }
        public string Type { get; set; }
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (base.Equals(obj)) return false;
            if (!(obj is IDa)) return false;
            var element = obj as IDa;
            if (element.Name != Name) return false;
            if (element.Type != Type) return false;
            return true;
        }
    }
}
