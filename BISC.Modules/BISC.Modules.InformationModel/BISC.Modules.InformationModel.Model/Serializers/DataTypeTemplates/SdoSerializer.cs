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
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.DoType;

namespace BISC.Modules.InformationModel.Model.Serializers.DataTypeTemplates
{
   public class SdoSerializer: DefaultModelElementSerializer<ISdo>
    {
        public SdoSerializer()
        {
            RegisterProperty(nameof(ISdo.Name),"name");
            RegisterProperty(nameof(ISdo.Type), "type");

        }
        public override IModelElement GetConcreteObject()
        {
            return new Sdo();
        }

    }
}
