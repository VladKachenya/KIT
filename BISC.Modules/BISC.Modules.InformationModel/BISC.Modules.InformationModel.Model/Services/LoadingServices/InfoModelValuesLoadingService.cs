using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Keys;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.InformationModel.Model.Elements;

namespace BISC.Modules.InformationModel.Model.Services.LoadingServices
{
    public class InfoModelValuesLoadingService : IDeviceElementLoadingService
    {
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IInfoModelService _infoModelService;
        private readonly IDoiValuesLoadingService _doiValuesLoadingService;

        public InfoModelValuesLoadingService(
            IConnectionPoolService connectionPoolService,
            IInfoModelService infoModelService,
            IDoiValuesLoadingService doiValuesLoadingService)
        {
            _connectionPoolService = connectionPoolService;
            _infoModelService = infoModelService;
            _doiValuesLoadingService = doiValuesLoadingService;
        }
        public void Dispose()
        {
            
        }

        public async Task<int> EstimateProgress(IDevice device)
        {
            var resTack = new Task<int>(() => 5);
            resTack.Start();
            return await resTack;
        }

        public async Task Load(IDevice device, IProgress<object> deviceLoadingProgress, ISclModel sclModel, CancellationToken cancellationToken)
        {
            var dois = _infoModelService.GetAllDoiWithDbRecursive(device);
            foreach (var doi in dois)
            {
                if(cancellationToken.IsCancellationRequested) return;
                await _doiValuesLoadingService.LoadDoiValues(sclModel, device, doi, InformationModelKeys.DataAttributeHeaderKeys.dbFc);
            }
        }

        public int Priority => 15;

    }
}