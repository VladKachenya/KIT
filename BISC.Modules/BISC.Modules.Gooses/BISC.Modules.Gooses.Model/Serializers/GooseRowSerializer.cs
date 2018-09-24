using BISC.Model.Global.Serializators;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Model.Model.Matrix;

namespace BISC.Modules.Gooses.Model.Serializers
{
   public class GooseRowSerializer : DefaultModelElementSerializer<IGooseRow>
    {
        public GooseRowSerializer()
        {
            RegisterProperty(nameof(IGooseRow.GooseRowType), "GooseRowType");
            RegisterProperty(nameof(IGooseRow.Signature), "Signature");
            RegisterProperty(nameof(IGooseRow.ReferencePath), "ReferencePath");
            RegisterProperty(nameof(IGooseRow.ValuesString), "ValuesString");
            RegisterProperty(nameof(IGooseRow.NumberOfFcdaInDataSetOfGoose), "NumberOfFcdaInDataSetOfGoose");

        }

        public override IModelElement GetConcreteObject()
        {
            return new GooseRow();
        }
    }
}
