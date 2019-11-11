using System;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Presentation.Infrastructure.HelperEntities;

namespace BISC.Presentation.Infrastructure.Services
{
    public interface IProjectManagementService
    {
        void SaveProject();
        Task SaveProjectAsync(bool isReconnectIfNeed = true, bool isAppClosingProcess = false);
        void DeleteDeviceFromProject(Guid deviceGuid);
        void SaveProjectAsAsync();
        void OpenProjectAsAsync();
        void OpenDefaultProjectAsync();
        void СreateNewProjectAsync();
        void ClearCurrentProjectAsync();


    }
}