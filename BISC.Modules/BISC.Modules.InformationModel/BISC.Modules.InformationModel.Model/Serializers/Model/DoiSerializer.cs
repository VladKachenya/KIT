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
    public class DoiSerializer : DefaultModelElementSerializer<IDoi>
    {
        public DoiSerializer()
        {
            RegisterModelElementCollection(typeof(Sdi));
            RegisterModelElementCollection(typeof(Dai));
            RegisterProperty(nameof(IDoi.Name),"name");
            RegisterProperty(nameof(IDoi.Description), "desc");
        }
        public override IModelElement GetConcreteObject()
        {
            return new Doi();
        }
    }
}
