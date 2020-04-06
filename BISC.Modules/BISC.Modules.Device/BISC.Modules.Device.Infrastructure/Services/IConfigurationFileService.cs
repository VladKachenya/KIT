using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public interface IConfigurationFileService
    {
        Task<OperationResult> SaveConfigurationToFile(ISclModel sclModel, IDevice device, string path);
    }
}