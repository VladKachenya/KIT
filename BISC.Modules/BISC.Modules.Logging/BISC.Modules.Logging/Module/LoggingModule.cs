using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Logging.Infrastructure.Keys;
using BISC.Modules.Logging.Infrastructure.Services;
using BISC.Modules.Logging.Infrastructure.ViewModels;
using BISC.Modules.Logging.Infrastructure.Views;
using BISC.Presentation.BaseItems.Events;
using BISC.Presentation.Infrastructure.Keys;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Logging.Infrastructure.Module
{
    public class LoggingModule:IAppModule
    {
        private readonly IInjectionContainer _injectionContainer;

        public LoggingModule(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }


        #region Implementation of IAppModule

        public void Initialize()
        {
            _injectionContainer.RegisterType<ILoggingService,LoggingService>(true);
            _injectionContainer.RegisterType<LogViewModel>();
            _injectionContainer.RegisterType<NotificationBarViewModel>();

            _injectionContainer.RegisterType<object, LogView>(LogKeys.LogKeysPresentation.LogViewKey);
            _injectionContainer.RegisterType<object, NotificationBarView>(LogKeys.LogKeysPresentation.NotificationBarViewName);

            _injectionContainer.ResolveType<IGlobalEventsService>().Subscribe<ShellLoadedEvent>((args =>
            {
                _injectionContainer.ResolveType<INavigationService>().NavigateViewToRegion(LogKeys.LogKeysPresentation.NotificationBarViewName,
                    KeysForNavigation.RegionNames.NotificationBarKey);
            }));
        }

        #endregion
    }
}