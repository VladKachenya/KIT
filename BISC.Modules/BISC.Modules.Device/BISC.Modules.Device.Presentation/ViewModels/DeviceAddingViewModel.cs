﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Interfaces;
using BISC.Modules.Device.Presentation.Interfaces.Factories;
using BISC.Modules.Device.Presentation.Interfaces.Services;
using BISC.Presentation.BaseItems.Commands;
using BISC.Presentation.BaseItems.Common;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Device.Presentation.ViewModels
{
    public class DeviceAddingViewModel : NavigationViewModelBase, IDeviceAddingViewModel
    {
        private readonly ICommandFactory _commandFactory;
        private readonly INavigationService _navigationService;

        private bool _isOpeningFromFile;

        public DeviceAddingViewModel(ICommandFactory commandFactory,INavigationService navigationService)
            : base(null)
        {
            _commandFactory = commandFactory;
            _navigationService = navigationService;

            SelectConnectCommand = _commandFactory.CreatePresentationCommand(OnSelectConnectExecute);
            SelectOpenFromFileCommand = _commandFactory.CreatePresentationCommand(OnSelectOpenFromFileExecute);

            CloseCommand = commandFactory.CreatePresentationCommand((() =>
            {
                DialogCommands.CloseDialogCommand.Execute(null, null);
                Dispose();
            }));

            OnSelectConnectExecute();
            //OnSelectOpenFromFileExecute();

        }

        private void OnSelectOpenFromFileExecute()
        {
            IsOpeningFromFile = true;
            _navigationService.NavigateViewToRegion(DeviceKeys.DeviceFromFileAddingViewKey,DeviceKeys.DeviceAddingRegionKey);
        }

        private void OnSelectConnectExecute()
        {
            IsOpeningFromFile = false;
            _navigationService.NavigateViewToRegion(DeviceKeys.DeviceConnectingViewKey, DeviceKeys.DeviceAddingRegionKey);

        }

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            OnSelectConnectExecute();
            base.OnNavigatedTo(navigationContext);
        }

        public bool IsOpeningFromFile
        {
            get => _isOpeningFromFile;
            set => SetProperty(ref _isOpeningFromFile,value);
        }

        public ICommand SelectOpenFromFileCommand { get; }
        public ICommand SelectConnectCommand { get; }
        public ICommand CloseCommand { get; }

        #region Overrides of ViewModelBase

        protected override void OnDisposing()
        {
            base.OnDisposing();
        }

        #endregion
    }
}
