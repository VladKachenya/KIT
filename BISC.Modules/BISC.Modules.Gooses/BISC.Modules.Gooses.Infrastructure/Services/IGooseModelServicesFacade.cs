using System.Collections.Generic;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Gooses.Infrastructure.Services
{
    public interface IGooseModelServicesFacade
    {
        /// <summary>
        /// Removing all goose inputs from project where owner name equals device name.
        /// </summary>
        /// <param name="biscProject">The project where goose inputs search.</param>
        /// <param name="deviceName">Owner name by which searching and removing.</param>
        void RemoveGooseInputsByDeviceName(IBiscProject biscProject, string deviceName);

        /// <summary>
        /// Set goose subscriptions and goose inputs of device to project from another project.
        /// </summary>
        /// <param name="device">The device for which set in goose subscriptions and goose inputs.</param>
        /// <param name="projectTo">The project where to set goose subscriptions and goose inputs.</param>
        /// <param name="projectFrom">The project where from take goose subscriptions and goose inputs.</param>
        void SetGooseReceiving(IDevice device, IBiscProject projectTo, IBiscProject projectFrom);

        /// <summary>
        /// Change goose inputs owner name to new owner name in project.
        /// </summary>
        /// <param name="biscProject">The project where changing owners of goose inputs.</param>
        /// <param name="device">The device whose goose inputs change the owner's names.</param>
        /// <param name="newDeviceOwnerName">The owner name which set as new owner name of goose inputs of device.</param>
        void ChangeGooseInputOwnerName(IBiscProject biscProject, IDevice device, string newDeviceOwnerName);
    }
}