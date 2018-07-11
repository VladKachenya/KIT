using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Modules.Connection.Infrastructure.Keys;
using BISC.Modules.Connection.Presentation.Interfaces.Ping;
using BISC.Modules.Connection.Presentation.Interfaces.Services;
using BISC.Modules.Connection.Presentation.Services;
using BISC.Modules.Connection.Presentation.View.Ping;
using BISC.Modules.Connection.Presentation.ViewModels.Ping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _injectionContainer.RegisterType<IPingAddingServise, PingAddingServise>(true);
            _injectionContainer.RegisterType<object, PingAddingViev>(ConnectionKeys.PingAddingViewKey);
            _injectionContainer.RegisterType<IIpAddress, IpAddress>(true);
            _injectionContainer.RegisterType<IPingAddingViewModel, PingAddingViewModel>(true);
            var presentationInitialization = _injectionContainer.ResolveType(typeof(ConnectionPresentationInitialization)) as ConnectionPresentationInitialization;
        }
    }
}
