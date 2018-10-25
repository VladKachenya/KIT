using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Presentation.Infrastructure.ChangeTracker;

namespace BISC.Presentation.Infrastructure.Services
{
    public class SaveCheckingEntity
    {
        public SaveCheckingEntity(IChangeTracker changeTracker, string entityFriendlyName, ICommand saveCommand, string deviceKey, string regionName=null)
        {

            ChangeTracker = changeTracker;
            EntityFriendlyName = entityFriendlyName;
            SaveCommand = saveCommand;
            DeviceKey = deviceKey;
            RegionName = regionName;
        }

        public static string NavigationKey = "SaveCheckingEntity";
        public IChangeTracker ChangeTracker { get; }
        public string EntityFriendlyName { get; }
        public ICommand SaveCommand { get; }
        public string RegionName { get; }
        public string DeviceKey { get; }
    }

    public class SaveResult
    {
        public bool IsSaved { get; set; }
        public bool IsDeclined { get; set; }
        public bool IsCancelled { get; set; }
    }
    public interface ISaveCheckingService
    {
        Task<bool> GetIsRegionCanBeClosed(string regionName);
        void AddSaveCheckingEntity(SaveCheckingEntity saveCheckingEntity);
        void RemoveSaveCheckingEntityByOwner(string regionName);
        Task<SaveResult> SaveAllUnsavedEntities(bool isNeedToAsk);
        Task<bool> GetIsRegionSaved(string regionName);
        Task<bool> GetIsDeviceEntitiesSaved(string deviceName);

    }
}