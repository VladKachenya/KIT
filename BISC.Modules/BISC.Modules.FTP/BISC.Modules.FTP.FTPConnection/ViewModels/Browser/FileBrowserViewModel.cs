using BISC.Infrastructure.Global.Services;
using BISC.Modules.FTP.FTPConnection.Events;
using BISC.Modules.FTP.Infrastructure.Factorys;
using BISC.Modules.FTP.Infrastructure.Model;
using BISC.Modules.FTP.Infrastructure.ViewModels.Browser;
using BISC.Modules.FTP.Infrastructure.ViewModels.Browser.BrowserElements;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BISC.Modules.FTP.FTPConnection.ViewModels.Browser
{
    public class FileBrowserViewModel : ViewModelBase, IFileBrowserViewModel
    {
        private readonly IBrowserElementViewModelFactory _browserElementViewModelFactory;
        private IFileBrowser _fileBrowser;
        private ICommandFactory _commandFactory;
        private IDeviceDirectoryViewModel _rootDeviceDirectoryViewModel;
        private IDeviceDirectoryViewModel _selectedDirectoryViewModel;
        private IGlobalEventsService _globalEventsService;



        public FileBrowserViewModel(IBrowserElementViewModelFactory browserElementViewModelFactory, ICommandFactory commandFactory,
            IGlobalEventsService globalEventsService)
        {
            _browserElementViewModelFactory = browserElementViewModelFactory;
            _commandFactory = commandFactory;
            _globalEventsService = globalEventsService;
            LoadRootCommand = _commandFactory.CreatePresentationCommand(OnLoadRootExecute);
            _globalEventsService.Subscribe<FTPReloadEvent>(reload => OnLoadRootExecute());
            SelectDirectoryCommand = _commandFactory.CreatePresentationCommand<object>(OnSelectDirectoryExecute);
        }




        private void OnSelectDirectoryExecute(object obj)
        {

            if ((obj as RoutedPropertyChangedEventArgs<object>)?.NewValue is TreeViewItem)
            {
                SelectedDirectoryViewModel =
                (((obj as RoutedPropertyChangedEventArgs<object>)?.NewValue as TreeViewItem)?.DataContext as
                    IFileBrowserViewModel)?.RootDeviceDirectoryViewModel;
            }
            else
            {
                SelectedDirectoryViewModel =
                    (obj as RoutedPropertyChangedEventArgs<object>)?.NewValue as
                    IDeviceDirectoryViewModel;
            }
        }

        private async void OnLoadRootExecute()
        {
            if (_fileBrowser == null) return;
            await _fileBrowser.LoadRootDirectory();
            _rootDeviceDirectoryViewModel =
                _browserElementViewModelFactory.CreateBrowserElementViewModelBase(_fileBrowser.RootDeviceDirectory, null) as
                    IDeviceDirectoryViewModel;
            OnPropertyChanged(nameof(RootDeviceDirectoryViewModel));
        }



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

        public ICommand SelectDirectoryCommand { get; }

        public ICommand LoadRootCommand { get; }


        #endregion

    }
}
