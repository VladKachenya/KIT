using System;
using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public interface IConfigurableModelElementsGetter
    {
        string ModuleName { get; }
        IEnumerable<IModelElement> GetConfigurableModelElements(IDevice device, ISclModel sclModel);
    }
}