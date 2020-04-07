using System.Collections.Generic;
using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public interface IConfigurationParser
    {
        OperationResult<string> GetConfiguration(IEnumerable<IModelElement> modelElements, IDevice owningDevice);
    }
}