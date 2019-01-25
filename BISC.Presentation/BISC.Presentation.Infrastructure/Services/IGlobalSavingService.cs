using System.Collections.Generic;
using System.Threading.Tasks;


namespace BISC.Presentation.Infrastructure.Services
{
    public interface IGlobalSavingService
    {
        Task SaveAllDevices(bool isReconnectIfNeed = true);
        Task<bool> GetIsRegionCanBeClosed(string regionName);
    }
}