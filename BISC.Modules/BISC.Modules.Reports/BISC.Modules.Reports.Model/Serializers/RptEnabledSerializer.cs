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
    public class RptEnabledSerializer : DefaultModelElementSerializer<IRptEnabled>
    {
        public RptEnabledSerializer()
        {
            RegisterProperty(nameof(IRptEnabled.Max), "max");
        }

        public override IModelElement GetConcreteObject()
        {
            return new RptEnabled();
        }
    }
}
