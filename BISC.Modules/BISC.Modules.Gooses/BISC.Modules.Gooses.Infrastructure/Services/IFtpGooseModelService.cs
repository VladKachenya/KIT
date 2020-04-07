
using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

namespace BISC.Modules.Gooses.Infrastructure.Services
{
    public interface IFtpGooseModelService
    {
        Task<OperationResult<List<GooseFtpDto>>> GetGooseDtosFromDevice(string ip);
        Task<OperationResult<IGooseMatrixFtp>> GetGooseMatrixByFtp(string ip);
        Task<OperationResult<List<IGooseInputModelInfo>>> GetGooseDeviceInputFromDevice(string ip, string deviceName);
        Task<OperationResult> WriteGooseToDevice(IDevice device, IEnumerable<IGooseControl> gooseControls);
        Task<OperationResult> WriteGooseMatrixFtpToDevice(IDevice device, IGooseMatrixFtp gooseMatrixFtp);
        Task<OperationResult> WriteGooseDeviceInputFromDevice(IDevice device, List<IGooseInputModelInfo> gooseInputModelInfos);
        Task<OperationResult> DeleteGoosesAndResetDevice(IDevice device);

    }
}