using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.Elements
{
    public interface ISdi:IModelElement
    {
        string Name { get; set; }
        List<ISdi> SdiCollection { get; }
        List<IDai> DaiCollection { get; }
    }
}