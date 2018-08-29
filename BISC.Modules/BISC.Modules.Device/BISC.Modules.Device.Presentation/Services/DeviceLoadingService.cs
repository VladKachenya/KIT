using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Presentation.Services
{
    public class DeviceLoadingService : IDeviceLoadingService
    {
        private List<IDeviceElementLoadingService> _elementLoadingServices;
        public DeviceLoadingService(IInjectionContainer injectionContainer)
        {
            _elementLoadingServices = injectionContainer.ResolveAll(typeof(IDeviceElementLoadingService))
                .Cast<IDeviceElementLoadingService>().ToList();
        }
        public void Dispose()
        {
            _elementLoadingServices.ForEach((service => service.Dispose()));
        }


        public async Task LoadElements(IDevice device, IProgress<DeviceLoadingEvent> deviceLoadingProgress)
        {
            var sortedElements = _elementLoadingServices.OrderBy((service => service.Priority));
            var itemsCount = 0;
            foreach (var sortedElement in sortedElements)
            {
                itemsCount += await sortedElement.EstimateProgress(device);
            }

            int currentElementsCount = 0;
            deviceLoadingProgress.Report(new DeviceLoadingEvent(itemsCount, 0, device.Name));
            foreach (var sortedElement in sortedElements)
            {
                await sortedElement.Load(device, new Progress<DeviceLoadingEvent>((deviceLoadingEvent =>
                    deviceLoadingProgress.Report(new DeviceLoadingEvent(itemsCount, ++currentElementsCount)))));
            }
        }

    }
}
