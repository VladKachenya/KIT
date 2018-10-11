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
        private List<SaveCheckingEntity> _saveCheckingEntities = new List<SaveCheckingEntity>();

        public SaveCheckingService(INavigationService navigationService,IGlobalEventsService globalEventsService)
        {
            _navigationService = navigationService;
            _globalEventsService = globalEventsService;
        }

        public async Task<bool> GetIsRegionCanBeClosed(string regionName)
        {
            if (_saveCheckingEntities.Any((entity => entity.RegionName == regionName)))
            {

                SaveResult saveResultEnum = new SaveResult();
                var modifiedEntities = _saveCheckingEntities.Where((entity =>
                    entity.RegionName==regionName)).ToList();
                if (modifiedEntities.Any()&&modifiedEntities.First().ChangeTracker.GetIsModifiedRecursive())
                {
                    BiscNavigationParameters navigationParameters = new BiscNavigationParameters();
                    navigationParameters.AddParameterByName(SaveCheckingEntity.NavigationKey, modifiedEntities).AddParameterByName(nameof(SaveResult), saveResultEnum);
                    await _navigationService.NavigateViewToGlobalRegion(KeysForNavigation.ViewNames.SaveChangesViewName, navigationParameters);
                }

                if (saveResultEnum.IsSaved)
                {
                    modifiedEntities.ForEach((entity =>
                    {
                        entity.SaveCommand?.Execute(null);
                    }));
                }
                return !saveResultEnum.IsCancelled;
            }
            else
            {
                return true;
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

        public async Task<SaveResult> SaveAllUnsavedEntities()
        {
            SaveResult saveResultEnum = new SaveResult();
            var modifiedEntities = _saveCheckingEntities.Where((entity =>
                entity.ChangeTracker.GetIsModifiedRecursive())).ToList();
            if (modifiedEntities.Any())
            {
                BiscNavigationParameters navigationParameters=new BiscNavigationParameters();
                navigationParameters.AddParameterByName(SaveCheckingEntity.NavigationKey,modifiedEntities).AddParameterByName(nameof(SaveResult),saveResultEnum);
             await  _navigationService.NavigateViewToGlobalRegion(KeysForNavigation.ViewNames.SaveChangesViewName,navigationParameters);
            }

            if (saveResultEnum.IsSaved)
            {
                modifiedEntities.ForEach((entity =>
                {
                    entity.SaveCommand?.Execute(null);
                } ));
            }
            return saveResultEnum;
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
    }
}
