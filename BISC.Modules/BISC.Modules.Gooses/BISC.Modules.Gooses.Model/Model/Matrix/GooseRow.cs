using BISC.Model.Global.Model;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BISC.Modules.Gooses.Infrastructure.Keys.GooseKeys;

namespace BISC.Modules.Gooses.Model.Model.Matrix
{
    public class GooseRow :ModelElement, IGooseRow
    {
        public GooseRow()
        {
            ElementName = GooseModelKeys.GooseRowKey;
        }
        public string Signature { get; set; }
        public string ReferencePath { get; set; }
        public string GooseRowType { get; set; }
        public int NumberOfFcdaInDataSetOfGoose { get; set; }

        public List<bool> ValueList { get; set; }
    }
}
