using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Model.Elements;

namespace BISC.Modules.InformationModel.Model.Serializers.Model
{
   public class LDeviceSerializer:DefaultModelElementSerializer<ILDevice>
    {
        public LDeviceSerializer()
        {
            RegisterProperty(nameof(ILDevice.Inst),"inst");
            //RegisterModelElementCollection(typeof(LogicalNode));
            RegisterProperty(nameof(ILDevice.LogicalNodeZero),"LN0");

        }
        public override IModelElement GetConcreteObject()
        {
            return new LDevice();
        }
    }
}
