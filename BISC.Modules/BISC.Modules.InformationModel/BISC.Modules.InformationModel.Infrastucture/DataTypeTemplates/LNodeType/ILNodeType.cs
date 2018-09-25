using System.Collections.Generic;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType
{
    public interface ILNodeType : IModelElement
    {
        string Id { get; set; }
        string LnClass { get; set; }

        ChildModelsList<IDo> DoList { get; }

    }
}