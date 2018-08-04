using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Model.Global.Common;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.LNodeType;

namespace BISC.Modules.InformationModel.Model.Serializers.DataTypeTemplates
{
    public class DoSerializer : DefaultModelElementSerializer<IDo>
    {
        public DoSerializer()
        {
            RegisterProperty(nameof(IDo.Name),"name");
            RegisterProperty(nameof(IDo.Type), "type");

        }
        public override IModelElement GetConcreteObject()
        {
            return new Do();
        }

    }
}
