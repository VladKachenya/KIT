using System.Threading.Tasks;

namespace BISC.Presentation.Infrastructure.Services
{
    public interface IProjectManagementService
    {
        void SaveProject();
        Task SaveProjectAsync(bool isReconnectIfNeed = true);

        void SaveProjectAsAsync();
        void OpenProjectAsAsync();
        void OpenDefaultProjectAsync();
        void СreateNewProjectAsync();
        void ClearCurrentProjectAsync();


    }
}