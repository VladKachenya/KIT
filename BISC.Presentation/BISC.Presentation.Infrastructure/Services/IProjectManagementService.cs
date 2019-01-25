namespace BISC.Presentation.Infrastructure.Services
{
    public interface IProjectManagementService
    {
        void SaveProjectAsync();

        void SaveProjectAsAsync();
        void OpenProjectAsAsync();
        void OpenDefaultProjectAsync();
        void СreateNewProjectAsync();
        void ClearCurrentProjectAsync();


    }
}