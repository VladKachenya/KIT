using BISC.Model.Infrastructure.Elements;
using System.Collections.Generic;

namespace BISC.Model.Infrastructure.Project
{
    public interface IBiscProject:IModelElement
    {
        ISclModel MainSclModel { get; set; }
        IModelElement CustomElements { get; set; }
    }
}