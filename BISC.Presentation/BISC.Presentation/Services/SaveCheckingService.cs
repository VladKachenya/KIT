using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.ChangeTracker;
using BISC.Presentation.Infrastructure.Keys;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Presentation.Services
{
    public class SaveCheckingService : ISaveCheckingService
    {
        private readonly INavigationService _navigationService;
        private List<SaveCheckingEntity> _saveCheckingEntities = new List<SaveCheckingEntity>();

        public SaveCheckingService(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public void AddSaveCheckingEntity(SaveCheckingEntity saveCheckingEntity)
        {
            if (!_saveCheckingEntities.Any((entity => entity.ObjectToTrack == saveCheckingEntity.ObjectToTrack)))
            {
                _saveCheckingEntities.Add(saveCheckingEntity);
            }
        }

        public void RemoveSaveCheckingEntityByOwner(IObjectWithChangeTracker objectWithChangeTracker)
        {
            var entityFinded =
                _saveCheckingEntities.FirstOrDefault((entity => entity.ObjectToTrack == objectWithChangeTracker));

            if (entityFinded != null)
            {
                _saveCheckingEntities.Remove(entityFinded);
            }
        }

        public async Task<SaveResult> SaveAllUnsavedEntities()
        {
            SaveResult saveResultEnum = new SaveResult();
            var modifiedEntities = _saveCheckingEntities.Where((entity =>
                entity.ObjectToTrack.ChangeTracker.GetIsModifiedRecursive())).ToList();
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
    }
}
