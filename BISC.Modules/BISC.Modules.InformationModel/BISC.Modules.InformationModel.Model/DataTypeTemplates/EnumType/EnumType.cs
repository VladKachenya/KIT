using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplate.TemplatesBase;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.EnumType;
using System.Collections.Generic;

namespace BISC.Modules.InformationModel.Model.DataTypeTemplates.EnumType
{
    public class EnumType : ModelElement, IEnumType
    {
        public EnumType()
        {
            ElementName = "EnumType";
        }
        public string Id { get; set; }
        public ChildModelsList<IEnumVal> EnumValList => new ChildModelsList<IEnumVal>(this, "EnumVal");
        public List<IDataEntityWithType> GetAllITypes()
        {
            var res = new List<IDataEntityWithType>();
            return res;
        }
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj))
            {
                return false;
            }

            if (!(obj is IEnumType))
            {
                return false;
            }

            var element = obj as IEnumType;
            if (element.Id != Id)
            {
                return false;
            }

            return true;
        }
    }
}
