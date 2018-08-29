using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.Services
{
    public interface ILogicalDeviceLoadingService
    {
        Task PrepareProgressData(string ip);
        int GetLogicalNodeCount();
        string GetDeviceName();
        Task<List<ILDevice>> GetLDeviceFromConnection(IProgress<LogicalNodeLoadingEvent> progress);
    }

    public class LogicalNodeLoadingEvent
    {

    }
}