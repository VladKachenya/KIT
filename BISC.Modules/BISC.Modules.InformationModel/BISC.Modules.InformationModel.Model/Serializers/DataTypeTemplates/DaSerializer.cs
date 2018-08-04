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
   public class DaSerializer: DefaultModelElementSerializer<IDa>
    {
        public DaSerializer()
        {
            RegisterProperty(nameof(IDa.Name),"name");
            RegisterProperty(nameof(IDa.Fc), "fc");
            RegisterProperty(nameof(IDa.Type), "type");
            RegisterProperty(nameof(IDa.BType), "bType");

        }
        public override IModelElement GetConcreteObject()
        {
            return new Da();
        }

    }
}
