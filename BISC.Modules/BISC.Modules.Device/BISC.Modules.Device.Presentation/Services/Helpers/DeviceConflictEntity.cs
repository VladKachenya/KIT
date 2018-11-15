using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;

namespace BISC.Modules.Device.Presentation.Services.Helpers
{
  public  class DeviceConflictEntity
    {
        public DeviceConflictEntity(ISclModel sclModelProject, ISclModel sclModelDevice, string deviceName)
        {
            SclModelProject = sclModelProject;
            SclModelDevice = sclModelDevice;
            DeviceName = deviceName;
        }

        public ISclModel SclModelProject { get; }
        public ISclModel SclModelDevice { get; }
        public string DeviceName { get; }

    }

}
