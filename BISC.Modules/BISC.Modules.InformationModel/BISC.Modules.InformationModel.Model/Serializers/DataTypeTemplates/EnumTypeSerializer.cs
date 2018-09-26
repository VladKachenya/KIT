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
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.EnumType;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.EnumType;

namespace BISC.Modules.InformationModel.Model.Serializers.DataTypeTemplates
{
   public class EnumTypeSerializer: DefaultModelElementSerializer<IEnumType>
    {

        public EnumTypeSerializer()
        {
            //RegisterModelElementCollection(typeof(EnumVal));
            RegisterProperty(nameof(IEnumType.Id),"id");
        }

        #region Overrides of DefaultModelElementSerializer<IEnumType>

        public override IModelElement GetConcreteObject()
        {
            return new EnumType();
        }

        #endregion
    }
}
