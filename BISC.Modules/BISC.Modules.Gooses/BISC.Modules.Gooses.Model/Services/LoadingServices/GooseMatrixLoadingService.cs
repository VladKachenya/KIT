using System;
using System.Threading;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
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
	public class GooseMatrixLoadingService: IDeviceElementLoadingService
	{
		private readonly IGoosesModelService _goosesModelService;
		private readonly IFtpGooseModelService _ftpGooseModelService;
		private readonly IDeviceWarningsService _deviceWarningsService;
	    private readonly IGooseMatrixFtpService _gooseMatrixFtpService;
	    private readonly ILoggingService _loggingService;

	    public GooseMatrixLoadingService(IGoosesModelService goosesModelService,
			IFtpGooseModelService ftpGooseModelService,IDeviceWarningsService deviceWarningsService, IGooseMatrixFtpService gooseMatrixFtpService,
	        ILoggingService loggingService)
		{
			_goosesModelService = goosesModelService;
			_ftpGooseModelService = ftpGooseModelService;
			_deviceWarningsService = deviceWarningsService;
		    _gooseMatrixFtpService = gooseMatrixFtpService;
		    _loggingService = loggingService;
		}


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
                var resMatrix = await _ftpGooseModelService.GetGooseMatrixByFtp(device.Ip);
                if (!resMatrix.IsSucceed)
                {
                    _deviceWarningsService.SetWarningOfDevice(device.DeviceGuid,
                        GooseKeys.GooseWarningKeys.ErrorGettingGooseOutOfDeviceKey,
                        "Ошибка вычитывания Goose матрицы из устройства");
                    _loggingService.LogMessage(resMatrix.GetFirstError(), SeverityEnum.Critical);
                }
                else
                {
                    var biscProject = sclModel.GetFirstParentOfType<IBiscProject>()?? new BiscProject();
                    if (biscProject.MainSclModel.Value == null)
                    {
                        biscProject.MainSclModel.Value = sclModel;
                    }

                    if (biscProject.CustomElements.Value == null)
                    {
                        biscProject.CustomElements.Value = new ModelElement() { ElementName = "CustomElements" };
                    }

                    _gooseMatrixFtpService.SetGooseMatrixFtpForDevice(device, resMatrix.Item, biscProject);

                    // Тут следует удалить потом
                    //_gooseMatrixFtpService.SetGooseMatrixFtpForDevice(device, resMatrix.Item);

                }
            }
            deviceLoadingProgress?.Report(new object());
		}

		public int Priority => 20;
	}
}
