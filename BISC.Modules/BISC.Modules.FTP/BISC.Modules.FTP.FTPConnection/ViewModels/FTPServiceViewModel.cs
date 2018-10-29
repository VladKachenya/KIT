using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Presentation.Interfaces.Factorys;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel;
using BISC.Modules.FTP.FTPConnection.Model;
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
using BISC.Modules.FTP.Infrastructure.Model.Factory;
using BISC.Modules.FTP.FTPConnection.Model.Factory;
using BISC.Infrastructure.Global.IoC;
using BISC.Modules.FTP.Infrastructure.ViewModels.Browser;
using BISC.Modules.FTP.FTPConnection.Events;
using System.Windows.Forms;
using System.IO;
using BISC.Presentation.BaseItems.Commands;

namespace BISC.Modules.FTP.FTPConnection.ViewModels
{
    public class FTPServiceViewModel : ComplexViewModelBase, IFTPServiceViewModel
    {
        #region ptivate filds
        private IFTPClientWrapper _ftpClientWrapper;
        private ICommandFactory _commandFactory;
        private IIpAddressViewModelFactory _ipAddressViewModelFactory;
        private IInjectionContainer _injectionContainer;
        private IGlobalEventsService _globalEventsService;
        private IIpAddressViewModel _ipAddressViewModel;
        private ILastIpAddressesViewModel _lastIpAddressesViewModel;
        private bool _isAnimate;
        private bool _isConnectingInProcess;
        private string _ftpPassword;
        private string _ftpLogin;
        private int _maxSizeOfList = 100; // Размер листа логирования 

        #endregion

        #region C-tor
        public FTPServiceViewModel(IFTPClientWrapper ftpClientWrapper, ICommandFactory commandFactory, IIpAddressViewModelFactory ipAddressViewModelFactory,
            ILastIpAddressesViewModelFactory lastIpAddressesViewModelFactory, IInjectionContainer injectionContainer, IGlobalEventsService globalEventsService,
            IFileBrowserViewModel fileBrowserViewModel)
        {
            _globalEventsService = globalEventsService;
            _ftpClientWrapper = ftpClientWrapper;
            _commandFactory = commandFactory;
            _ipAddressViewModelFactory = ipAddressViewModelFactory;
            _injectionContainer = injectionContainer;
            FileBrowserViewModel = fileBrowserViewModel;

            ConnectToDeviceCommand = _commandFactory.CreatePresentationCommand(OnConnectToDeviceCommand, () => !IsConnectingInProcess);
            ResetDeviceCommand = _commandFactory.CreatePresentationCommand(OnResetDeviceCommand, CanExecuteResetDeviceCommand);
            CloseCommand = commandFactory.CreatePresentationCommand((() =>
            {
                DialogCommands.CloseDialogCommand.Execute(null, null);
                Dispose();
            }));

            _globalEventsService.Subscribe<FTPChangingConnectionEvent>(obj => VerifyConnection());
            _globalEventsService.Subscribe<FTPActionMassageEvent>(obj => AddNoteToActionMassageList(obj.Status, obj.Message));
            _globalEventsService.Subscribe<FTPInteraktionEvent>(obj => IsAnimate = obj.IsInteractFTP);
            FTPActionMessageList = new ObservableCollection<IFTPActionMessage>();

            this.FtpIpAddressViewModel = _ipAddressViewModelFactory.GetPingItemViewModel("...", false);
            lastIpAddressesViewModelFactory.BuildLastPingIpAdresses(out _ipAddressViewModel, out _lastIpAddressesViewModel);
            OnPropertyChanged(nameof(FileBrowserViewModel));
        }
        #endregion

        #region private methods

        private async void OnConnectToDeviceCommand()
        {
            IsAnimate = true;
            try
            { 
                //Проверка правильности IP
                try
                {
                    // Проверка валидности IP
                    if (FtpIpAddressViewModel.FullIp != "..." && FtpIpAddressViewModel.FullIp != String.Empty)
                    {
                        await FtpIpAddressViewModel.PingGlobalEventAsync();
                    }
                    else
                    {
                        AddNoteToActionMassageList(false, "IP недоступен");
                        return;
                    }
                    //Проверка пинга 
                    if (FtpIpAddressViewModel.IsPingSuccess == true)

                        AddNoteToActionMassageList(true, "Устройство найдено");
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
           
                // Начало процесса подключения
                IsConnectingInProcess = true;
                try
                {
                    await TryCloseConnection();
                    AddNoteToActionMassageList(null, "Подключение к устройству");
                    var ftpClient = await _ftpClientWrapper.Connect(FtpIpAddressViewModel.FullIp, FtpLogin, FtpPassword);

                    if (_ftpClientWrapper.IsConnected) AddNoteToActionMassageList(true, "Подключение произведено");
                    else AddNoteToActionMassageList(false, "Подключение не произведено");

                    // Создание объекта файловой системы устройства
                    IBrowserElementFactory browserElementFactory = _injectionContainer.ResolveType<IBrowserElementFactory>() ;
                    browserElementFactory.SetConnectionProvider(ftpClient);
                    IFileBrowser fileBrowser = new FileBrowser(browserElementFactory);
                    FileBrowserViewModel.Model = fileBrowser;
                    FileBrowserViewModel.LoadRootCommand.Execute(null);
                }
                catch (Exception e)
                {
                    AddNoteToActionMassageList(_ftpClientWrapper.IsConnected, e.Message);
                    await TryCloseConnection();
                }
            }
            finally
            {
                IsAnimate = false;
            }
            IsConnectingInProcess = false;
            AddNoteToActionMassageList(null, "Процесс подключения завершен");

        }

        private async void OnResetDeviceCommand()
        {
            IsAnimate = true;
            try
            {
                AddNoteToActionMassageList(null, "Перезапуск устройства");
                await _ftpClientWrapper.ResetDeviceAsync();
            }
            catch (Exception e)
            {
                AddNoteToActionMassageList(false, e.Message);
            }
            finally
            {
                IsAnimate = false;
            }
        }

        public IFileBrowserViewModel FileBrowserViewModel { get; }

        private bool CanExecuteResetDeviceCommand()
        {
            return _ftpClientWrapper.IsConnected;
        }

        private async Task TryCloseConnection()
        {
            AddNoteToActionMassageList(null, "Закрытие соединения");
            await _ftpClientWrapper.Disconnect();
        }

        private bool IsConnectingInProcess {
            get
            {
                return _isConnectingInProcess;
            }
            set
            {
                _isConnectingInProcess = value;
                (ConnectToDeviceCommand as IPresentationCommand)?.RaiseCanExecute();
            }
        }

        private void VerifyConnection()
        {
            (ResetDeviceCommand as IPresentationCommand)?.RaiseCanExecute();
        }

        private void AddNoteToActionMassageList(bool? status, string massage)
        {
            var note = new FTPActionMassage() { Status = status, Message = massage };
            FTPActionMessageList.Insert(0, note);
            while (FTPActionMessageList.Count > _maxSizeOfList)
                FTPActionMessageList.RemoveAt(0);

        }

        #endregion

        #region Implementation of IFTPSreviceViewModel

        public IIpAddressViewModel FtpIpAddressViewModel
        {
            get => _ipAddressViewModel;
            set => SetProperty(ref _ipAddressViewModel, value);
        }
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

        public bool IsAnimate
        {
            get => _isAnimate;
            set
            {
                if (_isAnimate && value || !_isAnimate && !value)
                    return;
                SetProperty(ref _isAnimate, value);
            }
        }
        
        public ICommand ConnectToDeviceCommand { get; }

        public ICommand ResetDeviceCommand { get; }
        public ICommand CloseCommand { get; }


        public ObservableCollection<IFTPActionMessage> FTPActionMessageList { get; }

        public ILastIpAddressesViewModel LastIpAddressesViewModel
        {
            get => _lastIpAddressesViewModel;
        }
        #endregion

        #region Overrides of ViewModelBase

        protected override void OnDisposing()
        {
            _globalEventsService.Unsubscribe<FTPChangingConnectionEvent>(obj => VerifyConnection());
            _globalEventsService.Unsubscribe<FTPActionMassageEvent> (obj => AddNoteToActionMassageList(obj.Status, obj.Message));
            _globalEventsService.Unsubscribe<FTPInteraktionEvent>(obj => IsAnimate = obj.IsInteractFTP);
            base.OnDisposing();
        }
        #endregion

    }
}
