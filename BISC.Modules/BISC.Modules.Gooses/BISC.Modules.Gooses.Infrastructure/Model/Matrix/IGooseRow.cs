using BISC.Model.Infrastructure.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Gooses.Infrastructure.Model.Matrix
{
    public interface IGooseRow:IModelElement
    {
        string Signature { get; set; }
        string ReferencePath { get; set; }
        string GooseRowType { get; set; }
        List<bool> ValueList { get; set; }
    }
}