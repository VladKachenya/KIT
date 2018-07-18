using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Modules.Connection.Infrastructure.DeviceConnection;
using BISC.Modules.Connection.Infrastructure.Factorys;
using BISC.Modules.Connection.Model.Factorys;
using BISC.Modules.Connection.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _injectionContainer.RegisterType<IConnections, Connections>(true);
            _injectionContainer.RegisterType<IDeviceConnectionFactory, DeviceConnactionFactory>(true);
        }
    }
}
