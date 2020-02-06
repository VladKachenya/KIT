using System.Collections.Generic;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Infrastructure.Services;

namespace BISC.Modules.Gooses.Model.Services
{
    public class GooseModelServicesFacade : IGooseModelServicesFacade
    {
        private readonly IGooseMatrixFtpService _gooseMatrixFtpService;
        private readonly IGoosesModelService _goosesModelService;
        
        // Only goose model services
        public GooseModelServicesFacade(
            IGooseMatrixFtpService gooseMatrixFtpService,
            IGoosesModelService goosesModelService)
        {
            _gooseMatrixFtpService = gooseMatrixFtpService;
            _goosesModelService = goosesModelService;
        }

        public void RemoveGooseInputsByDeviceName(IBiscProject biscProject, string deviceName)
        {
            _goosesModelService.DeleteAllDeviceReferencesInGooseControlsInModel(biscProject, deviceName);
        }

        public void SetGooseReceiving(IDevice device, IBiscProject projectTo, IBiscProject projectFrom)
        {
            // Устанавливаем полученные Goose подписки из sclMode в наш текущий проект
            _goosesModelService.SetGooseInputModelInfosToProject(projectTo, device,
                _goosesModelService.GetGooseInputModelInfos(device, projectFrom));
            // Устанавливаем полученную Goose матрицн из sclMode в наш текущий проект
            _gooseMatrixFtpService.SetGooseMatrixFtpForDevice(device,
                _gooseMatrixFtpService.GetGooseMatrixFtpForDevice(device, projectFrom), projectTo);
        }


        public void ChangeGooseInputOwnerName(IBiscProject biscProject, IDevice device, string newDeviceOwnerName)
        {
            _goosesModelService.ChengeGooseDeviceInputOwner(biscProject, device, newDeviceOwnerName);
        }
    }
}