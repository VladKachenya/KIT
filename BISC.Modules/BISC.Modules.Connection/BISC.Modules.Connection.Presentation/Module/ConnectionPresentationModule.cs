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
            _injectionContainer.RegisterType<object, PingView>(ConnectionKeys.PingViewKey);
            _injectionContainer.RegisterType<IPingViewModel, PingViewModel>(true);
            //Рекомендуется создавать экземпляры этого класса через Фабрику.
            _injectionContainer.RegisterType<IPingItemViewModel, PingItemViewModel>();
            _injectionContainer.RegisterType<IPingItemsViewModelFactory, PingItemsViewModelFactory>(true);
            var presentationInitialization = _injectionContainer.ResolveType(typeof(ConnectionPresentationInitialization)) as ConnectionPresentationInitialization;

        }
    }
}
