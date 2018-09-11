using BISC.Modules.FTP.Infrastructure.ViewModels.Browser.BrowserElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.FTP.Infrastructure.ViewModels.Browser
{
    public interface IFileBrowserViewModel : IModel
    {
        IDeviceDirectoryViewModel RootDeviceDirectoryViewModel { get; }
        IDeviceDirectoryViewModel SelectedDirectoryViewModel { get; set; }
        ICommand SelectDirectoryCommand { get; }
        ICommand LoadRootCommand { get; }
    }
}
