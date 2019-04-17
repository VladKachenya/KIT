using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.IoC;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Saving;

namespace BISC.Modules.Device.Presentation.Services
{
   public class DeviceSavingService: IDeviceSavingService
    {
        private List<IDeviceElementSavingService> _elementSavingServices;

        public DeviceSavingService(IInjectionContainer injectionContainer)
        {
            _elementSavingServices = injectionContainer.ResolveAll(typeof(IDeviceElementSavingService))
                .Cast<IDeviceElementSavingService>().ToList();
        }


        #region Implementation of IDeviceSavingService

        public async Task<OperationResult> SaveAllDeviceElements(IDevice device)
        {
            var sortedElements = _elementSavingServices.OrderBy((service => service.Priority));
            
            foreach (var sortedElement in sortedElements)
            {
              var savingRes= await sortedElement.Save(device);
                if (!savingRes.IsSucceed)
                {
                    return savingRes;
                }
            }
            return OperationResult.SucceedResult;
        }

        #endregion
    }
}
