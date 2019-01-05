using System.Collections.Generic;
using System.Threading.Tasks;

namespace BISC.Presentation.Infrastructure.Services
{
    public interface ISaveManagementService
    {
        Task<bool> GetIsRegionCanBeClosed(string regionName);
    }
}