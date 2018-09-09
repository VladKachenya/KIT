using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;
using BISC.Model.Global.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.LNodeType;

namespace BISC.Modules.InformationModel.Model.Serializers.DataTypeTemplates
{
    public class LNodeTypeSerializer : DefaultModelElementSerializer<ILNodeType>
    {
        public LNodeTypeSerializer()
        {
            RegisterModelElementCollection(typeof(Do));
            RegisterProperty(nameof(ILNodeType.Id),"id");
            RegisterProperty(nameof(ILNodeType.LnClass), "lnClass");

        }
        public override IModelElement GetConcreteObject()
        {
            return new LNodeType();
        }
    }
}
