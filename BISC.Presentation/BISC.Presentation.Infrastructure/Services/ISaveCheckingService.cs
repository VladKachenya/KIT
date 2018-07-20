using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Presentation.Infrastructure.ChangeTracker;

namespace BISC.Presentation.Infrastructure.Services
{
    public class SaveCheckingEntity
    {
        public SaveCheckingEntity(IObjectWithChangeTracker objectToTrack, string entityFriendlyName, ICommand saveCommand)
        {

            ObjectToTrack = objectToTrack;
            EntityFriendlyName = entityFriendlyName;
            SaveCommand = saveCommand;
        }

        public static string NavigationKey = "SaveCheckingEntity";
        public IObjectWithChangeTracker ObjectToTrack { get; }
        public string EntityFriendlyName { get; }
        public ICommand SaveCommand { get; }
    }

    public class SaveResult
    {
        public bool IsSaved { get; set; }
        public bool IsDeclined { get; set; }
        public bool IsCancelled { get; set; }
    }
    public interface ISaveCheckingService
    {

        void AddSaveCheckingEntity(SaveCheckingEntity saveCheckingEntity);
        void RemoveSaveCheckingEntityByOwner(IObjectWithChangeTracker objectWithChangeTracker);
        Task<SaveResult> SaveAllUnsavedEntities();
    }
}