using System.Collections.Generic;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

namespace BISC.Modules.Gooses.Infrastructure.Services
{
    public interface IGooseModelServicesFacade
    {
        // IGooseMatrixFtpService
        IGooseMatrixFtp GetGooseMatrix(IDevice device, IBiscProject biscProject = null);

        // IGooseMatrixFtpService
        void SetGooseMatrix(IDevice device, IGooseMatrixFtp gooseMatrixFtp, IBiscProject biscProject = null);

        //IGoosesModelService
        List<IGooseInputModelInfo> GetGooseInputs(IDevice device, IBiscProject biscProject = null);

        //IGoosesModelService
        void ChangeGooseInputOwnerName(IBiscProject biscProject, IDevice device, string newDeviceOwnerName);

        //IGoosesModelService
        void DeleteGooseInputsByDeviceName(IBiscProject biscProject, string deviceName);

        void SetGooseReceivingAndSending(IDevice device, IBiscProject projectTo, IBiscProject projectFrom);
    }
}