using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Modules.FTP.FTPConnection.Model;
using BISC.Modules.FTP.FTPConnection.Services;
using BISC.Modules.FTP.FTPConnection.ViewModels;
using BISC.Modules.FTP.FTPConnection.Views;
using BISC.Modules.FTP.Infrastructure.Keys;
using BISC.Modules.FTP.Infrastructure.Model;
using BISC.Modules.FTP.Infrastructure.Serviсes;
using BISC.Modules.FTP.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.FTPConnection.Module
{
    public class FTPConnectionModule : IAppModule
    {
        private readonly IInjectionContainer _injectionContainer;

        public FTPConnectionModule(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }

        public void Initialize()
        {
            // Работать сдесь!!!
            _injectionContainer.RegisterType<IFTPAddingServise, FTPAddingService>(true);
            _injectionContainer.RegisterType<IFTPServiceViewModel, FTPServiceViewModel>();
            _injectionContainer.RegisterType<IFTPClientWrapper, FTPClientWrapper>();
            _injectionContainer.RegisterType<object, FTPServiceView>(FTPKeys.FTPServiceViewKey);

            var presentationInitialization = _injectionContainer.ResolveType(typeof(FTPConnectionInitialization)) as FTPConnectionInitialization;
        }
    }
}
