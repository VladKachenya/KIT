using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.InformationModel.Presentation.Services
{
   public class InfoModelLoadingService:IDeviceElementLoadingService
    {
        public InfoModelLoadingService()
        {
            
        }
        public Task<int> EstimateProgress()
        {
            throw new NotImplementedException();
        }

        public Task Load(IDevice device, IProgress<DeviceLoadingEvent> deviceLoadingProgress)
        {
            throw new NotImplementedException();
        }
        public int Priority => 10;
        public void Dispose()
        {
           
        }
    }
}
