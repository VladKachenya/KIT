using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Services.Helpers;
using BISC.Modules.Device.Presentation.ViewModels.Conflicts;
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Modules.Device.Presentation.ViewModels.Factories
{
    public class DeviceConflictFactory
    {
        private readonly Func<DeviceConflictViewModel> _deviceConflictViewModelFunc;
        private readonly ICommandFactory _commandFactory;

        public DeviceConflictFactory(Func<DeviceConflictViewModel> deviceConflictViewModelFunc,ICommandFactory commandFactory)
        {
            _deviceConflictViewModelFunc = deviceConflictViewModelFunc;
            _commandFactory = commandFactory;
        }

        public DeviceConflictViewModel CreateDeviceConflictViewModel(DeviceConflictEntity deviceConflictEntity,IElementConflictResolver elementConflictResolver)
        {
            var haveConflict = elementConflictResolver.GetIfConflictsExists(deviceConflictEntity.DeviceName,
                deviceConflictEntity.SclModelDevice, deviceConflictEntity.SclModelProject);
            DeviceConflictViewModel deviceConflictViewModel = _deviceConflictViewModelFunc();
            deviceConflictViewModel.ConflictTitle = elementConflictResolver.ConflictName;
            deviceConflictViewModel.IsConflictOk = !haveConflict;
            return deviceConflictViewModel;
        }
    }
}
