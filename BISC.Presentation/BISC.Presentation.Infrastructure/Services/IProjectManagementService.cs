namespace BISC.Presentation.Infrastructure.Services
{
    public interface IProjectManagementService
    {
        void SaveProject();
        void SaveProjectAs();
        void OpenProjectAs();
        void OpenDefaultProject();
        void СreateNewProject();
        void ClearCurrentProject();


    }
}