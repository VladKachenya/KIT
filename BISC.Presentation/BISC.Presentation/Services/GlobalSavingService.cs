using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Presentation.Services
{
   public class GlobalSavingService: IGlobalSavingService
    {
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly IDeviceReconnectionService _deviceReconnectionService;

        public GlobalSavingService(ISaveCheckingService saveCheckingService,IDeviceReconnectionService deviceReconnectionService)
        {
            _saveCheckingService = saveCheckingService;
            _deviceReconnectionService = deviceReconnectionService;
        }


        #region Implementation of IGlobalSavingService

        public async Task SaveAllDevices()
        {
            var allUnsavedEntities = _saveCheckingService.GetSaveCheckingEntities()
                .Where((entity => entity.ChangeTracker.GetIsModifiedRecursive())).ToList();


            



        }

        #endregion
    }
}
