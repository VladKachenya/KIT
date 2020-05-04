using System;
using System.Threading;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;

namespace BISC.Modules.InformationModel.Model.Services.LoadingServices
{
    public class InfoModelLoadingService : IDeviceElementLoadingService
    {

        private readonly IConnectionPoolService _connectionPoolService;
        private readonly ILogicalDevice _logicalDeviceLoadingService;
        private readonly IInfoModelService _infoModelService;

        public InfoModelLoadingService(
            IConnectionPoolService connectionPoolService,
            ILogicalDevice logicalDeviceLoadingService, 
            IInfoModelService infoModelService)
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

        public async Task Load(IDevice device, IProgress<object> deviceLoadingProgress, ISclModel sclModel,
            CancellationToken cancellationToken)
        {
            int loadedLns = 0;
            var devices = await _logicalDeviceLoadingService.GetLDeviceFromConnection(
                 new Progress<LogicalNodeLoadingEvent>(loadingEvent =>
                     deviceLoadingProgress.Report(new object())), sclModel, device.Name, cancellationToken);
            _infoModelService.InitializeInfoModel(device, device.Name, sclModel);
            foreach (var ldevice in devices)
            {
                IDeviceAccessPoint accessPoint;
                device.TryGetFirstChildOfType(out accessPoint);
                _infoModelService.AddOrReplaceLDevice(accessPoint, ldevice);
            }
        }

        public int Priority => 10;

        public void Dispose()
        {

        }





    }
}