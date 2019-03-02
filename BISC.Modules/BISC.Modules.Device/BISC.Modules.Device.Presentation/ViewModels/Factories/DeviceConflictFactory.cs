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
        private readonly Func<DeviceManualConflictViewModel> _deviceConflictViewModelFunc;
        private readonly ICommandFactory _commandFactory;

        public DeviceConflictFactory(Func<DeviceManualConflictViewModel> deviceConflictViewModelFunc,ICommandFactory commandFactory)
        {
            _deviceConflictViewModelFunc = deviceConflictViewModelFunc;
            _commandFactory = commandFactory;
        }

        public DeviceConflictViewModel CreateDeviceConflictViewModel(DeviceConflictContext deviceConflictContext,IElementConflictResolver elementConflictResolver)
        {
            if (elementConflictResolver.ConflictType == ConflictType.ManualResolveNeeded)
            {
                var haveConflict = elementConflictResolver.GetIfConflictsExists(deviceConflictContext.DeviceGuid,
                    deviceConflictContext.SclModelDevice, deviceConflictContext.SclModelProject);
                DeviceManualConflictViewModel deviceManualConflictViewModel = _deviceConflictViewModelFunc();
                deviceManualConflictViewModel.ConflictTitle = elementConflictResolver.ConflictName;
                deviceManualConflictViewModel.IsConflictOk = !haveConflict;
                deviceManualConflictViewModel.IsConflictResolved = !haveConflict;
                deviceManualConflictViewModel.ShowConflictInTool = _commandFactory.CreatePresentationCommand((() =>
                {
                    elementConflictResolver.ShowConflicts(deviceConflictContext.DeviceGuid,
                        deviceConflictContext.SclModelDevice, deviceConflictContext.SclModelProject);
                }));
                return deviceManualConflictViewModel;
            }
            else
            {
                DeviceAutomaticResolveConflictViewModel deviceAutomaticResolveConflictViewModel=new DeviceAutomaticResolveConflictViewModel();
                   var haveConflict = elementConflictResolver.GetIfConflictsExists(deviceConflictContext.DeviceGuid,
                    deviceConflictContext.SclModelDevice, deviceConflictContext.SclModelProject);

                deviceAutomaticResolveConflictViewModel.ConflictTitle = elementConflictResolver.ConflictName;
                deviceAutomaticResolveConflictViewModel.IsConflictOk = !haveConflict;
                deviceAutomaticResolveConflictViewModel.ShowConflictInTool = _commandFactory.CreatePresentationCommand((() =>
                {
                    elementConflictResolver.ShowConflicts(deviceConflictContext.DeviceGuid,
                        deviceConflictContext.SclModelDevice, deviceConflictContext.SclModelProject);
                }));
                return deviceAutomaticResolveConflictViewModel;
            }
        }
    }
}
