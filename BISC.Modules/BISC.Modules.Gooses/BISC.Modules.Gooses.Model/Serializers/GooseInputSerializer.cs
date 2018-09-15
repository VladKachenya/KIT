using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Model.Model;

namespace BISC.Modules.Gooses.Model.Serializers
{
   public class GooseInputSerializer:DefaultModelElementSerializer<IGooseInput>
    {
        public GooseInputSerializer()
        {
            RegisterModelElementCollection(typeof(IExternalGooseRef));
        }

        #region Overrides of DefaultModelElementSerializer<IGooseInput>

        public override IModelElement GetConcreteObject()
        {
            return new GooseInput();
        }

        #endregion
    }
}
