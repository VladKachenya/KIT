using BISC.Model.Infrastructure.Elements;
using System.Collections.Generic;

namespace BISC.Model.Infrastructure.Project
{
    public interface IBiscProject : IModelElement
    {
        ChildModelProperty<ISclModel> MainSclModel { get; }
        ChildModelProperty<IModelElement> CustomElements { get; }
    }
}