using BISC.Infrastructure.Global.Services;
using BISC.Modules.FTP.Infrastructure.Model;
using BISC.Modules.FTP.Infrastructure.ViewModels;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BISC.Modules.FTP.FTPConnection.ViewModels
{
    public class FTPServiceViewModel : ViewModelBase, IFTPServiceViewModel
    {
        #region ptivate filds
        private IFTPClientWrapper _ftpClientWrapper;
        private ICommandFactory _commandFactory;
        private IGlobalEventsService _globalEventsService;
        private bool _isConnectingInProcess;

        #endregion

        #region C-tor
        public FTPServiceViewModel(IFTPClientWrapper ftpClientWrapper, ICommandFactory commandFactory, IGlobalEventsService globalEventsService)
        {
            _ftpClientWrapper = ftpClientWrapper;
            _commandFactory = commandFactory;
            _globalEventsService = globalEventsService;
            ConnectToDeviceCommand = _commandFactory.CreatePresentationCommand(OnConnectToDeviceCommand, CanExecuteConnectToDeviceCommand);
            ResetDeviceCommand = _commandFactory.CreatePresentationCommand(OnResetDeviceCommand, CanExecuteResetDeviceCommand);

        }
        #endregion

        #region private methods

        private async void OnConnectToDeviceCommand()
        {
            _isConnectingInProcess = true;
            (ConnectToDeviceCommand as IPresentationCommand)?.RaiseCanExecute();
            try
            {
                await TryCloseConnection();
                var ftpClient = await _ftpClientWrapper.Connect(FtpIpAddress, FtpLogin, FtpPassword);
                IsFtpConnected = _ftpClientWrapper.IsConnected;
                //IBrowserElementFactory browserElementFactory = new FtpBrowserElementFactory(_container);
                //browserElementFactory.SetConnectionProvider(ftpClient);
                //IFileBrowser fileBrowser = new FileBrowser(browserElementFactory);
                //FileBrowserViewModel.Model = fileBrowser;
                //FileBrowserViewModel.LoadRootCommand.Execute(null);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                await TryCloseConnection();
                MessageBox.Show(e.Message); // В дальнейшем необходимо выводить это сообщение в ToolTip
            }
            _isConnectingInProcess = false;
            (ConnectToDeviceCommand as IPresentationCommand)?.RaiseCanExecute();

        }

        private bool CanExecuteConnectToDeviceCommand()
        {
            return true; //_isConnectingInProcess;
        }

        private async void OnResetDeviceCommand()
        {
            try
            {
                await _ftpClientWrapper.ResetDeviceAsync();
                //(_globalIecModel.DeviceConnection as IecConnection).Stop();
                MessageBox.Show("Устройство перезапускается");
                //if (window is Window)
                //{
                //    (window as Window)?.Close();
                //}
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private bool CanExecuteResetDeviceCommand()
        {
            return true;
        }

        private async Task TryCloseConnection()
        {
            await _ftpClientWrapper.Disconnect();
            IsFtpConnected = _ftpClientWrapper.IsConnected;
        }

        #endregion

        #region Implementation of IFTPSreviceViewModel

        public string FtpIpAddress { get; set; }
        public string FtpPassword { get; set; }
        public string FtpLogin { get; set; }
        public bool IsFtpConnected { get; set; }

        public ICommand ConnectToDeviceCommand { get; }

        public ICommand ResetDeviceCommand { get; }
        #endregion
    }
}
