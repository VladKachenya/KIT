using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.FTP.Infrastructure.ViewModels.Browser.BrowserElements
{
    public interface IDeviceDirectoryViewModel : IBrowserElementViewModel
    {
        ObservableCollection<IBrowserElementViewModel> ChildBrowserElementViewModels { get; }
        ICommand LoadDirectoryCommand { get; }
        ICommand CreateChildDirectoryCommand { get; }
        ICommand UploadFileInDirectoryCommand { get; }
    }
}
