﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.Services
{
    public interface ILogicalDevice
    {
        Task PrepareProgressData(string ip);
        int GetLogicalNodeCount();
        string GetDeviceName();
        Task<List<ILDevice>> GetLDeviceFromConnection(IProgress<LogicalNodeLoadingEvent> progress, ISclModel sclModel,string deviceName,CancellationToken cancellationToken);
    }

    public class LogicalNodeLoadingEvent
    {

    }
}