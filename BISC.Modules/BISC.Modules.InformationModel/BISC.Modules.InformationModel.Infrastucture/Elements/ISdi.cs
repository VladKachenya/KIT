using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.Elements
{
    public interface ISdi:IModelElement, INameable
    {
        ChildModelsList<ISdi> SdiCollection { get; }
        ChildModelsList<IDai> DaiCollection { get; }
    }
}