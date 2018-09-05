using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Loading.Events;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Interfaces.Services;
using BISC.Presentation.BaseItems.Commands;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Device.Presentation.Services
{
    public class DeviceLoadingService : IDeviceLoadingService
    {
        private readonly IDeviceModelService _deviceModelService;
        private readonly ITreeManagementService _treeManagementService;
        private readonly IDeviceAddingService _deviceAddingService;
        private readonly Func<ISclModel> _sclModelCreator;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly IBiscProject _biscProject;
        private List<IDeviceElementLoadingService> _elementLoadingServices;

        public DeviceLoadingService(IInjectionContainer injectionContainer,IDeviceModelService deviceModelService,
            ITreeManagementService treeManagementService, IDeviceAddingService deviceAddingService,
            Func<ISclModel> sclModelCreator, IGlobalEventsService globalEventsService,IBiscProject biscProject)
        {
            _deviceModelService = deviceModelService;
            _treeManagementService = treeManagementService;
            _deviceAddingService = deviceAddingService;
            _sclModelCreator = sclModelCreator;
            _globalEventsService = globalEventsService;
            _biscProject = biscProject;
            _elementLoadingServices = injectionContainer.ResolveAll(typeof(IDeviceElementLoadingService))
                .Cast<IDeviceElementLoadingService>().ToList();
        }

        public void Dispose()
        {
            _elementLoadingServices.ForEach((service => service.Dispose()));
        }

        public async Task LoadElements(List<IDevice> devicesToLoad)
        {
            var sortedElements = _elementLoadingServices.OrderBy((service => service.Priority));

            foreach (var device in devicesToLoad)
            {
                BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
                biscNavigationParameters.AddParameterByName(DeviceKeys.DeviceModelKey, device);
                DialogCommands.CloseDialogCommand.Execute(null, null);
                var treeItemId = _treeManagementService.AddTreeItem(biscNavigationParameters,
                    DeviceKeys.DeviceLoadingTreeItemViewKey,
                    null);
                var sclModel = _sclModelCreator();
                var itemsCount = 0;

                foreach (var sortedElement in sortedElements)
                {
                    itemsCount += await sortedElement.EstimateProgress(device);
                }
                _deviceModelService.AddDeviceInModel(sclModel, device);

                int currentElementsCount = 0;


                foreach (var sortedElement in sortedElements)
                {
                    await sortedElement.Load(device,
                        new Progress<object>(deviceLoadingEvent =>
                        {
                            _globalEventsService.SendMessage(new DeviceLoadingEvent(device.Ip, device.Name,
                                itemsCount, ++currentElementsCount));
                        }),sclModel);
                }

                _treeManagementService.DeleteTreeItem(treeItemId);
                _deviceAddingService.AddDevicesInProject(new List<IDevice>() {device}, sclModel);
            }
        }

    }
}