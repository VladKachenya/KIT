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
        private struct DevicesForSaving
        {
            public IDevice Device;
            public bool IsRestartNecessry;
            public List<SaveCheckingEntity> UnsavedEntitiesOfDevise;
        }


        private readonly ISaveCheckingService _saveCheckingService;
        private readonly Func<IDeviceReconnectionService> _deviceReconnectionService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly IUserInteractionService _userInteractionService;
        private readonly IInjectionContainer _injectionContainer;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly INavigationService _navigationService;
        private ISclModel _sclModel;

        public GlobalSavingService(ISaveCheckingService saveCheckingService,
            Func<IDeviceReconnectionService> deviceReconnectionService,
            IDeviceModelService deviceModelService, IUserInteractionService userInteractionService, IInjectionContainer injectionContainer, IGlobalEventsService globalEventsService,
            INavigationService navigationService)
        {
            _saveCheckingService = saveCheckingService;
            _deviceReconnectionService = deviceReconnectionService;
            _deviceModelService = deviceModelService;
            _userInteractionService = userInteractionService;
            _injectionContainer = injectionContainer;
            _globalEventsService = globalEventsService;
            _navigationService = navigationService;
        }


        #region Implementation of IGlobalSavingService

        public async Task SaveAllDevices(bool isReconnectIfNeed = true)
        {
            _globalEventsService.SendMessage(new ShellBlockEvent { IsBlocked = true });
            var allUnsavedEntities = _saveCheckingService.GetSaveCheckingEntities()
                .Where((entity => entity.ChangeTracker.GetIsModifiedRecursive())).ToList();

            try
            {
                var devisesForSaving = await GetDevicesForSaving(allUnsavedEntities);
                bool? confirmResult = await GetUserConfirmation(devisesForSaving);

                if (!(bool)confirmResult)
                {
                    return;
                }

                var savingRes = await _saveCheckingService.SaveAllUnsavedEntities(false);
                _globalEventsService.SendMessage(new ShellBlockEvent { IsBlocked = false });

                if (savingRes.IsValidationFailed)
                {
                    return;
                }

                if (isReconnectIfNeed)
                {
                    await RestartAndReconnestDevices(devisesForSaving.Where(el => el.IsRestartNecessry).ToList());
                }
                else
                {
                    await RestartOnlyDevices(devisesForSaving.Where(el => el.IsRestartNecessry).ToList());
                }
            }
            finally
            {
                _globalEventsService.SendMessage(new ShellBlockEvent { IsBlocked = false });

            }
        }

        public async Task<bool> GetIsRegionCanBeClosed(string regionName)
        {
            if (_saveCheckingService.GetSaveCheckingEntities().Any((entity => entity.RegionName == regionName)))
            {
                var modifiedEntities = _saveCheckingService.GetSaveCheckingEntities().Where((entity =>
                    entity.RegionName == regionName)).ToList();

                var devisesForSaving = await GetDevicesForSaving(modifiedEntities);

                bool? res = true;
                if (modifiedEntities.Any() && modifiedEntities.First().ChangeTracker.GetIsModifiedRecursive())
                {
                    res = await GetUserConfirmation(devisesForSaving, true);
                }
                if (res == null)
                {
                    return false;
                }

                if ((bool)res)
                {
                    var entitiesForSaving = new List<SaveCheckingEntity>();
                    foreach (var devise in devisesForSaving)
                    {
                        entitiesForSaving.AddRange(devise.UnsavedEntitiesOfDevise);
                    }
                    await _saveCheckingService.ExecuteSave(entitiesForSaving);

                    var dev = devisesForSaving.Where(el => el.IsRestartNecessry).ToList();
                    if (dev.Count > 0)
                    {
                        await RestartAndReconnestDevices(dev);
                    }
                }
            }

            return true;
        }

        #endregion

        #region private filds

        private ISclModel GetSclModel()
        {
            return _sclModel ?? (_sclModel = _injectionContainer.ResolveType<IBiscProject>().MainSclModel.Value);
        }

        private async Task<List<DevicesForSaving>> GetDevicesForSaving(List<SaveCheckingEntity> unsavedEntities)
        {
            var res = new List<DevicesForSaving>();
            var sclModel = GetSclModel();
            foreach (var unsavedEntity in unsavedEntities)
            {
                if (string.IsNullOrEmpty(unsavedEntity.DeviceKey))
                {
                    if (res.Any(el => el.Device == null))
                    {
                        var emptyDeviseForSaving = res.First(el => el.Device == null);
                        await ConfigureDeviceForSaving(emptyDeviseForSaving, unsavedEntity);
                    }
                    else
                    {
                        res.Add(await GetDevicesForSaving(unsavedEntity));
                    }
                }
                else
                {
                    if (res.Any(el => (el.Device != null) && (el.Device.Name == unsavedEntity.DeviceKey)))
                    {
                        var deviseForSaving = res.First(el => (el.Device != null) && (el.Device.Name == unsavedEntity.DeviceKey));
                        await ConfigureDeviceForSaving(deviseForSaving, unsavedEntity);
                    }
                    else
                    {
                        res.Add(await GetDevicesForSaving(unsavedEntity, _deviceModelService.GetDeviceByName(sclModel, unsavedEntity.DeviceKey)));
                    }
                }
            }

            foreach (var deviceForSaving in res)
            {
                if (deviceForSaving.IsRestartNecessry)
                {
                    var deviceChengests = await _saveCheckingService.GetIsDeviceEntitiesSaved(deviceForSaving.Device.Name);
                    if (!deviceChengests.IsEntitiesSaved)
                    {
                        deviceForSaving.UnsavedEntitiesOfDevise.Clear();
                        deviceForSaving.UnsavedEntitiesOfDevise.AddRange(deviceChengests.UnsavedCheckingEntities);
                    }
                }
            }
            return res;
        }

        private async Task<bool?> GetUserConfirmation(List<DevicesForSaving> devicesForSaving, bool isCancellationNecessary = false)
        {
            List<string> warnings = new List<string>();
            var devicesForReconnect = devicesForSaving.Where(el => el.IsRestartNecessry).ToList();
            foreach (var deviceForSaving in devicesForSaving)
            {
                if (deviceForSaving.IsRestartNecessry)
                {
                    warnings.Add("УСТРОЙСТВО " + deviceForSaving.Device?.Name + " БУДЕТ ПЕРЕЗАГРУЖЕНО!");
                }

                warnings.AddRange(deviceForSaving.UnsavedEntitiesOfDevise.Select(el => el.EntityFriendlyName + " будет сохранено!"));
            }

            var confirmationList = isCancellationNecessary
                ? new List<string>() { "Да", "Нет", "Отмена" }
                : new List<string>() { "Да", "Нет" };

            var res = await _userInteractionService.ShowOptionToUser("Сохранение изменений",
                "Желаете произвести сохранение?", confirmationList, warnings);

            switch (res)
            {
                case 0: return true;
                case 1: return false;
                default: return null;
            }

        }

        private async Task RestartAndReconnestDevices(List<DevicesForSaving> devicesForRestartAndReconnect)
        {
            var restartAndReconnecTasks = new Task[devicesForRestartAndReconnect.Count];
            for (int i = 0; i < devicesForRestartAndReconnect.Count; i++)
            {
                restartAndReconnecTasks[i] =
                    _deviceReconnectionService().RestartDevice(devicesForRestartAndReconnect[i].Device);

            }
            await Task.WhenAll(restartAndReconnecTasks);
        }

        private async Task RestartOnlyDevices(List<DevicesForSaving> devicesForRestart)
        {
            var restartTasks = new Task[devicesForRestart.Count];
            for (int i = 0; i < devicesForRestart.Count; i++)
            {
                restartTasks[i] = _deviceReconnectionService().RebootOnly(devicesForRestart[i].Device);
            }
            await Task.WhenAll(restartTasks);
        }

        private async Task<DevicesForSaving> GetDevicesForSaving(SaveCheckingEntity unsavedEntity, IDevice device = null)
        {
            DevicesForSaving emptyDeviseForSaving;
            emptyDeviseForSaving.Device = device;
            emptyDeviseForSaving.IsRestartNecessry = unsavedEntity.SavingCommand != null && await unsavedEntity.SavingCommand.IsSavingByFtpNeeded();
            emptyDeviseForSaving.UnsavedEntitiesOfDevise = new List<SaveCheckingEntity> { unsavedEntity };
            return emptyDeviseForSaving;
        }

        private async Task ConfigureDeviceForSaving(DevicesForSaving devicesForSaving, SaveCheckingEntity unsavedEntity)
        {
            if (unsavedEntity.SavingCommand != null &&
                await unsavedEntity.SavingCommand.IsSavingByFtpNeeded())
            {
                devicesForSaving.IsRestartNecessry = true;
            }
            devicesForSaving.UnsavedEntitiesOfDevise.Add(unsavedEntity);
        }
        #endregion
    }
}