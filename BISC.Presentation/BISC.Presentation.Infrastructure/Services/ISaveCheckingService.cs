using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Presentation.Infrastructure.ChangeTracker;
using BISC.Presentation.Infrastructure.Commands;

namespace BISC.Presentation.Infrastructure.Services
{
    public class SaveCheckingEntity
    {
        public SaveCheckingEntity(IChangeTracker changeTracker, string entityFriendlyName, Func<Task> saveTask,ISavingCommand savingCommand, string deviceKey, string regionName=null)
        {

            ChangeTracker = changeTracker;
            EntityFriendlyName = entityFriendlyName;
            SaveTask = saveTask;
	        SavingCommand = savingCommand;
	        DeviceKey = deviceKey;
            RegionName = regionName;
        }

        public static string NavigationKey = "SaveCheckingEntity";
        public IChangeTracker ChangeTracker { get; }
        public string EntityFriendlyName { get; }
        public Func<Task> SaveTask { get; }
	    public ISavingCommand SavingCommand { get; }
	    public string RegionName { get; }
        public string DeviceKey { get; }
    }

    public class SaveResult
    {
        public bool IsSaved { get; set; }
        public bool IsDeclined { get; set; }
        public bool IsCancelled { get; set; }
		public  bool IsValidationFailed { get; set; }
    }
    public interface ISaveCheckingService
    {
        Task<bool> GetIsRegionCanBeClosed(string regionName);
        void AddSaveCheckingEntity(SaveCheckingEntity saveCheckingEntity);
        void RemoveSaveCheckingEntityByOwner(string regionName);
        Task<SaveResult> SaveAllUnsavedEntities(bool isNeedToAsk);
	    Task<SaveResult> SaveDeviceUnsavedEntities(string deviceName,bool isNeedToAsk);

		Task<bool> GetIsRegionSaved(string regionName);
        Task<UnsavedEntitiesInfo> GetIsDeviceEntitiesSaved(string deviceName);

    }

    public class UnsavedEntitiesInfo
    {
        public UnsavedEntitiesInfo(bool isEntitiesSaved, List<SaveCheckingEntity> unsavedCheckingEntities)
        {
            IsEntitiesSaved = isEntitiesSaved;
            UnsavedCheckingEntities = unsavedCheckingEntities;
        }

        public bool IsEntitiesSaved { get; }
        public List<SaveCheckingEntity> UnsavedCheckingEntities { get; }
    }
}