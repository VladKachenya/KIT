using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Modules.Connection.Infrastructure.Keys;
using BISC.Modules.Connection.Presentation.Factorys;
using BISC.Modules.Connection.Presentation.Interfaces.Factorys;
using BISC.Modules.Connection.Presentation.Interfaces.Ping;
using BISC.Modules.Connection.Presentation.Interfaces.Services;
using BISC.Modules.Connection.Presentation.Services;
using BISC.Modules.Connection.Presentation.View;
using BISC.Modules.Connection.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Connection.Presentation.Factorys.ChangeIpNetworkCard;
using BISC.Modules.Connection.Presentation.Interfaces.Factorys.ChangeIpNetworkCard;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel.ChangeIpNetworkCard;
using BISC.Modules.Connection.Presentation.View.ChangeIpNetworkCard;
using BISC.Modules.Connection.Presentation.ViewModels.ChangeIpNetworkCard;

namespace BISC.Modules.Connection.Presentation.Module
{
    public class ConnectionPresentationModule : IAppModule
    {
        private readonly IInjectionContainer _injectionContainer;
        public ConnectionPresentationModule(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }
        public void Initialize()
        {
            _injectionContainer.RegisterType<IConnectionPresentationViewAddingServise, ConnectionPresentationViewAddingServise>(true);
            _injectionContainer.RegisterType<object, PingView>(ConnectionKeys.PingViewKey);
            _injectionContainer.RegisterType<object, ChangeIpNetworkCardView>(ConnectionKeys.ChangeIpNetworkCardViewKey);
            _injectionContainer.RegisterType<IPingViewModel, PingViewModel>();
            //Рекомендуется создавать экземпляры этого класса через Фабрику.
            _injectionContainer.RegisterType<IIpAddressViewModel, IpAddressViewModel>();
            _injectionContainer.RegisterType<IIpAddressViewModelFactory, IpAddressViewModelFactory>(true);
            _injectionContainer.RegisterType<ILastIpAddressesViewModel, LastIpAddressesViewModel>();
            _injectionContainer.RegisterType<ILastIpAddressesViewModelFactory, LastIpAddressesViewModelFactory>(true);
            _injectionContainer.RegisterType<IChangeIpNetworkCardViewModel, ChangeIpNetworkCardViewModel>();
            _injectionContainer.RegisterType<ICurrentCardConfigurationViewModel, CurrentCardConfigurationViewModel>();
            _injectionContainer.RegisterType<ICustomNetworkCardSettingsViewModel, CustomNetworkCardSettingsViewModel>();
            _injectionContainer.RegisterType<ICustomIpSettingsViewModel, CustomIpSettingsViewModel>();
            _injectionContainer.RegisterType<ICustomIpSettingsViewModelFactory, CustomIpSettingsViewModelFactory>(true);

            var presentationInitialization = _injectionContainer.ResolveType(typeof(ConnectionPresentationInitialization)) as ConnectionPresentationInitialization;

        }
    }
}
