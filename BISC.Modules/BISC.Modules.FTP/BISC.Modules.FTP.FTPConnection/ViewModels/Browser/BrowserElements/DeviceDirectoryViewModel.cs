﻿using BISC.Infrastructure.Global.Services;
using BISC.Modules.FTP.FTPConnection.Events;
using BISC.Modules.FTP.Infrastructure.Factorys;
using BISC.Modules.FTP.Infrastructure.Model.BrowserElements;
using BISC.Modules.FTP.Infrastructure.ViewModels.Browser.BrowserElements;
using BISC.Presentation.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.FTP.FTPConnection.ViewModels.Browser.BrowserElements
{
    public class DeviceDirectoryViewModel : BrowserElementViewModel, IDeviceDirectoryViewModel
    {
        private readonly IBrowserElementViewModelFactory _browserElementViewModelFactory;
        private IDeviceDirectory _model;
        private string _elementPath;
        private ObservableCollection<IBrowserElementViewModel> _childBrowserElementViewModels;
        private string _name;


        public DeviceDirectoryViewModel(IBrowserElementViewModelFactory browserElementViewModelFactory, ICommandFactory commandFactory,
            IGlobalEventsService globalEventsService)
            : base (commandFactory, globalEventsService)
        {
            _browserElementViewModelFactory = browserElementViewModelFactory;
            _childBrowserElementViewModels = new ObservableCollection<IBrowserElementViewModel>();
            CreateChildDirectoryCommand = _commandFactory.CreatePresentationCommand(OnCreateChildDirectoryExecute);
            UploadFileInDirectoryCommand = _commandFactory.CreatePresentationCommand(OnUploadFileInDirectoryExecute);
        }

        private void OnUploadFileInDirectoryExecute()
        {
            _globalEventsService.SendMessage(new FTPUploadEvetn() { PathInDevise = _elementPath });
        }

        private void OnCreateChildDirectoryExecute()
        {
            throw new NotImplementedException();
        }

        #region Implementation of Model

        public override object Model
        {
            get
            {
                return _model;

            }
            set
            {

                _model = value as IDeviceDirectory;
                _elementPath = _model.ElementPath;
                _name = _model.Name;
                OnPropertyChanged(nameof(ElementPath));
                OnPropertyChanged(nameof(Name));

                _childBrowserElementViewModels.Clear();
                _model.BrowserElementsInDirectory?.ForEach((element =>
                    ChildBrowserElementViewModels.Add(
                        _browserElementViewModelFactory.CreateBrowserElementViewModelBase(element, this))));

            }
        }

        #endregion

        #region Implementation of IBrowserElementViewModel

        public override string ElementPath
        {
            get { return _elementPath; }
        }

        public override string Name
        {
            get { return _name; }
        }

        #endregion

        #region Implementation of IDeviceDirectoryViewModel

        public ObservableCollection<IBrowserElementViewModel> ChildBrowserElementViewModels
        {
            get { return _childBrowserElementViewModels; }
        }

        public ICommand LoadDirectoryCommand { get; }

        public ICommand CreateChildDirectoryCommand { get; }

        public ICommand UploadFileInDirectoryCommand { get; }

        #endregion
    }
}
