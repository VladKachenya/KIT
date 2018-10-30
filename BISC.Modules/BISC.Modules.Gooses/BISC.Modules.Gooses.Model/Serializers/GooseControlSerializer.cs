using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Serializing;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Model.Model;

namespace BISC.Modules.Gooses.Model.Serializers
{
   public class GooseControlSerializer:DefaultModelElementSerializer<IGooseControl>
    {
        public GooseControlSerializer()
        {
            RegisterProperty(nameof(IGooseControl.AppId), "appID");
            RegisterProperty(nameof(IGooseControl.Name), "name");
            RegisterProperty(nameof(IGooseControl.DataSet), "datSet");
            RegisterProperty(nameof(IGooseControl.ConfRev), "confRev");
            RegisterProperty(nameof(IGooseControl.IsDynamic), "isDynamic",SerializingType.Extended);

            //RegisterModelElementCollection(typeof(ISubscriberDevice));
        }

        #region Overrides of DefaultModelElementSerializer<IGooseControl>

        public override IModelElement GetConcreteObject()
        {
            return new GooseControl();
        }

        #endregion
    }
}
