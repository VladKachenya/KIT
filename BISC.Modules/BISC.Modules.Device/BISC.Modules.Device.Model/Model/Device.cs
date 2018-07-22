using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Project;

namespace BISC.Modules.Device.Model.Model
{
    public class Device:ModelElement,IDevice
    {
        #region Implementation of IDevice

        public string Name { get; set; }
        public string Ip { get; set; }
        public ISclModel ParentSclModel { get; set; }

        #endregion
    }
}
