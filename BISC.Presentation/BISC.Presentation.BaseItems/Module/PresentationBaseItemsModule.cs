using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;

namespace BISC.Presentation.BaseItems.Module
{
   public class PresentationBaseItemsModule:IAppModule
    {
        private readonly IInjectionContainer _injectionContainer;

        public PresentationBaseItemsModule(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }
        public void Initialize()
        {

        }
    }
}
