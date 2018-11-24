namespace BISC.Infrastructure.Global.Services
{
    public interface IProjectService
    {
        void OpenDefaultProject();
        void SaveCurrentProject();
        void OpenProjectAs(string fileName);
        void SaveProjectAs(string fileName);
        string GetCurrentProjectPath(bool isFull);
        void SetDefaultProjectPath();
        void СreateNewProject();

    }
}