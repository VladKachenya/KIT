using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Interfaces;
using BISC.Presentation.Services;
using BISC.Presentation.ViewModels.Tab;

namespace BISC.Presentation.Module
{
   public class GlobalPresentationModule:IAppModule
    {
        private readonly IInjectionContainer _injectionContainer;

        public GlobalPresentationModule(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }
        public void Initialize()
        {
            _injectionContainer.RegisterType<ITabManagementService, TabManagementService>();
            _injectionContainer.RegisterType<ITabHostViewModel, TabHostViewModel>(true);
            _injectionContainer.RegisterType<ITabViewModel,TabViewModel>();

        }
    }
}
