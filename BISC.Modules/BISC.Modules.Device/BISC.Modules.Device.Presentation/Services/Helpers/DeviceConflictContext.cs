using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;

namespace BISC.Modules.Device.Presentation.Services.Helpers
{
  public  class DeviceConflictContext
    {
        public DeviceConflictContext(ISclModel sclModelProject, ISclModel sclModelDevice, Guid deviceGuid)
        {
            SclModelProject = sclModelProject;
            SclModelDevice = sclModelDevice;
            DeviceGuid = deviceGuid;
        }

        public ISclModel SclModelProject { get; }
        public ISclModel SclModelDevice { get; }
        public Guid DeviceGuid { get; }
        public bool IsRestartNeeded { get; set; }

    }

}
