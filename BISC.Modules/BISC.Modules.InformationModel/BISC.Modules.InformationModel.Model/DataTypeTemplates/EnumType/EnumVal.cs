using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.EnumType;

namespace BISC.Modules.InformationModel.Model.DataTypeTemplates.EnumType
{
   public class EnumVal:ModelElement, IEnumVal
    {
        public EnumVal()
        {
            ElementName = "EnumVal";
        }
        public int Ord { get; set; }
        public string Value { get; set; }
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (base.Equals(obj)) return false;
            if (!(obj is IEnumVal)) return false;
            var element = obj as IEnumVal;
            if (element.Ord != Ord) return false;
            if (element.Value != Value) return false;
            return true;
        }
    }
}
