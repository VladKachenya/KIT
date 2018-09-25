using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.Elements
{
    public interface ISdi:IModelElement
    {
        string Name { get; set; }
        ChildModelsList<ISdi> SdiCollection { get; }
        ChildModelsList<IDai> DaiCollection { get; }
    }
}