using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.Gooses.Infrastructure.Model.Matrix
{
    public interface IGooseMatrix:IModelElement
    {
        string RelatedIedName { get; set; }

        List<IGooseRow> GooseRows { get; }

    }
}