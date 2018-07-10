using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Presentation.Interfaces;
using BISC.Modules.Device.Presentation.Interfaces.Factories;
using BISC.Modules.Device.Presentation.Interfaces.Services;
using BISC.Modules.Device.Presentation.Services;
using BISC.Modules.Device.Presentation.ViewModels;
using BISC.Modules.Device.Presentation.ViewModels.Factories;
using BISC.Modules.Device.Presentation.ViewModels.Tree;
using BISC.Modules.Device.Presentation.Views;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Device.Presentation.Module
{
    public class DevicePresentationModule : IAppModule
    {

        private readonly IInjectionContainer _injectionContainer;

        public DevicePresentationModule(IInjectionContainer injectionContainer )
        {
            _injectionContainer = injectionContainer;
        }

        public void Initialize()
        {
            _injectionContainer.RegisterType<IDeviceAddingService, DeviceAddingService>(true);
            _injectionContainer.RegisterType<object, DeviceAddingView>(DeviceKeys.DeviceAddingViewKey);
            _injectionContainer.RegisterType<IDeviceAddingViewModel, DeviceAddingViewModel>();
            _injectionContainer.RegisterType<IFileViewModel, FileViewModel>();
            _injectionContainer.RegisterType<IFileViewModelFactory, FileViewModelFactory>();
            _injectionContainer.RegisterType<IDeviceViewModelFactory, DeviceViewModelFactory>();
            _injectionContainer.RegisterType<IDeviceViewModel, DeviceViewModel>(true);
            _injectionContainer.RegisterType<DeviceTreeItemViewModelFactory>(true);
            _injectionContainer.RegisterType<DeviceTreeItemViewModel>();

            var presentationInitialization = _injectionContainer.ResolveType(typeof(DevicePresentationInitialization)) as DevicePresentationInitialization;

        }


    }
}