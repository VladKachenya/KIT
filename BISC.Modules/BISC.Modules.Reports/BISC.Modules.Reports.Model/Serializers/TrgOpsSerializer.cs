using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Model.Serializers
{
    public class TrgOpsSerializer : DefaultModelElementSerializer<ITrgOps>
    {
        public TrgOpsSerializer()
        {
            RegisterProperty(nameof(ITrgOps.Dchg), "dchg");
            RegisterProperty(nameof(ITrgOps.Qchg), "qchg");
            RegisterProperty(nameof(ITrgOps.Dupd), "dupd");
            RegisterProperty(nameof(ITrgOps.Period), "period");
            RegisterProperty(nameof(ITrgOps.Gi), "gi");
        }

        public override IModelElement GetConcreteObject()
        {
            return new TrgOps();
        }
    }
}
