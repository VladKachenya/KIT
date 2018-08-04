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
   public class DoTypeSerializer: DefaultModelElementSerializer<IDoType>
    {
        public DoTypeSerializer()
        {
            RegisterProperty(nameof(IDoType.Cdc),"cdc");
            RegisterProperty(nameof(IDoType.Id),"id");
            RegisterModelElementCollection(typeof(Da));
            RegisterModelElementCollection(typeof(Sdo));
        }
        public override IModelElement GetConcreteObject()
        {
            return new DoType();
        }

    }
}
