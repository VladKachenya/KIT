using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.Elements
{
    public interface IDoi:IModelElement, INameable
    {
        string Description { get; set; }
        ChildModelsList<ISdi> SdiCollection { get; }
        ChildModelsList<IDai> DaiCollection { get; }     
    }
}