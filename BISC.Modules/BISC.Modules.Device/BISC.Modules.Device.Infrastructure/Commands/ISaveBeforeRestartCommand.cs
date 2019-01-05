using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Device.Infrastructure.Commands
{
    public interface ISaveBeforeRestartCommand
    {
        Task SaveBeforeRestart(IDevice existingDevice,List<SaveCheckingEntity> saveCheckingEntities);
    }
}