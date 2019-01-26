using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Presentation.Infrastructure.Events;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BISC.Presentation.Services
{
    public class GlobalSavingService : IGlobalSavingService
    {
        private class DevicesForSaving
        {
            public DevicesForSaving(IDevice device)
            {
                Device = device;
                UnsavedEntitiesOfDevise = new List<SaveCheckingEntity>();
            }
            public IDevice Device { get; }
            public bool IsRestartNecessry { get; set; }
            public List<SaveCheckingEntity> UnsavedEntitiesOfDevise { get; }
            public async Task AddUnsavedEntity(SaveCheckingEntity entity)
            {
                if (entity.SavingCommand != null &&
                  await entity.SavingCommand.IsSavingByFtpNeeded())
                {
                    IsRestartNecessry = true;
                }
                UnsavedEntitiesOfDevise.Add(entity);
            }
        }

        private readonly ISaveCheckingService _saveCheckingService;
        private readonly Func<IDeviceReconnectionService> _deviceReconnectionService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly IUserInteractionService _userInteractionService;
        private readonly IInjectionContainer _injectionContainer;
        private readonly IGlobalEventsService _globalEventsService;
        private ISclModel _sclModel;

        public GlobalSavingService(ISaveCheckingService saveCheckingService,
            Func<IDeviceReconnectionService> deviceReconnectionService,
            IDeviceModelService deviceModelService, IUserInteractionService userInteractionService, 
            IInjectionContainer injectionContainer, IGlobalEventsService globalEventsService)
        {
            _saveCheckingService = saveCheckingService;
            _deviceReconnectionService = deviceReconnectionService;
            _deviceModelService = deviceModelService;
            _userInteractionService = userInteractionService;
            _injectionContainer = injectionContainer;
            _globalEventsService = globalEventsService;
        }


        #region Implementation of IGlobalSavingService

        public async Task<SaveResult> SaveAllDevices(bool isReconnectIfNeed = true)
        {
            var res = new SaveResult();
            _globalEventsService.SendMessage(new ShellBlockEvent { IsBlocked = true });
            var allUnsavedEntities = _saveCheckingService.GetSaveCheckingEntities()
                .Where((entity => entity.ChangeTracker.GetIsModifiedRecursive())).ToList();
            try
            {
                var devisesForSaving = await GetDevicesForSaving(allUnsavedEntities);
                bool? confirmResult = await GetUserConfirmation(devisesForSaving);

                if (!(bool)confirmResult)
                {
                    res.IsDeclined = false;
                    return res;
                }

                var savingRes = await _saveCheckingService.SaveAllUnsavedEntities(false);
                _globalEventsService.SendMessage(new ShellBlockEvent { IsBlocked = false });

                if (savingRes.IsValidationFailed)
                {
                    return savingRes;
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

            res.IsSaved = true;
            return res;
        }

        public async Task<SaveResult> SaveСhangesToRegion(string regionName, bool isCancelPossible = false )
        {
            var saveResult = new SaveResult();

            if (_saveCheckingService.GetSaveCheckingEntities().Any((entity => entity.RegionName == regionName)))
            {
                var modifiedEntities = _saveCheckingService.GetSaveCheckingEntities().Where((entity =>
                    entity.RegionName == regionName)).ToList();
                // Получение всех связанных изменений
                var devisesForSaving = await GetDevicesForSaving(modifiedEntities);

                var entitiesForSaving = new List<SaveCheckingEntity>();
                foreach (var devise in devisesForSaving)
                {
                    entitiesForSaving.AddRange(devise.UnsavedEntitiesOfDevise);
                }

                // Валидация перед сохранением
                foreach (var entity in entitiesForSaving)
                {
                    if ((entity.SavingCommand != null) && !(await entity.SavingCommand.ValidateBeforeSave()).IsSucceed)
                    {
                        saveResult.IsValidationFailed = true;
                        return saveResult;
                    }
                }

                bool? res = true;
                if (modifiedEntities.Any() && modifiedEntities.First().ChangeTracker.GetIsModifiedRecursive())
                {
                    res = await GetUserConfirmation(devisesForSaving, isCancelPossible);
                }
                if (res == null)
                {
                    saveResult.IsCancelled = true;
                    return saveResult;
                }
                if ((bool)res)
                {
                    await _saveCheckingService.ExecuteSave(entitiesForSaving);

                    var dev = devisesForSaving.Where(el => el.IsRestartNecessry).ToList();
                    if (dev.Count > 0)
                    {
                        await RestartAndReconnestDevices(dev);
                    }
                }
                else
                {
                    saveResult.IsDeclined = true;
                    return saveResult;
                }
            }
            saveResult.IsSaved = true;
            return saveResult;
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
                        await emptyDeviseForSaving.AddUnsavedEntity(unsavedEntity);
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
                        await deviseForSaving.AddUnsavedEntity(unsavedEntity);
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
            DevicesForSaving emptyDeviseForSaving = new DevicesForSaving(device)
            {
                IsRestartNecessry = unsavedEntity.SavingCommand != null && await unsavedEntity.SavingCommand.IsSavingByFtpNeeded()
            };
            emptyDeviseForSaving.UnsavedEntitiesOfDevise.Add(unsavedEntity);
            return emptyDeviseForSaving;
        }
        #endregion
    }
}