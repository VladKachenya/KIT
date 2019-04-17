using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Saving;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Presentation.Factories;
using BISC.Modules.Gooses.Presentation.ViewModels.GooseControls;
using BISC.Presentation.Infrastructure.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BISC.Modules.Gooses.Presentation.Services.SavingServices
{
    public class GooseControlSavingService : IDeviceElementSavingService
    {
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IFtpGooseModelService _ftpGooseModelService;
        private readonly GooseControlViewModelFactory _gooseControlViewModelFactory;
        private readonly IGoosesModelService _goosesModelService;
        private readonly ILoggingService _loggingService;

        public GooseControlSavingService(IConnectionPoolService connectionPoolService, IFtpGooseModelService ftpGooseModelService,
            GooseControlViewModelFactory gooseControlViewModelFactory, IGoosesModelService goosesModelService, ILoggingService loggingService)
        {
            _connectionPoolService = connectionPoolService;
            _ftpGooseModelService = ftpGooseModelService;
            _gooseControlViewModelFactory = gooseControlViewModelFactory;
            _goosesModelService = goosesModelService;
            _loggingService = loggingService;
        }
        public int Priority => 10;
        public async Task<OperationResult> Save(IDevice device)
        {
            // Тут мы берём модель конвертируем её в вью модель а затем переводим в GooseFtpDto
            var gooseControls = _goosesModelService.GetGooseControlsOfDevice(device);
            var gooseControlViewModelsToSave =
                _gooseControlViewModelFactory.CreateGooseControlViewModel(device, gooseControls);
            List<GooseFtpDto> gooseFtpDtos = gooseControlViewModelsToSave.Where((model => model.IsDynamic)).Select((GetGooseFtpDtosFromViewModel)).ToList();

            var res = await _ftpGooseModelService.WriteGooseDtosToDevice(device, gooseFtpDtos);
            if (res.IsSucceed)
            {
                _loggingService.LogMessage($"Сохранение блоков управления GOOSE в устройство {device.Name} по FTP {device.Ip} произошло успешно", SeverityEnum.Info);
                return new OperationResult<SavingCommandResultEnum>(SavingCommandResultEnum.SavedOk);
            }
            else
            {
                _loggingService.LogMessage($"Сохранение блоков управления GOOSE в устройство {device.Name} по FTP {device.Ip} произошло с ошибкой", SeverityEnum.Critical);
                return new OperationResult<SavingCommandResultEnum>(SavingCommandResultEnum.SavedWithErrors, false, res.GetFirstError());
            }
        }

        private GooseFtpDto GetGooseFtpDtosFromViewModel(GooseControlViewModel gooseControlViewModel)
        {
            var gooseFtpDto = new GooseFtpDto();
            gooseFtpDto.Name = gooseControlViewModel.Name;
            gooseFtpDto.AppId = gooseControlViewModel.AppId;
            gooseFtpDto.FixedOffs = gooseControlViewModel.FixedOffs;
            gooseFtpDto.GoId = gooseControlViewModel.GoId;
            gooseFtpDto.GseType = gooseControlViewModel.GseType;
            gooseFtpDto.MacAddress = gooseControlViewModel.MacAddress;
            gooseFtpDto.MaxTime = gooseControlViewModel.MaxTime;
            gooseFtpDto.MinTime = gooseControlViewModel.MinTime;
            gooseFtpDto.SelectedDataset = gooseControlViewModel.SelectedDataset;
            gooseFtpDto.VlanId = gooseControlViewModel.VlanId;
            gooseFtpDto.VlanPriority = gooseControlViewModel.VlanPriority;
            gooseFtpDto.ConfRev = gooseControlViewModel.ConfRev;
            gooseFtpDto.LdInst = gooseControlViewModel.LdInst;
            return gooseFtpDto;
        }
    }
}