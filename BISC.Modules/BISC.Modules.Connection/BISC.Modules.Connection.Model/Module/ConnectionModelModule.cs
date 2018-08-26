using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Connection.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.Connection.Model.Connection;

namespace BISC.Modules.Connection.Model.Module
{
    public class ConnectionModelModule : IAppModule
    {
        private readonly IInjectionContainer _injectionContainer;

        public ConnectionModelModule(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }
        public void Initialize()
        {

            _injectionContainer.RegisterType<IPingService, PingService>(true);
            _injectionContainer.RegisterType<IIpValidationService, IpValidationServic>(true);
            _injectionContainer.RegisterType<IConnectionPoolService,ConnectionPoolService>(true);
            _injectionContainer.RegisterType<IDeviceConnection, DeviceConnection>();
            //_injectionContainer.RegisterType<IIpModel, IpModel>();
        }
    }
}
