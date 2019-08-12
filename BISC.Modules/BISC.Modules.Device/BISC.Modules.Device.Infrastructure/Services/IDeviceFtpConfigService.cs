using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Modules.Device.Infrastructure.Model.Config;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public interface IDeviceFtpConfigService
    {
        Task<OperationResult<IDeviceFtpConfig>> ReadDeviceFtpConfig(string ip,string customPath=null);
	    Task<OperationResult> SaveDeviceFtpConfig(string ip, IDeviceFtpConfig deviceFtpConfigToSave);
    }
}