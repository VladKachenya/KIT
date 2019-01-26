using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Presentation.Infrastructure.HelperEntities;

namespace BISC.Presentation.Infrastructure.Services
{
    public interface ISaveCheckingService
    {
        void AddSaveCheckingEntity(SaveCheckingEntity saveCheckingEntity);
        void RemoveSaveCheckingEntityByOwner(string regionName);
        Task<SaveResult> SaveAllUnsavedEntities(bool isNeedToAsk);
	    Task<SaveResult> SaveDeviceUnsavedEntities(string deviceName,bool isNeedToAsk);
        Task ExecuteSave(List<SaveCheckingEntity> entitiesToSave);
        List<SaveCheckingEntity> GetSaveCheckingEntities();
        Task<bool> GetIsRegionSaved(string regionName);

        Task<UnsavedEntitiesInfo> GetIsDeviceEntitiesSaved(string deviceName);

    }
}