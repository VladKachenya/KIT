using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.Gooses.Infrastructure.Model
{
    public interface IGooseInput:IModelElement
    {
        ChildModelsList<IExternalGooseRef> ExternalGooseReferences { get; }
    }
}