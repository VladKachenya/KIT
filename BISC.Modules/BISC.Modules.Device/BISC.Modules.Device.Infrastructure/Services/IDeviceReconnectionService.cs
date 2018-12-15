using System;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public interface IDeviceReconnectionService
    {
        Task<bool> ReconnectDevice(IDevice existingDevice, TreeItemIdentifier treeItemIdToRemove);

        Task RestartDevice(IDevice existingDevice,TreeItemIdentifier treeItemIdToRemove);
	    Task ExecuteBeforeRestart(Func<Task> taskToExecute, IDevice existingDevice);
    }
}