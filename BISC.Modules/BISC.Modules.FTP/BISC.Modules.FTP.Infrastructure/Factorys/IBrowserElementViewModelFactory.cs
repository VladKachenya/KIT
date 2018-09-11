using BISC.Modules.FTP.Infrastructure.Model.BrowserElements;
using BISC.Modules.FTP.Infrastructure.ViewModels.Browser.BrowserElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.Infrastructure.Factorys
{
    public interface IBrowserElementViewModelFactory
    {
        IBrowserElementViewModel CreateBrowserElementViewModelBase(IDeviceBrowserElement deviceBrowserElement, IDeviceDirectoryViewModel parentDeviceDirectoryViewModel);

    }
}
