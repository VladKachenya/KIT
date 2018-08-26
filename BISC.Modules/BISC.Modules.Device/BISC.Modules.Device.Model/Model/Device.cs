using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Connection;

namespace BISC.Modules.Device.Model.Model
{
    public class Device:ModelElement,IDevice
    {
        #region Implementation of IDevice

        public string Name { get; set; }
        public string Ip { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string Type { get; set; }
        public string Revision { get; set; }
        public ISclModel ParentSclModel { get; set; }

        #endregion
    }
}
