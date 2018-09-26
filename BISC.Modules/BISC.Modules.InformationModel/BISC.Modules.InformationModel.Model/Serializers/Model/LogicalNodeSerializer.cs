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
   public class LogicalNodeSerializer:DefaultModelElementSerializer<ILogicalNode>
    {
        public LogicalNodeSerializer()
        {
            RegisterProperty(nameof(ILogicalNode.Inst),"inst");
            RegisterProperty(nameof(ILogicalNode.LnClass), "lnClass");
            RegisterProperty(nameof(ILogicalNode.LnType), "lnType");
            RegisterProperty(nameof(ILogicalNode.Prefix), "prefix");
            //RegisterModelElementCollection(typeof(Doi));
        }
        public override IModelElement GetConcreteObject()
        {
            return new LogicalNode();
        }

    }
}
