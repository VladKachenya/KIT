using BISC.Infrastructure.Global.IoC;
using BISC.Modules.FTP.FTPConnection.ViewModels.Browser.BrowserElements;
using BISC.Modules.FTP.Infrastructure.Factorys;
using BISC.Modules.FTP.Infrastructure.Model.BrowserElements;
using BISC.Modules.FTP.Infrastructure.ViewModels.Browser.BrowserElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.FTPConnection.Factory
{
    public class BrowserElementViewModelFactory : IBrowserElementViewModelFactory
    {
        private readonly IInjectionContainer _injectionContainer;

        #region privat methods

        public BrowserElementViewModelFactory(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }

        #endregion

        #region Implementation of IBrowserElementViewModelFactory

        public IBrowserElementViewModel CreateBrowserElementViewModelBase(IDeviceBrowserElement deviceBrowserElement, IDeviceDirectoryViewModel parentDeviceDirectoryViewModel)
        {
            IBrowserElementViewModel browserElementViewModel = _injectionContainer.ResolveType( typeof(IBrowserElementViewModel), deviceBrowserElement.StrongName) as IBrowserElementViewModel;
            browserElementViewModel.Model = deviceBrowserElement;
            browserElementViewModel.ParentDeviceDirectoryViewModel = parentDeviceDirectoryViewModel;
            return browserElementViewModel;
        }

        #endregion
    }
}
