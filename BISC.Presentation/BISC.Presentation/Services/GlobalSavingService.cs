using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Presentation.Infrastructure.Events;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BISC.Presentation.Services
{
    public class GlobalSavingService : IGlobalSavingService
    {
        private struct DevicesForReconnect
        {
            public IDevice Device;
            public bool IsReconnectNeed;
        }

        private readonly ISaveCheckingService _saveCheckingService;
        private readonly Func<IDeviceReconnectionService> _deviceReconnectionService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly IUserInteractionService _userInteractionService;
        private readonly IInjectionContainer _injectionContainer;
        private readonly IGlobalEventsService _globalEventsService;

        public GlobalSavingService(ISaveCheckingService saveCheckingService,
            Func<IDeviceReconnectionService> deviceReconnectionService,
            IDeviceModelService deviceModelService, IUserInteractionService userInteractionService, IInjectionContainer injectionContainer, IGlobalEventsService globalEventsService)
        {
            _saveCheckingService = saveCheckingService;
            _deviceReconnectionService = deviceReconnectionService;
            _deviceModelService = deviceModelService;
            _userInteractionService = userInteractionService;
            _injectionContainer = injectionContainer;
            _globalEventsService = globalEventsService;
        }


        #region Implementation of IGlobalSavingService

        public async Task SaveAllDevices()
        {
            _globalEventsService.SendMessage(new ShellBlockEvent { IsBlocked = true });
            try
            {
                var sclModel = _injectionContainer.ResolveType<IBiscProject>().MainSclModel.Value;
                List<DevicesForReconnect> devices = new List<DevicesForReconnect>();

                var allUnsavedEntities = _saveCheckingService.GetSaveCheckingEntities()
                    .Where((entity => entity.ChangeTracker.GetIsModifiedRecursive())).ToList();
                foreach (var unsavedEntity in allUnsavedEntities)
                {
                    if (!string.IsNullOrEmpty(unsavedEntity.DeviceKey) && !devices.Any(el => el.Device.Name == unsavedEntity.DeviceKey))
                    {
                        DevicesForReconnect el;
                        el.Device = _deviceModelService.GetDeviceByName(sclModel, unsavedEntity.DeviceKey);
                        el.IsReconnectNeed = await unsavedEntity.SavingCommand.IsSavingByFtpNeeded();
                        devices.Add(el);
                    }
                    else if (!string.IsNullOrEmpty(unsavedEntity.DeviceKey))
                    {
                        var dev = devices.FirstOrDefault(el => el.Device.Name == unsavedEntity.DeviceKey);
                        if (!dev.IsReconnectNeed)
                        {
                            dev.IsReconnectNeed =
                            await unsavedEntity.SavingCommand.IsSavingByFtpNeeded();
                        }
                    }
                }

                List<string> warnings = new List<string>();
                var devicesForReconnect = devices.Where(el => el.IsReconnectNeed).ToList();
                warnings.AddRange(devicesForReconnect.Select(device =>
                    "УСТРОЙСТВО " + device.Device.Name + " БУДЕТ ПЕРЕЗАГРУЖЕНО!"));
                warnings.AddRange(
                    allUnsavedEntities.Select(el => el.EntityFriendlyName + " будет сохранено!"));
                var res = await _userInteractionService.ShowOptionToUser("Сохранение изменений",
                    "Желаете произвести сохранение?",
                    new List<string>() { "Да", "Нет" }, warnings);
                if (res == 1)
                {
                    return;
                }

                var savingRes = await _saveCheckingService.SaveAllUnsavedEntities(false);
                _globalEventsService.SendMessage(new ShellBlockEvent { IsBlocked = false });

                if (savingRes.IsValidationFailed)
                {
                    return;
                }

                var restartAndReconnecTasks = new Task[devicesForReconnect.Count];
                for (int i = 0; i < devicesForReconnect.Count; i++)
                {
                    restartAndReconnecTasks[i] =
                        _deviceReconnectionService().RestartAndReconnectDevice(devicesForReconnect[i].Device);

                }

                await Task.WhenAll(restartAndReconnecTasks);
            }
            finally
            {
                _globalEventsService.SendMessage(new ShellBlockEvent { IsBlocked = false });

            }

        }

        #endregion
    }
}