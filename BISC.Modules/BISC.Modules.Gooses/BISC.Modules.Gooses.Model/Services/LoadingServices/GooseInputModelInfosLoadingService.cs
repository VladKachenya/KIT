using System;
using System.Threading;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Global.Project;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Services;

namespace BISC.Modules.Gooses.Model.Services.LoadingServices
{
    public class GooseInputModelInfosLoadingService : IDeviceElementLoadingService
    {
        private readonly IGoosesModelService _goosesModelService;
        private readonly IFtpGooseModelService _ftpGooseModelService;
        private readonly IDeviceWarningsService _deviceWarningsService;
        private readonly IBiscProject _biscProject;

        public GooseInputModelInfosLoadingService(IGoosesModelService goosesModelService,
            IFtpGooseModelService ftpGooseModelService, IDeviceWarningsService deviceWarningsService,
            IBiscProject biscProject)
        {
            _goosesModelService = goosesModelService;
            _ftpGooseModelService = ftpGooseModelService;
            _deviceWarningsService = deviceWarningsService;
            _biscProject = biscProject;
        }

        #region Implementation of IDeviceElementLoadingService



        public void Dispose()
        {
        }

        public Task<int> EstimateProgress(IDevice device)
        {
            return Task.FromResult(2);
        }

        public async Task Load(IDevice device, IProgress<object> deviceLoadingProgress, ISclModel sclModel, CancellationToken cancellationToken)
        {
            deviceLoadingProgress?.Report(new object());
            if (device.Manufacturer == DeviceKeys.DeviceManufacturer.BemnManufacturer)
            {
                var resMatrix = await _ftpGooseModelService.GetGooseDeviceInputFromDevice(device.Ip, device.Name);
                if (!resMatrix.IsSucceed)
                {
                    _deviceWarningsService.SetWarningOfDevice(device.DeviceGuid,
                        GooseKeys.GooseWarningKeys.ErrorGettingGooseOutOfDeviceKey,
                        "Ошибка вычитывания GooseInputIfos из устройства");
                }
                else
                {
                    var biscProject = sclModel.GetFirstParentOfType<IBiscProject>() ?? new BiscProject();
                    if (biscProject.MainSclModel.Value == null)
                    {
                        biscProject.MainSclModel.Value = sclModel;
                    }

                    if (biscProject.CustomElements.Value == null)
                    {
                        biscProject.CustomElements.Value = new ModelElement() { ElementName = "CustomElements" };
                    }

                    _goosesModelService.SetGooseInputModelInfosToProject(biscProject,
                        device, resMatrix.Item);

                    // Тут следует удалить потом
                    //_goosesModelService.SetGooseInputModelInfosToProject(_biscProject,
                    //    device, resMatrix.Item);
                }
            }
            deviceLoadingProgress?.Report(new object());
        }

        public int Priority => 25;
        #endregion

    }
}
