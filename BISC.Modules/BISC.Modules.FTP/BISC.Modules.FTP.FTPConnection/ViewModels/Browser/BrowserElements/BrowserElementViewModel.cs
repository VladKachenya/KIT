using BISC.Infrastructure.Global.Services;
using BISC.Modules.FTP.FTPConnection.Events;
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
        protected IGlobalEventsService _globalEventsService;

        public BrowserElementViewModel(ICommandFactory commandFactory, IGlobalEventsService globalEventsService)
        {
            _commandFactory = commandFactory;
            _globalEventsService = globalEventsService;
            DeleteElementCommand = _commandFactory.CreatePresentationCommand(OnDeleteElementExecute);
        }

        private async void OnDeleteElementExecute()
        {
            _globalEventsService.SendMessage(new FTPInteraktionEvent(true));
            try
            {
                _globalEventsService.SendMessage(new FTPActionMassageEvent { Status = null, Message = "Удаление файла" + Name });
                await (Model as IDeviceBrowserElement)?.DeleteElementAsync();
                _globalEventsService.SendMessage(new FTPReloadEvent());
            }
            finally
            {
                _globalEventsService.SendMessage(new FTPInteraktionEvent(false));
            }

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
