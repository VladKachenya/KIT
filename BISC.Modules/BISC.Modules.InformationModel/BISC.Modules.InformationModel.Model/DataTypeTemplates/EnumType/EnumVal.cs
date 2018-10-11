using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
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
        public override int CompareTo(object obj)
        {
            if (base.CompareTo(obj) == -1) return -1;
            if (!(obj is IEnumVal)) return -1;
            var element = obj as IEnumVal;
            if (element.Ord != Ord) return -1;
            if (element.Value != Value) return -1;
            return 1;
        }
    }
}
