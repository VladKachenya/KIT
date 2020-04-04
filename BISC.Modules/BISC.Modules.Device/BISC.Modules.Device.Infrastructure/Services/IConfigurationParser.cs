using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public interface IConfigurationParser
    {
        OperationResult<string> GetConfiguration(IEnumerable<IModelElement> modelElements);
    }
}