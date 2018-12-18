using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Services;
using BISC.Presentation.Infrastructure.ChangeTracker;
using BISC.Presentation.Infrastructure.Events;
using BISC.Presentation.Infrastructure.Keys;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Presentation.Services
{
    public class SaveCheckingService : ISaveCheckingService
    {
        private readonly INavigationService _navigationService;
        private readonly IGlobalEventsService _globalEventsService;
	    private readonly IUserInteractionService _userInteractionService;
	    private List<SaveCheckingEntity> _saveCheckingEntities = new List<SaveCheckingEntity>();

        public SaveCheckingService(INavigationService navigationService, IGlobalEventsService globalEventsService,IUserInteractionService userInteractionService)
        {
            _navigationService = navigationService;
            _globalEventsService = globalEventsService;
	        _userInteractionService = userInteractionService;
        }

        public async Task<bool> GetIsRegionCanBeClosed(string regionName)
        {
	        if (_saveCheckingEntities.Any((entity => entity.RegionName == regionName)))
	        {

		        SaveResult saveResultEnum = new SaveResult();
		        var modifiedEntities = _saveCheckingEntities.Where((entity =>
			        entity.RegionName == regionName)).ToList();
		        if (modifiedEntities.Any() && modifiedEntities.First().ChangeTracker.GetIsModifiedRecursive())
		        {
			        BiscNavigationParameters navigationParameters = new BiscNavigationParameters();
			        navigationParameters.AddParameterByName(SaveCheckingEntity.NavigationKey, modifiedEntities)
				        .AddParameterByName(nameof(SaveResult), saveResultEnum);
			        await _navigationService.NavigateViewToGlobalRegion(KeysForNavigation.ViewNames.SaveChangesViewName,
				        navigationParameters);
		        }

		        if (saveResultEnum.IsSaved)
			        await ExecuteSave(modifiedEntities);

		        return !saveResultEnum.IsCancelled;
	        }
	        else
            {
                return true;
            }
        }


	    public async Task<bool> ValidateSave(List<SaveCheckingEntity> entitiesToSave)
	    {
		    foreach (var entityToSave in entitiesToSave)
		    {
			    if ((entityToSave?.SavingCommand==null))
			    {
				    continue;
			    }
			    var res =await entityToSave.SavingCommand.ValidateBeforeSave();
			    if (res.IsSucceed) continue;
			    await _userInteractionService.ShowOptionToUser("Ошибка сохранения", res.GetFirstError(), new List<string>() {"Ок"});
			    return false;


		    }
			return true;
	    }

	    public async Task ExecuteSave(List<SaveCheckingEntity> entitiesToSave)
	    {
		    foreach (var modifiedEntity in entitiesToSave)
		    {
			    await modifiedEntity.SaveTask();
			    if (entitiesToSave.Count > 1)
				    await Task.Delay(100);
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
					return new SaveResult(){IsValidationFailed = true};
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
            var unsavedCheckingEntities= _saveCheckingEntities.Where((entity =>
                entity.DeviceKey == deviceName && entity.ChangeTracker.GetIsModifiedRecursive())).ToList();

            return new UnsavedEntitiesInfo(!unsavedCheckingEntities.Any(),unsavedCheckingEntities);
        }
    }
}
