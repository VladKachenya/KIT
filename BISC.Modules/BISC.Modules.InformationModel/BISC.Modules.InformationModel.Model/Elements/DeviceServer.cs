using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.InformationModel.Model.Elements
{
   public class DeviceServer:ModelElement,IDeviceServer
    {
        public DeviceServer()
        {
            LDevicesCollection=new List<ILDevice>();
        }
        public List<ILDevice> LDevicesCollection { get; }
    }
}
