using System.Collections.Generic;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplate.TemplatesBase;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType
{
    public interface IDoType : IModelElement, ITemplateWithId
    {
        string Cdc { get; set; }
        ChildModelsList<IDa> DaList { get; }
        ChildModelsList<ISdo> SdoList { get;  }
    }
}