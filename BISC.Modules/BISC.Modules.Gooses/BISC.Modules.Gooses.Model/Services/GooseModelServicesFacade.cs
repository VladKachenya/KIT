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

        public GooseModelServicesFacade(
            IGooseMatrixFtpService gooseMatrixFtpService,
            IGoosesModelService goosesModelService)
        {
            _gooseMatrixFtpService = gooseMatrixFtpService;
            _goosesModelService = goosesModelService;
        }

        public void DeleteGooseInputsByDeviceName(IBiscProject biscProject, string deviceName)
        {
            _goosesModelService.DeleteAllDeviceReferencesInGooseControlsInModel(biscProject, deviceName);
        }

        public void SetGooseReceivingAndSending(IDevice device, IBiscProject projectTo, IBiscProject projectFrom)
        {
            // Устанавливаем полученные Goose подписки из sclMode в наш текущий проект
            _goosesModelService.SetGooseInputModelInfosToProject(projectTo, device,
                _goosesModelService.GetGooseInputModelInfos(device, projectFrom));
            // Устанавливаем полученную Goose матрицн из sclMode в наш текущий проект
            _gooseMatrixFtpService.SetGooseMatrixFtpForDevice(device,
                _gooseMatrixFtpService.GetGooseMatrixFtpForDevice(device, projectFrom), projectTo);
        }

        public IGooseMatrixFtp GetGooseMatrix(IDevice device, IBiscProject biscProject = null)
        {
            return _gooseMatrixFtpService.GetGooseMatrixFtpForDevice(device, biscProject);
        }

        public void SetGooseMatrix(IDevice device, IGooseMatrixFtp gooseMatrixFtp, IBiscProject biscProject = null)
        {
            _gooseMatrixFtpService.SetGooseMatrixFtpForDevice(device, gooseMatrixFtp, biscProject);
        }

        public List<IGooseInputModelInfo> GetGooseInputs(IDevice device, IBiscProject biscProject = null)
        {
            return _goosesModelService.GetGooseInputModelInfos(device, biscProject);
        }

        public void ChangeGooseInputOwnerName(IBiscProject biscProject, IDevice device, string newDeviceOwnerName)
        {
            _goosesModelService.ChengeGooseDeviceInputOwner(biscProject, device, newDeviceOwnerName);
        }
    }
}