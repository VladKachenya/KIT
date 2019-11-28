using System.Collections.Generic;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplate.TemplatesBase;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;

namespace BISC.Modules.InformationModel.Model.DataTypeTemplates.DoType
{
    public class DoType : ModelElement, IDoType
    {
        public DoType()
        {
            ElementName = "DOType";
        }
        public string Id { get; set; }
        public string Cdc { get; set; }
        public ChildModelsList<IDa> DaList => new ChildModelsList<IDa>(this, "DA");
        public ChildModelsList<ISdo> SdoList => new ChildModelsList<ISdo>(this, "SDO");
        public List<IDataEntityWithType> GetAllITypes()
        {
            var res = new List<IDataEntityWithType>(DaList);
            res.AddRange(SdoList);
            return res;
        }
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is IDoType)) return false;
            var element = obj as IDoType;
            if (element.Id != Id) return false;
            if (element.Cdc != Cdc) return false;
            return true;
        }
    }
}
