using System.Collections.Generic;
using BISC.Model.Infrastructure;

namespace BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DaType
{
    public interface IDaType : IModelElement
    {
        string Id { get; set; }
        List<IBda> Bdas { get; }
    }
}