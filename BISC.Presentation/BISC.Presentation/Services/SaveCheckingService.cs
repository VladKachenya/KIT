using BISC.Infrastructure.Global.Services;
using BISC.Presentation.Infrastructure.Keys;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.HelperEntities;

namespace BISC.Presentation.Services
{
    public class SaveCheckingService : ISaveCheckingService
    {
        private readonly INavigationService _navigationService;
        private readonly IUserInteractionService _userInteractionService;
        private List<SaveCheckingEntity> _saveCheckingEntities = new List<SaveCheckingEntity>();

        public SaveCheckingService(INavigationService navigationService, IUserInteractionService userInteractionService)
        {
            _navigationService = navigationService;
            _userInteractionService = userInteractionService;
        }



        public async Task<bool> ValidateSave(List<SaveCheckingEntity> entitiesToSave)
        {
            foreach (var entityToSave in entitiesToSave)
            {
                if ((entityToSave?.SavingCommand == null))
                {
                    continue;
                }
                var res = await entityToSave.SavingCommand.ValidateBeforeSave();
                if (res.IsSucceed)
                {
                    continue;
                }

                await _userInteractionService.ShowOptionToUser("Ошибка сохранения", res.GetFirstError(), new List<string>() { "Ок" });
                return false;


            }
            return true;
        }

        public async Task ExecuteSave(List<SaveCheckingEntity> entitiesToSave)
        {


            foreach (var modifiedEntity in entitiesToSave)
            {
                if (modifiedEntity.SavingCommand != null)
                {
                    await modifiedEntity.SavingCommand.SaveAsync();

                    if (entitiesToSave.Count > 1)
                    {
                        await Task.Delay(100);
                    }
                }

                if (_saveCheckingEntities.Contains(modifiedEntity))
                {
                    _saveCheckingEntities.Remove(modifiedEntity);
                }
            }
        }



        public void AddSaveCheckingEntity(SaveCheckingEntity saveCheckingEntity)
        {
            if (!_saveCheckingEntities.Any((entity => entity.ChangeTracker == saveCheckingEntity.ChangeTracker)))
            {
                _saveCheckingEntities.Add(saveCheckingEntity);
            }
        }




        public void RemoveSaveCheckingEntityByOwner(string regionName)
        {
            var entityFinded =
                _saveCheckingEntities.FirstOrDefault((entity => entity.RegionName == regionName));

            if (entityFinded != null)
            {
                _saveCheckingEntities.Remove(entityFinded);
            }
        }

        public async Task<SaveResult> SaveAllUnsavedEntities(bool isNeedToAsk)
        {
            return await SaveUnsavedEntities((entity =>
                entity.ChangeTracker.GetIsModifiedRecursive()), isNeedToAsk);
        }

        private async Task<SaveResult> SaveUnsavedEntities(Func<SaveCheckingEntity, bool> predicate, bool isNeedToAsk)
        {
            SaveResult saveResultEnum = new SaveResult();
            var modifiedEntities = _saveCheckingEntities.Where(predicate).ToList();
            if (modifiedEntities.Any() && isNeedToAsk)
            {
                BiscNavigationParameters navigationParameters = new BiscNavigationParameters();
                navigationParameters.AddParameterByName(SaveCheckingEntity.NavigationKey, modifiedEntities)
                    .AddParameterByName(nameof(SaveResult), saveResultEnum);
                await _navigationService.NavigateViewToGlobalRegion(KeysForNavigation.ViewNames.SaveChangesViewName,
                    navigationParameters);
            }


            if (saveResultEnum.IsSaved || !isNeedToAsk)
            {
                if (!await ValidateSave(modifiedEntities))
                {
                    return new SaveResult() { IsValidationFailed = true };
                }
                await ExecuteSave(modifiedEntities);
            }

            return saveResultEnum;
        }


        public async Task<SaveResult> SaveDeviceUnsavedEntities(string deviceName, bool isNeedToAsk)
        {
            return await SaveUnsavedEntities((entity => entity.DeviceKey == deviceName &&
                                                        entity.ChangeTracker.GetIsModifiedRecursive()), isNeedToAsk);
        }

        public List<SaveCheckingEntity> GetSaveCheckingEntities()
        {
            return _saveCheckingEntities;
        }

        public async Task<bool> GetIsRegionSaved(string regionName)
        {
            var modifiedEntities = _saveCheckingEntities.Where((entity =>
                entity.RegionName == regionName)).ToList();
            if (modifiedEntities.Any() && modifiedEntities.First().ChangeTracker.GetIsModifiedRecursive())
            {
                return false;
            }

            return true;
        }

        public async Task<UnsavedEntitiesInfo> GetIsDeviceEntitiesSaved(string deviceName)
        {
            var unsavedCheckingEntities = _saveCheckingEntities.Where((entity =>
                 entity.DeviceKey == deviceName && entity.ChangeTracker.GetIsModifiedRecursive())).ToList();

            return new UnsavedEntitiesInfo(!unsavedCheckingEntities.Any(), unsavedCheckingEntities);
        }
    }
}
