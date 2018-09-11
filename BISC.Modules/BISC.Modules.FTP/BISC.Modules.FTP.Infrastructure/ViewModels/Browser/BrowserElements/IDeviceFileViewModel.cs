using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.FTP.Infrastructure.ViewModels.Browser.BrowserElements
{
    public interface IDeviceFileViewModel : IBrowserElementViewModel
    {
        ICommand DownloadElementCommand { get; }
    }
}
