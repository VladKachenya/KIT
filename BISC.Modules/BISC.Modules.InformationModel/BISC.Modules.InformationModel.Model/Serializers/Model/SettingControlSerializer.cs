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
   public  class SettingControlSerializer:DefaultModelElementSerializer<ISettingControl>
    {
        public SettingControlSerializer()
        {
            RegisterProperty(nameof(ISettingControl.NumOfSGs), "numOfSGs");
            RegisterProperty(nameof(ISettingControl.ActSG), "actSG");
        }

        public override IModelElement GetConcreteObject()
        {
            return new SettingControl();
        }
    }
}
