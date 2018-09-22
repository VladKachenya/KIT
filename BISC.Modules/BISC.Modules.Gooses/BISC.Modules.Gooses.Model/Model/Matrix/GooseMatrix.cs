using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

namespace BISC.Modules.Gooses.Model.Model.Matrix
{
   public class GooseMatrix:ModelElement, IGooseMatrix
    {
        public GooseMatrix()
        {
            ElementName = GooseKeys.GooseModelKeys.GooseMatrixKey;
            GooseRows=new List<IGooseRow>();
        }
        #region Implementation of IGooseMatrix

        public string RelatedIedName { get; set; }
        public List<IGooseRow> GooseRows { get; }

        #endregion
    }
}
