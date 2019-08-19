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
using BISC.Presentation.Infrastructure.Keys;

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
            public List<SaveCheckingEntity> UnsavedEntitiesOfDevise { get; }
            public async Task AddUnsavedEntity(SaveCheckingEntity entity)
            {
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

        public GlobalSavingService(
            ISaveCheckingService saveCheckingService,
            Func<IDeviceReconnectionService> deviceReconnectionService,
            IDeviceModelService deviceModelService,
            IUserInteractionService userInteractionService,
            IInjectionContainer injectionContainer,
            IGlobalEventsService globalEventsService)
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
            }
            finally
            {
                _globalEventsService.SendMessage(new ShellBlockEvent { IsBlocked = false });
            }

            res.IsSaved = true;
            return res;
        }

        public async Task<SaveResult> SaveСhangesToRegion(string regionName, bool isCancelPossible = false)
        {
            var saveResult = new SaveResult();

            if (_saveCheckingService.GetSaveCheckingEntities().Any((entity => entity.RegionName == regionName)))
            {
                var modifiedEntities = _saveCheckingService.GetSaveCheckingEntities().Where((entity =>
                    entity.RegionName == regionName && entity.ChangeTracker.GetIsModifiedRecursive())).ToList();
                return await SaveChengests(modifiedEntities, isCancelPossible);
            }
            return new SaveResult() { IsSaved = true };
        }

        public async Task<SaveResult> SaveСhangesOfDevice(Guid deviceGuid)
        {

            if (_saveCheckingService.GetSaveCheckingEntities().Any((entity => entity.DeviceGuid == deviceGuid)))
            {
                var modifiedEntities = _saveCheckingService.GetSaveCheckingEntities().Where((entity =>
                    entity.DeviceGuid == deviceGuid && entity.ChangeTracker.GetIsModifiedRecursive())).ToList();
                return await SaveChengests(modifiedEntities, false);
            }
            return new SaveResult() { IsSaved = true };
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
                if (unsavedEntity.DeviceGuid == new Guid(KeysForNavigation.AppGuids.NullGuid))
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
                    if (res.Any(el => (el.Device != null) && (el.Device.DeviceGuid == unsavedEntity.DeviceGuid)))
                    {
                        var deviseForSaving = res.First(el => (el.Device != null) && (el.Device.DeviceGuid == unsavedEntity.DeviceGuid));
                        await deviseForSaving.AddUnsavedEntity(unsavedEntity);
                    }
                    else
                    {
                        res.Add(await GetDevicesForSaving(unsavedEntity, _deviceModelService.GetDeviceByGuid(sclModel, unsavedEntity.DeviceGuid)));
                    }
                }
            }
            return res;
        }

        private async Task<bool?> GetUserConfirmation(List<DevicesForSaving> devicesForSaving, bool isCancellationNecessary = false)
        {
            List<string> warnings = new List<string>();
            foreach (var deviceForSaving in devicesForSaving)
            {
                warnings.AddRange(deviceForSaving.UnsavedEntitiesOfDevise.Select(el => el.EntityFriendlyName));
            }

            var confirmationList = isCancellationNecessary
                ? new List<string>() { "Да", "Нет", "Отмена" }
                : new List<string>() { "Да", "Нет" };

            var res = await _userInteractionService.ShowOptionToUser("В проекте имеются следующие несохранённые изменения:",
                "Желаете сохранить текущий проект?", confirmationList, warnings);

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
            DevicesForSaving emptyDeviseForSaving = new DevicesForSaving(device);
            emptyDeviseForSaving.UnsavedEntitiesOfDevise.Add(unsavedEntity);
            return emptyDeviseForSaving;
        }

        private async Task<SaveResult> SaveChengests(List<SaveCheckingEntity> modifiedEntities, bool isCancelPossible)
        {
            var saveResult = new SaveResult();
            // Получение всех связанных изменений
            var devisesForSaving = await GetDevicesForSaving(modifiedEntities);

            var entitiesForSaving = new List<SaveCheckingEntity>();
            foreach (var devise in devisesForSaving)
            {
                entitiesForSaving.AddRange(devise.UnsavedEntitiesOfDevise);
            }

            // Валидация перед сохранением
            var warnings = new List<string>();
            foreach (var entity in entitiesForSaving)
            {
                if (entity.SavingCommand == null) continue;
                var validationRes = await entity.SavingCommand.ValidateBeforeSave();
                if (!(validationRes.IsSucceed))
                {
                    warnings.AddRange(validationRes.ErrorList);
                    saveResult.IsValidationFailed = true;
                }
            }

            if (saveResult.IsValidationFailed)
            {
                var task = _userInteractionService.ShowOptionToUser("Ошибка валидации данных:",
                   "Сохранение не может быть произведено!", new List<string>() { "Ок" }, warnings);
                return saveResult;
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
            }
            else
            {
                saveResult.IsDeclined = true;
                return saveResult;
            }
            return new SaveResult();
        }
        #endregion
    }
}