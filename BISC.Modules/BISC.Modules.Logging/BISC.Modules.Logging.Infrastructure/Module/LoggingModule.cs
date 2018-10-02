using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Logging.Infrastructure.Services;

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
            _injectionContainer.RegisterType<ILoggingService,LoggingService>();
        }

        #endregion
    }
}