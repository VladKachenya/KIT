using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.InformationModel.Infrastucture.Services;

namespace BISC.Modules.InformationModel.Presentation.Services
{
    public class InfoModelLoadingService : IDeviceElementLoadingService
    {

        private readonly IConnectionPoolService _connectionPoolService;
        private readonly ILogicalDeviceLoadingService _logicalDeviceLoadingService;
        private readonly IInfoModelService _infoModelService;

        public InfoModelLoadingService(IConnectionPoolService connectionPoolService,
            ILogicalDeviceLoadingService logicalDeviceLoadingService, IInfoModelService infoModelService)
        {
            _connectionPoolService = connectionPoolService;
            _logicalDeviceLoadingService = logicalDeviceLoadingService;
            _infoModelService = infoModelService;
        }

        public async Task<int> EstimateProgress(IDevice device)
        {
            await _logicalDeviceLoadingService.PrepareProgressData(device.Ip);
            device.Name = _logicalDeviceLoadingService.GetDeviceName();
            return _logicalDeviceLoadingService.GetLogicalNodeCount();
        }

        public async Task Load(IDevice device, IProgress<DeviceLoadingEvent> deviceLoadingProgress)
        {
            int loadedLns = 0;
            var devices = await _logicalDeviceLoadingService.GetLDeviceFromConnection(
                 new Progress<LogicalNodeLoadingEvent>(loadingEvent =>
                     deviceLoadingProgress.Report(new DeviceLoadingEvent(null, ++loadedLns))));
            foreach (var ldevice in devices)
            {
                _infoModelService.AddOrReplaceLDevice(device, ldevice);
            }
        }

        public int Priority => 10;

        public void Dispose()
        {

        }





    }
}