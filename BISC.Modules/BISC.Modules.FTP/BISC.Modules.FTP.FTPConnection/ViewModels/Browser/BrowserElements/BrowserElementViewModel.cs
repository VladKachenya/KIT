using BISC.Modules.FTP.Infrastructure.Model.BrowserElements;
using BISC.Modules.FTP.Infrastructure.ViewModels.Browser.BrowserElements;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.FTP.FTPConnection.ViewModels.Browser.BrowserElements
{
    public abstract class BrowserElementViewModel : ViewModelBase, IBrowserElementViewModel
    {
        private IDeviceDirectoryViewModel _parentDeviceDirectoryViewModel;
        protected ICommandFactory _commandFactory;

        public BrowserElementViewModel(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
            DeleteElementCommand = _commandFactory.CreatePresentationCommand(OnDeleteElementExecute);
        }

        private async void OnDeleteElementExecute()
        {
            await (Model as IDeviceBrowserElement)?.DeleteElementAsync();

        }


        public abstract object Model { get; set; }


        #region Implementation of IBrowserElementViewModel

        public IDeviceDirectoryViewModel ParentDeviceDirectoryViewModel
        {
            get { return _parentDeviceDirectoryViewModel; }
            set
            {
                _parentDeviceDirectoryViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeleteElementCommand { get; }

        public abstract string ElementPath { get; }
        public abstract string Name { get; }

        #endregion

    }
}
