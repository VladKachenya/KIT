using BISC.Infrastructure.Global.Services;
using BISC.Modules.FTP.FTPConnection.Events;
using BISC.Modules.FTP.Infrastructure.Factorys;
using BISC.Modules.FTP.Infrastructure.Model;
using BISC.Modules.FTP.Infrastructure.ViewModels.Browser;
using BISC.Modules.FTP.Infrastructure.ViewModels.Browser.BrowserElements;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
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
            var openDilog = new OpenFileDialog(){ Multiselect = true, Title = "Выберите файлы" };
            openDilog.Filter = "All files(*.*)|*.*";
            if (openDilog.ShowDialog() == DialogResult.Cancel)
                return;
            List<string> data = new List<string>();
            var fileNames = openDilog.SafeFileNames;
            for (int i = 0; i < openDilog.FileNames.Length; i++)// (string fileName in openDilog.FileNames)
            {
                if (!IsDigitsOnly(fileNames[i]))
                {
                    _globalEventsService.SendMessage(message: new FTPActionMassageEvent { Status = false, Message = "Имя файла не должно " +
                        "содержать цифры, переименуёте фаил "  + fileNames[i] });
                    fileNames[i]=null;
                    continue;
                }
                FileStream file = new FileStream(openDilog.FileNames[i], FileMode.Open);
                StreamReader readFile = new StreamReader(file);
                data.Add(readFile.ReadToEnd());
                readFile.Close();
                file.Close();
            }
            var fileNamesLict = new List<string>();
            foreach (var el in fileNames.ToList<string>())
                if (el != null) fileNamesLict.Add(el);
            
            _globalEventsService.SendMessage(message: new FTPActionMassageEvent { Status = null, Message = "Начало процесса записи" });
            await _ftpClientWrapper.UploadFileString(data, fileNamesLict);
            _globalEventsService.SendMessage(message: new FTPActionMassageEvent { Status = true, Message = "Процесс записи окончен" });

            OnLoadRootExecuteAsync();
        }

        private async void OnLoadRootExecuteAsync()
        {
            _globalEventsService.SendMessage(new FTPActionMassageEvent { Status = null, Message = "Перечитывание файловой системы" });
            if (_fileBrowser == null) return;
            await _fileBrowser.LoadRootDirectory();
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
