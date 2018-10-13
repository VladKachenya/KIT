using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DaType;

namespace BISC.Modules.InformationModel.Model.DataTypeTemplates.DaType
{
  public  class Bda:ModelElement,IBda
    {
        public Bda()
        {
            ElementName = "BDA";
        }

        public string Name { get; set; }
        public string BType { get; set; }
        public string Type { get; set; }
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (base.Equals(obj)) return false;
            if (!(obj is IBda)) return false;
            var element = obj as IBda;
            if (element.Name != Name) return false;
            if (element.BType != BType) return false;
            if (element.Type != Type) return false;
            return true;
        }
    }
}
