using System.Collections.Generic;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Gooses.Presentation.Interfaces.Services
{
    public interface IGooseViewModelService
    {
        void IncrementConfRevisionGooseControls(IDevice device, List<string> dataSetsNames);
    }
}