using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.InformationModel.Model.Elements
{
    public class LogicalNodeZero:LogicalNode,ILogicalNodeZero
    {
        public LogicalNodeZero()
        {
            ElementName = InfoModelKeys.ModelKeys.LogicalNodeZeroKey;
            LnClass = "LLN0";
        }
        public ChildModelProperty<ISettingControl> SettingControl => new ChildModelProperty<ISettingControl>(this, InfoModelKeys.ModelKeys.SettingControlKey);

    }
}
