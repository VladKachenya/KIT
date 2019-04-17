using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.HelperEntities;


namespace BISC.Presentation.Infrastructure.Services
{
    public interface IGlobalSavingService
    {
        Task<SaveResult> SaveAllDevices(bool isReconnectIfNeed = true);
        Task<SaveResult> SaveСhangesToRegion(string regionName, bool isCancelPossible = false);
        Task<SaveResult> SaveСhangesOfDevice(Guid deviceGuid);

    }
}