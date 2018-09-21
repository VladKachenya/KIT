using BISC.Model.Global.Serializators;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Gooses.Model.Serializers
{
   public class GooseRowSerializer : DefaultModelElementSerializer<IGooseRow>
    {
        public GooseRowSerializer()
        {
            RegisterProperty(nameof(IGooseRow.GooseRowType), "GooseRowType");
            RegisterProperty(nameof(IGooseRow.Signature), "Signature");
            RegisterProperty(nameof(IGooseRow.ReferencePath), "ReferencePath");
            RegisterProperty(nameof(IGooseRow.ValueList), "ValueList");
        }
    }
}
