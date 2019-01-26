using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Commands;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Device.Presentation.Commands
{
   public class SaveBeforeRestartCommand: ISaveBeforeRestartCommand
    {
        private readonly IDeviceReconnectionService _deviceReconnectionService;

        public SaveBeforeRestartCommand(IDeviceReconnectionService deviceReconnectionService)
        {
            _deviceReconnectionService = deviceReconnectionService;
        }

        public Task SaveBeforeRestart(IDevice existingDevice, List<SaveCheckingEntity> saveCheckingEntities)
        {
            throw new NotImplementedException();
        }
    }
}
