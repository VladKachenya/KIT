
using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

namespace BISC.Modules.Gooses.Infrastructure.Services
{
    public interface IFtpGooseModelService
    {
        Task<OperationResult<List<GooseFtpDto>>> GetGooseDtosFromDevice(string ip);
        Task<OperationResult> WriteGooseDtosToDevice(IDevice device, List<GooseFtpDto> gooseDtos);
	    Task<OperationResult<IGooseMatrixFtp>> GetGooseMatrixByFtp(string ip);
        Task<OperationResult> WriteGooseMatrixFtpToDevice(IDevice Device, IGooseMatrixFtp gooseMatrixFtp);
        Task<OperationResult<List<IGooseInputModelInfo>>> GetGooseDeviceInputFromDevice(string ip, string deviceName);
        Task<OperationResult> WriteGooseDeviceInputFromDevice(string ip, List<IGooseInputModelInfo> gooseInputModelInfos);
        Task<OperationResult> DeletGoosesAndResetDevice(IDevice device);

    }
}