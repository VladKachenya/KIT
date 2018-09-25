using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.EnumType
{
    public interface IEnumType : IModelElement
    {
        string Id { get; set; }
        ChildModelsList<IEnumVal> EnumValList { get;  }

    }
}