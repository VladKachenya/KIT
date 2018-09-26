using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Model.Model.Matrix;

namespace BISC.Modules.Gooses.Model.Serializers
{
   public class GooseMatrixSerializer:DefaultModelElementSerializer<IGooseMatrix>
    {
        public GooseMatrixSerializer()
        {
            //RegisterModelElementCollection(typeof(IGooseRow));
            RegisterProperty(nameof(IGooseMatrix.RelatedIedName),"RelatedIedName");
        }
        #region Overrides of DefaultModelElementSerializer<IGooseMatrix>

        public override IModelElement GetConcreteObject()
        {
            return new GooseMatrix();
        }

        #endregion
    }
}
