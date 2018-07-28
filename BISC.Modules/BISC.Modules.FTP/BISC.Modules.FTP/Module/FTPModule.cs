using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Modules.FTP.Infrastructure.Keys;
using BISC.Modules.FTP.Infrastructure.Serviсes;
using BISC.Modules.FTP.Infrastructure.ViewModel;
using BISC.Modules.FTP.Services;
using BISC.Modules.FTP.View;
using BISC.Modules.FTP.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.Module
{
    public class FTPModule : IAppModule
    {
        private readonly IInjectionContainer _injectionContainer;

        public FTPModule(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }

        public void Initialize()
        {
            // Работать сдесь.!!!
            _injectionContainer.RegisterType<IFTPAddingServise, FTPAddingService>(true);
            _injectionContainer.RegisterType<IFTPServiceViewModel, FTPServiceViewModel>();
            _injectionContainer.RegisterType<object, FTPServiceView>(FTPKeys.FTPServiceViewKey);

            var presentationInitialization = _injectionContainer.ResolveType(typeof(FTPInitialization)) as FTPInitialization;
        }
    }
}
