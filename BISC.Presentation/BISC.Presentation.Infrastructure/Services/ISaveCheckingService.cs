using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Presentation.Infrastructure.ChangeTracker;

namespace BISC.Presentation.Infrastructure.Services
{
    public class SaveCheckingEntity
    {
        public SaveCheckingEntity(IChangeTracker changeTracker, string entityFriendlyName, ICommand saveCommand, string regionName=null)
        {

            ChangeTracker = changeTracker;
            EntityFriendlyName = entityFriendlyName;
            SaveCommand = saveCommand;
            RegionName = regionName;
        }

        public static string NavigationKey = "SaveCheckingEntity";
        public IChangeTracker ChangeTracker { get; }
        public string EntityFriendlyName { get; }
        public ICommand SaveCommand { get; }
        public string RegionName { get; }
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
        void RemoveSaveCheckingEntityByOwner(IObjectWithChangeTracker objectWithChangeTracker);
        Task<SaveResult> SaveAllUnsavedEntities();
        Task<bool> GetIsRegionSaved(string regionName);

    }
}