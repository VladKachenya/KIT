using System.Collections.Generic;
using System.Threading.Tasks;


namespace BISC.Presentation.Infrastructure.Services
{
    public interface IGlobalSavingService
    {
        Task<bool> SaveAllDevices(bool isReconnectIfNeed = true);
        Task<bool> GetIsRegionCanBeClosed(string regionName);
        Task<bool> SaveСhangesToRegion(string regionName);
    }
}