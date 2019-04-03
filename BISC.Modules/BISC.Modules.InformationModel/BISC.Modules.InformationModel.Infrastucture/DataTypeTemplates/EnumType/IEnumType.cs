using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplate.TemplatesBase;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.EnumType
{
    public interface IEnumType : IModelElement, ITemplateWithId
    {
        ChildModelsList<IEnumVal> EnumValList { get;  }

    }
}