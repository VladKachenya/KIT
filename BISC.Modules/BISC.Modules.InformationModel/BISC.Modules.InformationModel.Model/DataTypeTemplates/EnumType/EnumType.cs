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
    public class EnumType:ModelElement,IEnumType
    {
        public EnumType()
        {
            ElementName = "EnumType";
        }
        public string Id { get; set; }
        public ChildModelsList<IEnumVal> EnumValList =>new ChildModelsList<IEnumVal>(this, "EnumVal");
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is IEnumType)) return false;
            var element = obj as IEnumType;
            if (element.Id != Id) return false;
            return true;
        }
    }
}
