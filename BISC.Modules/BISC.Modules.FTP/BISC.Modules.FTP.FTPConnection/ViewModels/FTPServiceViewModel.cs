using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Presentation.Interfaces.Factorys;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel;
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
        private IIpAddressViewModelFactory _ipAddressViewModelFactory;
        private bool _isConnectingInProcess;
        private string _ftpPassword;
        private string _ftpLogin;
        private bool? _isFtpConnectid;
        private string _toolTipForConnection;

        #endregion

        #region C-tor
        public FTPServiceViewModel(IFTPClientWrapper ftpClientWrapper, ICommandFactory commandFactory, IGlobalEventsService globalEventsService,
            IIpAddressViewModelFactory ipAddressViewModelFactory)
        {
            _ftpClientWrapper = ftpClientWrapper;
            _commandFactory = commandFactory;
            _globalEventsService = globalEventsService;
            _ipAddressViewModelFactory = ipAddressViewModelFactory;
            ConnectToDeviceCommand = _commandFactory.CreatePresentationCommand(OnConnectToDeviceCommand, () => !_isConnectingInProcess);
            ResetDeviceCommand = _commandFactory.CreatePresentationCommand(OnResetDeviceCommand, CanExecuteResetDeviceCommand);
            this.FtpIpAddressViewModel = _ipAddressViewModelFactory.GetPingItemViewModel("....", false);
            IsFtpConnected = null;
            ToolTipForConnection = "Ожидается подключение";
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
                var ftpClient = await _ftpClientWrapper.Connect(FtpIpAddressViewModel.FullIp, FtpLogin, FtpPassword);
                IsFtpConnected = _ftpClientWrapper.IsConnected;
                ToolTipForConnection = "Подключение произведено";
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
                IsFtpConnected = _ftpClientWrapper.IsConnected;
                ToolTipForConnection = e.Message;
            }
            _isConnectingInProcess = false;
            (ConnectToDeviceCommand as IPresentationCommand)?.RaiseCanExecute();

        }

        //private bool CanExecuteConnectToDeviceCommand()
        //{
        //    return !_isConnectingInProcess;
        //}

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

        public IIpAddressViewModel FtpIpAddressViewModel { get; set; }
        public string FtpLogin
        {
            get => _ftpLogin;
            set => SetProperty(ref _ftpLogin, value); 
        }
        public string FtpPassword
        {
            get => _ftpPassword;
            set => SetProperty(ref _ftpPassword, value);
        }

        public bool? IsFtpConnected
        {
            get => _isFtpConnectid;
            set => SetProperty(ref _isFtpConnectid, value);
        }
        public string ToolTipForConnection
        {
            get => _toolTipForConnection;
            set => SetProperty(ref _toolTipForConnection, value);
        }

        public ICommand ConnectToDeviceCommand { get; }

        public ICommand ResetDeviceCommand { get; }
        #endregion
    }
}
