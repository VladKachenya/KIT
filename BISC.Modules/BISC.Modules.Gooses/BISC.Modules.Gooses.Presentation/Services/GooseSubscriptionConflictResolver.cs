using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.HelpClasses;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Factorys;

namespace BISC.Modules.Gooses.Presentation.Services
{
    public class GooseSubscriptionConflictResolver : IElementConflictResolver
    {
        private readonly IGoosesModelService _goosesModelService;
        private readonly IGooseMatrixFtpService _gooseMatrixFtpService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IFtpGooseModelService _ftpGooseModelService;
        private readonly IGoCbFtpEntityFactory _cbFtpEntityFactory;

        public GooseSubscriptionConflictResolver(IGoosesModelService goosesModelService, IGooseMatrixFtpService gooseMatrixFtpService, 
            IDeviceModelService deviceModelService, IConnectionPoolService connectionPoolService, IFtpGooseModelService ftpGooseModelService,
            IGoCbFtpEntityFactory cbFtpEntityFactory)
        {
            _goosesModelService = goosesModelService;
            _gooseMatrixFtpService = gooseMatrixFtpService;
            _deviceModelService = deviceModelService;
            _connectionPoolService = connectionPoolService;
            _ftpGooseModelService = ftpGooseModelService;
            _cbFtpEntityFactory = cbFtpEntityFactory;
        }
        #region implementation of IElementConflictResolver

        public string ConflictName => "GooseSubscription";
        public ConflictType ConflictType => ConflictType.ManualResolveNeeded;
        public bool GetIfConflictsExists(Guid deviceGuid, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            //Получение IGooseInputModelInfo из проекта и устройства
            var gooseInputModelInfosInDevice = _goosesModelService.GetGooseInputModelInfos(
                _deviceModelService.GetDeviceByGuid(sclModelInDevice, deviceGuid),
                sclModelInDevice.GetFirstParentOfType<IBiscProject>());
            var gooseInputModelInfosInProgect = _goosesModelService.GetGooseInputModelInfos(
                _deviceModelService.GetDeviceByGuid(sclModelInProject, deviceGuid),
                sclModelInProject.GetFirstParentOfType<IBiscProject>());

            // Получение Goose матриц из sclModel
            var gooseMatrixInDevice =
                _gooseMatrixFtpService.GetGooseMatrixFtpForDevice(
                    _deviceModelService.GetDeviceByGuid(sclModelInDevice, deviceGuid),
                    sclModelInDevice.GetFirstParentOfType<IBiscProject>());
            var gooseMatrixInProject = 
                _gooseMatrixFtpService.GetGooseMatrixFtpForDevice(
                _deviceModelService.GetDeviceByGuid(sclModelInProject, deviceGuid), 
                sclModelInProject.GetFirstParentOfType<IBiscProject>());

            //Сравнение IGooseInputModelInfo из проекта и из устройства
            //Изначально идёт проверка по количеству подписок
            if (gooseInputModelInfosInDevice.Count != gooseInputModelInfosInProgect.Count)
            {
                return true;
            }

            //Затем сравниваются сами непосредственно подписки(отправляющий goose) 
            foreach (var gooseInputModelInfoInProgect in gooseInputModelInfosInProgect)
            {
                if (!gooseInputModelInfosInDevice.Any(el => el.ModelElementCompareTo(gooseInputModelInfoInProgect)))
                {
                    return true;
                }
            }

            //Проверка строчек подписок
            foreach (var gooseInputModelInfoInProgect in gooseInputModelInfosInProgect)
            {
                // Конвертируем в ссылку на Goose
                var goCdFtpEntity =
                    _cbFtpEntityFactory.GetIGoCbFtpEntityFromGooseInputModelInfo(gooseInputModelInfoInProgect);
                //Получаем строки подписки из устройства
                var subscriptionRowsInDevice =
                    _gooseMatrixFtpService.GetAllRowEntitiesOfGoCd(gooseMatrixInDevice, goCdFtpEntity);
                var subscriptionRowsInProject =
                    _gooseMatrixFtpService.GetAllRowEntitiesOfGoCd(gooseMatrixInProject, goCdFtpEntity);

                if (subscriptionRowsInProject.Count != subscriptionRowsInDevice.Count)
                {
                    return true;
                }

                foreach (var rowFtpInProject in subscriptionRowsInProject)
                {
                    if (!subscriptionRowsInDevice.Any(el => el.ModelElementCompareTo(rowFtpInProject)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public async Task<ResolvingResult> ResolveConflict(bool isFromDevice, Guid deviceGuid, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            //Получение bisc проектов из проекта и девайса
            var biscInDevice = sclModelInDevice.GetFirstParentOfType<IBiscProject>();
            var biscInProject = sclModelInProject.GetFirstParentOfType<IBiscProject>();

            //Получение девайсов из проекта и устройства
            var deviceInProject = _deviceModelService.GetDeviceByGuid(sclModelInProject, deviceGuid);
            var deviceInDevice = _deviceModelService.GetDeviceByGuid(sclModelInDevice, deviceGuid);

            //Получение IGooseInputModelInfo из проекта и устройства
            var gooseInputModelInfosInDevice = _goosesModelService.GetGooseInputModelInfos(deviceInDevice, biscInDevice);
            var gooseInputModelInfosInProgect = _goosesModelService.GetGooseInputModelInfos(deviceInProject, biscInProject);

            //Получение Матрицы из проекта и устройства
            var gooseMatrixInDevice = _gooseMatrixFtpService.GetGooseMatrixFtpForDevice(deviceInDevice, biscInDevice);
            var gooseMatrixInProject = _gooseMatrixFtpService.GetGooseMatrixFtpForDevice(deviceInProject, biscInProject);

            if (isFromDevice)
            {
                _goosesModelService.SetGooseInputModelInfosToProject(biscInProject, deviceInProject, gooseInputModelInfosInDevice);
                _gooseMatrixFtpService.SetGooseMatrixFtpForDevice(deviceInProject, gooseMatrixInDevice);
            }
            else
            {
                if (_connectionPoolService.GetConnection(deviceInDevice.Ip).IsConnected)
                {
                    await _ftpGooseModelService.WriteGooseDeviceInputFromDevice(deviceInDevice.Ip,
                        gooseInputModelInfosInProgect);
                    await _ftpGooseModelService.WriteGooseMatrixFtpToDevice(deviceInDevice, gooseMatrixInProject);
                    return new ResolvingResult(){IsRestartNeeded = true};
                }
                return new ResolvingResult("Устройство не отвечает");
            }
            return ResolvingResult.SucceedResult;
        }

        public void ShowConflicts(Guid deviceGuid, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            //throw new NotImplementedException();
        }
        #endregion

        #region private metnods



        #endregion
    }
}