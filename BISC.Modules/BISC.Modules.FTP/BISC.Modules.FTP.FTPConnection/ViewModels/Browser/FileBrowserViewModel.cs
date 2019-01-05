using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.FTP.FTPConnection.Events;
using BISC.Modules.FTP.Infrastructure.Factorys;
using BISC.Modules.FTP.Infrastructure.Model;
using BISC.Modules.FTP.Infrastructure.ViewModels.Browser;
using BISC.Modules.FTP.Infrastructure.ViewModels.Browser.BrowserElements;
using BISC.Presentation.BaseItems.Common;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace BISC.Modules.FTP.FTPConnection.ViewModels.Browser
{
    public class FileBrowserViewModel : ComplexViewModelBase, IFileBrowserViewModel
    {
        #region private filds
        private readonly IBrowserElementViewModelFactory _browserElementViewModelFactory;
        private IFileBrowser _fileBrowser;
        private IFTPClientWrapper _ftpClientWrapper;
        private ICommandFactory _commandFactory;
        private IDeviceDirectoryViewModel _rootDeviceDirectoryViewModel;
        private IDeviceDirectoryViewModel _selectedDirectoryViewModel;
        private IGlobalEventsService _globalEventsService;
        #endregion

        #region C-tor
        public FileBrowserViewModel(IBrowserElementViewModelFactory browserElementViewModelFactory, ICommandFactory commandFactory,
            IGlobalEventsService globalEventsService, IFTPClientWrapper ftpClientWrapper)
        {
            _ftpClientWrapper = ftpClientWrapper;
            _browserElementViewModelFactory = browserElementViewModelFactory;
            _commandFactory = commandFactory;
            _globalEventsService = globalEventsService;

            LoadFileToDeviceCommand = _commandFactory.CreatePresentationCommand(OnLoadFileToDevice, () => _ftpClientWrapper.IsConnected); 
            LoadRootCommand = _commandFactory.CreatePresentationCommand(OnLoadRootExecuteAsync);
            _globalEventsService.Subscribe<FTPReloadEvent>(reload => OnLoadRootExecuteAsync());
            _globalEventsService.Subscribe<FTPChangingConnectionEvent>(obj => VerifyConnection());
        }


        #endregion

        #region private methods
        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c >= '0' && c <= '9')
                    return false;
            }

            return true;
        }
        private void VerifyConnection()
        {
            (LoadFileToDeviceCommand as IPresentationCommand).RaiseCanExecute();
        }
        private async void OnLoadFileToDevice()
        {
            List<FileInfo> selectidFilesInfo = FileHelper.SelectFileToOpen("Выберите файлы", "All files(*.*)|*.*", true).GetListOfValue();
            if (!selectidFilesInfo.Any()) return;

            _globalEventsService.SendMessage( new FTPInteraktionEvent(true));
            List<FileInfo> editedSelectidFileInfo = new List<FileInfo>(selectidFilesInfo);
            try
            {
                _globalEventsService.SendMessage(new FTPActionMassageEvent {Status = null, Message = "Проверка имён выбранных файлов" });
                foreach (var element in selectidFilesInfo)
                {
                    if (!IsDigitsOnly(element.Name))
                    {
                        _globalEventsService.SendMessage(
                            new FTPActionMassageEvent {
                            Status = false,
                            Message = "Имя файла не должно " +
                                      "содержать цифры, переименуёте фаил " + element.Name
                            });
                        editedSelectidFileInfo.Remove(element);
                    }
                }
                _globalEventsService.SendMessage(new FTPActionMassageEvent { Status = null, Message = "Вычитывание выбранных файлов" });
                List<string> data = new List<string>();
                foreach (var element in editedSelectidFileInfo)
                {
                    FileStream file = new FileStream(element.FullName, FileMode.Open);
                    StreamReader readFile = new StreamReader(file);
                    data.Add(readFile.ReadToEnd());
                    readFile.Close();
                    file.Close();
                }
                var fileNamesList = new List<string>( from element in editedSelectidFileInfo select element.Name);
                _globalEventsService.SendMessage(message: new FTPActionMassageEvent { Status = null, Message = "Начало процесса записи" });
                await _ftpClientWrapper.UploadFileString(data, fileNamesList);
                _globalEventsService.SendMessage(message: new FTPActionMassageEvent { Status = true, Message = "Процесс записи окончен" });
                OnLoadRootExecuteAsync();
            }
            finally
            {
                _globalEventsService.SendMessage(new FTPInteraktionEvent(false));
            }
        }

        private async void OnLoadRootExecuteAsync()
        {
            _globalEventsService.SendMessage(new FTPActionMassageEvent { Status = null, Message = "Чтение файловой системы" });
            if (_fileBrowser == null) return;
            try
            {
                await _fileBrowser.LoadRootDirectory();
            }
            catch (Exception e)
            {
                _globalEventsService.SendMessage(new FTPActionMassageEvent { Status = false, Message = "Ошибка чтение файловой системы" });
                _rootDeviceDirectoryViewModel = null;
                OnPropertyChanged(nameof(RootDeviceDirectoryViewModel));
                return;
            }
            _rootDeviceDirectoryViewModel =
                _browserElementViewModelFactory.CreateBrowserElementViewModelBase(_fileBrowser.RootDeviceDirectory, null) as
                    IDeviceDirectoryViewModel;
            OnPropertyChanged(nameof(RootDeviceDirectoryViewModel));
        }

        #endregion

        #region Implementation of IFileBrowserViewModel

        public object Model
        {
            get
            {

                return _fileBrowser;
            }
            set
            {
                _fileBrowser = value as IFileBrowser;
            }
        }

        public IDeviceDirectoryViewModel RootDeviceDirectoryViewModel
        {
            get { return _rootDeviceDirectoryViewModel; }
        }

        public IDeviceDirectoryViewModel SelectedDirectoryViewModel
        {
            get { return _selectedDirectoryViewModel; }
            set
            {
                _selectedDirectoryViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadRootCommand { get; }
        public ICommand LoadFileToDeviceCommand { get; }

        #endregion

        #region Overrides of ViewModelBase

        protected override void OnDisposing()
        {
            _globalEventsService.Unsubscribe<FTPChangingConnectionEvent>(obj => VerifyConnection());
            _globalEventsService.Unsubscribe<FTPReloadEvent>(reload => OnLoadRootExecuteAsync());
            base.OnDisposing();
        }

        #endregion

    }
}
