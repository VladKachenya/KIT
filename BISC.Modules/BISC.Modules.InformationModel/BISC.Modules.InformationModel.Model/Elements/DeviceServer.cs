using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.InformationModel.Model.Elements
{
   public class DeviceServer:ModelElement,IDeviceServer
    {
        public DeviceServer()
        {
            ElementName = InfoModelKeys.ModelKeys.ServerKey;
        }
        public ChildModelsList<ILDevice> LDevicesCollection =>new ChildModelsList<ILDevice>(this, InfoModelKeys.ModelKeys.LDeviceKey);
    }
}
