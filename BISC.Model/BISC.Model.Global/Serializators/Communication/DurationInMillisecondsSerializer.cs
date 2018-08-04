using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model.Communication;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project.Communication;

namespace BISC.Model.Global.Serializators.Communication
{
   public class DurationInMillisecondsSerializer:DefaultModelElementSerializer<IDurationInMilliSec>
    {
        public DurationInMillisecondsSerializer()
        {
            RegisterValueToProperty(nameof(IDurationInMilliSec.Value));
            RegisterProperty(nameof(IDurationInMilliSec.Multiplier),"multiplier");
            RegisterProperty(nameof(IDurationInMilliSec.Unit),"unit");
        }
        public override IModelElement GetConcreteObject()
        {
            return new DurationInMilliSec();
        }

    }
}
