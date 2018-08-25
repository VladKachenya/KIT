using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.Connection.MMS.MmsClientServices;

namespace BISC.Modules.Connection.MMS.Module
{
   public class ConnectionMmsModule:IAppModule
    {
        private readonly IInjectionContainer _injectionContainer;


        public ConnectionMmsModule(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }
        public void Initialize()
        {
           _injectionContainer.RegisterType<IMmsConnectionFacade,MmsConnectionFacade>();
        }
    }
}
