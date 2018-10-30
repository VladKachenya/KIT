
using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;

namespace BISC.Modules.Gooses.Infrastructure.Services
{
    public interface IFtpGooseModelService
    {
        Task<List<GooseFtpDto>> GetGooseDtosFromDevice(string ip);
        Task<OperationResult> WriteGooseDtosToDevice(string ip, List<GooseFtpDto> gooseDtos);
    }
}