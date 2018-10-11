using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

namespace BISC.Modules.Gooses.Model.Model.Matrix
{
   public class GooseMatrix:ModelElement, IGooseMatrix
    {
        public GooseMatrix()
        {
            ElementName = GooseKeys.GooseModelKeys.GooseMatrixKey;
        }
        #region Implementation of IGooseMatrix

        public string RelatedIedName { get; set; }
        public ChildModelsList<IGooseRow> GooseRows =>new ChildModelsList<IGooseRow>(this, "GooseRow");

        #endregion
        public override int CompareTo(object obj)
        {
            if (base.CompareTo(obj) == -1) return -1;
            if (!(obj is IGooseMatrix)) return -1;
            var element = obj as IGooseMatrix;
            if (element.RelatedIedName != RelatedIedName) return -1;
            return 1;
        }
    }


 
}
