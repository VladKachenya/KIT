using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Saving;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Presentation.Infrastructure.Commands;

namespace BISC.Modules.Gooses.Presentation.Services.SavingServices
{
    public class GooseSubscriptionsSavingService : IDeviceElementSavingService
    {
        private readonly IGooseSavingService _gooseSavingService;
        private readonly IFtpGooseModelService _ftpGooseModelService;
        private readonly IGooseMatrixFtpService _gooseMatrixFtpService;
        private readonly IGoosesModelService _goosesModelService;
        private readonly ILoggingService _loggingService;

        public GooseSubscriptionsSavingService(IGooseSavingService gooseSavingService, IFtpGooseModelService ftpGooseModelService, 
            IGooseMatrixFtpService gooseMatrixFtpService, IGoosesModelService goosesModelService, ILoggingService loggingService)
        {
            _gooseSavingService = gooseSavingService;
            _ftpGooseModelService = ftpGooseModelService;
            _gooseMatrixFtpService = gooseMatrixFtpService;
            _goosesModelService = goosesModelService;
            _loggingService = loggingService;
        }
        public int Priority => 20;
        public async Task<OperationResult> Save(IDevice device)
        {
            if (!(device.Type == DeviceKeys.DeviceTypes.MR761Type ||
                  device.Type == DeviceKeys.DeviceTypes.MR762Type ||
                  device.Type == DeviceKeys.DeviceTypes.MR763Type ||
                  device.Type == DeviceKeys.DeviceTypes.MR771Type))
            {
                return OperationResult.SucceedResult;
            }
            var gooseSubscriptions = _goosesModelService.GetGooseInputModelInfos(device);
            var gooseSubscriptionMatrix = _gooseMatrixFtpService.GetGooseMatrixFtpForDevice(device);
            var res = await _ftpGooseModelService.WriteGooseDeviceInputFromDevice(device.Ip, gooseSubscriptions);
            if (res.IsSucceed)
            {
                _loggingService.LogMessage($"Сохранение подписок на Goose-ы устройства {device.Name} с IP: {device.Ip} произошло успешно", SeverityEnum.Info);
            }
            else
            {
                _loggingService.LogMessage($"Сохранение подписок на Goose-ы устройства {device.Name} с IP: {device.Ip} произошло с ошибкой", SeverityEnum.Critical);
                return new OperationResult<SavingCommandResultEnum>(SavingCommandResultEnum.SavedWithErrors, false, res.GetFirstError());
            }

            

            res = await _ftpGooseModelService.WriteGooseMatrixFtpToDevice(device, gooseSubscriptionMatrix);
            if (res.IsSucceed)
            {
                _loggingService.LogMessage($"Сохранение матрицы подписок на Goose-ы устройства {device.Name} с IP: {device.Ip} произошло успешно", SeverityEnum.Info);
                return new OperationResult<SavingCommandResultEnum>(SavingCommandResultEnum.SavedOk);
            }
            else
            {
                _loggingService.LogMessage($"Сохранение матрицы подписок на Goose-ы устройства {device.Name} с IP: {device.Ip} произошло с ошибкой", SeverityEnum.Critical);
                return new OperationResult<SavingCommandResultEnum>(SavingCommandResultEnum.SavedWithErrors, false, res.GetFirstError());
            }
        }
    }
}