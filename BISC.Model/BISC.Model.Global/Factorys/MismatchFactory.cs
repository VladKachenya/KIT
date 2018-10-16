using BISC.Model.Global.Services;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Factorys;
using BISC.Model.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Model.Global.Factorys
{
    public class MismatchFactory : IMismatchFactory
    {
        public IMismatch GetInequalityMismatch(IModelElement item1, IModelElement item2, int childPosition)
        {
            return new InequalityMismatch(item1, item2, childPosition);
        }

        public IMismatch GetMissingMismatch(IModelElement missingItem)
        {
            return new MissingMismatch(missingItem);
        }
    }
}
