using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Model.Infrastructure.Factorys
{
    public interface IMismatchFactory
    {
        IMismatch GetInequalityMismatch(IModelElement item1, IModelElement item2, int childPosition);
        IMismatch GetMissingMismatch(IModelElement missingItem);
    }
}
