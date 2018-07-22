using System.Collections.Generic;
using BISC.Model.Infrastructure;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType
{
    public interface ILNodeType : IModelElement
    {
        string Id { get; set; }
        List<IDo> DoList { get; }
    }
}