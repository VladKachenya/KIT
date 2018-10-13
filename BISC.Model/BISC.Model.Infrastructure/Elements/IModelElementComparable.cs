using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Model.Infrastructure.Elements
{
    public interface IModelElementComparable
    {
        bool ModelElementCompareTo(IModelElement element);
    }
}
