using BISC.Modules.FTP.Infrastructure.Model.BrowserElements;
using BISC.Modules.FTP.Infrastructure.ViewModels.Browser.BrowserElements;
using BISC.Presentation.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.FTP.FTPConnection.ViewModels.Browser.BrowserElements
{
    public class DeviceFileViewModel : BrowserElementViewModel, IDeviceFileViewModel
    {
        private IDeviceBrowserElement _model;
        private string _elementPath;
        private string _name;

        public DeviceFileViewModel(ICommandFactory commandFactory)
            : base(commandFactory)
        {
            DownloadElementCommand = _commandFactory.CreatePresentationCommand(OnDownloadElementExecute);
            ParentDeviceDirectoryViewModel?.LoadDirectoryCommand?.Execute(null);
        }

        private void OnDownloadElementExecute()
        {
            (_model as IDeviceFile)?.Download();
        }


        #region Implementation of IViewModel<IDeviceBrowserElement>

        public override object Model
        {
            get { return _model; }
            set
            {
                _model = value as IDeviceBrowserElement;
                _elementPath = _model.ElementPath;
                _name = _model.Name;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(ElementPath));

            }
        }

        public override string ElementPath
        {
            get { return _elementPath; }
        }

        public override string Name
        {
            get { return _name; }
        }

        #endregion

        #region Implementation of IBrowserElementViewModel




        #endregion

        #region Implementation of IDeviceFileViewModel

        public ICommand DownloadElementCommand { get; }

        #endregion
    }
}
