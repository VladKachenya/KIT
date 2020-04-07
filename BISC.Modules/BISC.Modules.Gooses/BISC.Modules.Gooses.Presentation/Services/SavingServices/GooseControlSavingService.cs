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

        private readonly IFtpGooseModelService _ftpGooseModelService;

        private readonly IGoosesModelService _goosesModelService;
        private readonly ILoggingService _loggingService;
        private IConnectionPoolService _connectionPoolService;

        public GooseControlSavingService(
            IConnectionPoolService connectionPoolService, 
            IFtpGooseModelService ftpGooseModelService,
            IGoosesModelService goosesModelService, 
            ILoggingService loggingService)
        {
            _connectionPoolService = connectionPoolService;
            _ftpGooseModelService = ftpGooseModelService;
            _goosesModelService = goosesModelService;
            _loggingService = loggingService;
        }
        public int Priority => 10;
        public async Task<OperationResult> Save(IDevice device)
        {
            if (!_connectionPoolService.GetIsDeviceConnect(device.Ip))
            {
                _loggingService.LogMessage($"Устройство {device.Name}  {device.Ip} не отвечает", SeverityEnum.Critical);
                return new OperationResult<SavingCommandResultEnum>(SavingCommandResultEnum.SavedWithErrors, false, "Устройство не подключено");
            }
            // Тут мы берём модель конвертируем её в вью модель а затем переводим в GooseFtpDto
            var gooseControls = _goosesModelService.GetGooseControlsOfDevice(device);
            var res = await _ftpGooseModelService.WriteGooseToDevice(device, gooseControls);
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
    }
}