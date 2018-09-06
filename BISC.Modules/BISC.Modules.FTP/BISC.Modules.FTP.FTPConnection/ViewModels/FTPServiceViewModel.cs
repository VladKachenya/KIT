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
using System.Collections.ObjectModel;
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
        private int _maxSizeOfList = 100; // Максимальный размер листа логирования 100

        #endregion

        #region C-tor
        public FTPServiceViewModel(IFTPClientWrapper ftpClientWrapper, ICommandFactory commandFactory, IGlobalEventsService globalEventsService,
            IIpAddressViewModelFactory ipAddressViewModelFactory, ILastIpAddressesViewModel lastIpAddressesViewModel)
        {
            _ftpClientWrapper = ftpClientWrapper;
            _commandFactory = commandFactory;
            _globalEventsService = globalEventsService;
            _ipAddressViewModelFactory = ipAddressViewModelFactory;
            LastIpAddressesViewModel = lastIpAddressesViewModel;
            ConnectToDeviceCommand = _commandFactory.CreatePresentationCommand(OnConnectToDeviceCommand, () => !_isConnectingInProcess);
            ResetDeviceCommand = _commandFactory.CreatePresentationCommand(OnResetDeviceCommand, CanExecuteResetDeviceCommand);
            this.FtpIpAddressViewModel = _ipAddressViewModelFactory.GetPingItemViewModel("....", false);
            LastIpAddressesViewModel.CurrentAddressViewModel = FtpIpAddressViewModel;
            FTPActionMessageList = new ObservableCollection<IFTPActionMessage>();
        }
        #endregion

        #region private methods
        private void AddNoteToActionMassageList( bool? status, string massage)
        {
            var note = new FTPActionMassage();
            note.Status = status;
            note.Message = massage;
            FTPActionMessageList.Add(note);
            while (FTPActionMessageList.Count > _maxSizeOfList)
                FTPActionMessageList.RemoveAt(0);
            
        }

        private async void OnConnectToDeviceCommand()
        {
            try
            {
                if (FtpIpAddressViewModel.FullIp != String.Empty)
                   await FtpIpAddressViewModel.PingGlobalEventAsync();
                else
                {
                    AddNoteToActionMassageList(false, "IP недоступен");
                    return;
                }
                if (FtpIpAddressViewModel.IsPingSuccess == true) AddNoteToActionMassageList(true, "Устройство найдено");
                else
                {
                    AddNoteToActionMassageList(false, "Устройство не найдено");
                    return;
                }
            }
            catch (Exception e)
            {
                AddNoteToActionMassageList(false, e.Message);
                return;
            }
            AddNoteToActionMassageList(null, "Подключение к устройству");
            _isConnectingInProcess = true;
            (ConnectToDeviceCommand as IPresentationCommand)?.RaiseCanExecute();
            try
            {
                await TryCloseConnection();
                var ftpClient = await _ftpClientWrapper.Connect(FtpIpAddressViewModel.FullIp, FtpLogin, FtpPassword);
                if (_ftpClientWrapper.IsConnected) AddNoteToActionMassageList(true, "Подключение произведено");
                else AddNoteToActionMassageList(false, "Подключение не произведено");
                //IBrowserElementFactory browserElementFactory = new FtpBrowserElementFactory(_container);
                //browserElementFactory.SetConnectionProvider(ftpClient);
                //IFileBrowser fileBrowser = new FileBrowser(browserElementFactory);
                //FileBrowserViewModel.Model = fileBrowser;
                //FileBrowserViewModel.LoadRootCommand.Execute(null);
            }
            catch (Exception e)
            {
                AddNoteToActionMassageList(_ftpClientWrapper.IsConnected, e.Message);
                await TryCloseConnection();
            }
            _isConnectingInProcess = false;
            AddNoteToActionMassageList(null, "Процесс подключения завершон");
            (ConnectToDeviceCommand as IPresentationCommand)?.RaiseCanExecute();

        }

        //private bool CanExecuteConnectToDeviceCommand()
        //{
        //    return !_isConnectingInProcess;
        //}

        private async void OnResetDeviceCommand()
        {
            AddNoteToActionMassageList(null, "Попытка перезапустить устройство");
            try
            {
                await _ftpClientWrapper.ResetDeviceAsync();
                //(_globalIecModel.DeviceConnection as IecConnection).Stop();
                AddNoteToActionMassageList(null, "Устройство перезапускается");
            }
            catch (Exception e)
            {
                AddNoteToActionMassageList(false, e.Message);
            }
        }

        private bool CanExecuteResetDeviceCommand()
        {
            return true;
        }

        private async Task TryCloseConnection()
        {
            AddNoteToActionMassageList(null, "Закрытие соединения");
            await _ftpClientWrapper.Disconnect();
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

        public ICommand ConnectToDeviceCommand { get; }

        public ICommand ResetDeviceCommand { get; }

        public ObservableCollection<IFTPActionMessage> FTPActionMessageList { get; }

        public ILastIpAddressesViewModel LastIpAddressesViewModel { get; }
        #endregion
    }
}
