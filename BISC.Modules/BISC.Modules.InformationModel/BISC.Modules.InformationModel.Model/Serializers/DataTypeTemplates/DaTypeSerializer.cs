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
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DaType;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.DaType;

namespace BISC.Modules.InformationModel.Model.Serializers.DataTypeTemplates
{
    public class DaTypeSerializer: DefaultModelElementSerializer<IDaType>
    {
        public DaTypeSerializer()
        {
            //RegisterModelElementCollection(typeof(Bda));
            RegisterProperty(nameof(IDaType.Id),"id");
        }
        public override IModelElement GetConcreteObject()
        {
            return new DaType();
        }

    }
}
