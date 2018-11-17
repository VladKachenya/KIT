namespace BISC.Infrastructure.Global.Services
{
    public interface IProjectService
    {
        void OpenDefaultProject();
        void SaveCurrentProject();
        void ClearCurrentProject();
        void OpenProjectAs(string fileName);
        void SaveProjectAs(string fileName);
        string GetCurrentProjectPath(bool isFull);
    }
}