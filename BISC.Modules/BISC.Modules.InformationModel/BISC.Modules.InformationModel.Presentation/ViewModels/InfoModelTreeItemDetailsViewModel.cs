using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Connection.Infrastructure.Events;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Presentation.Helpers;
using BISC.Modules.InformationModel.Presentation.Interfaces;
using BISC.Modules.InformationModel.Presentation.Interfaces.Factories;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace BISC.Modules.InformationModel.Presentation.ViewModels
{
    public class InfoModelTreeItemDetailsViewModel : NavigationViewModelBase
    {
        private readonly IInfoModelTreeFactory _infoModelTreeFactory;
        private readonly ILoggingService _loggingService;
        private readonly IUserInterfaceComposingService _userInterfaceComposingService;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly ModelValuesLoadingHelper _modelValuesLoadingHelper;
        private bool _isDeviceConnected;
        private bool _isLocalValuesShowing;
        private ObservableCollection<IInfoModelItemViewModel> _allIecTreeItems;
        private IInfoModelItemViewModel _selectedTreeItem;
        private bool _isFcSortChecked;
        private IModelElement _model;
        private BiscNavigationParameters _biscNavigationParameters;
        private bool _isLoadingValuesInProgress;
        private bool _isHideButtons;

        public InfoModelTreeItemDetailsViewModel(IInfoModelTreeFactory infoModelTreeFactory, ILoggingService loggingService, IUserInterfaceComposingService userInterfaceComposingService,
            ICommandFactory commandFactory, IGlobalEventsService globalEventsService, IConnectionPoolService connectionPoolService, ModelValuesLoadingHelper modelValuesLoadingHelper)
        {
            _infoModelTreeFactory = infoModelTreeFactory;
            _loggingService = loggingService;
            _userInterfaceComposingService = userInterfaceComposingService;
            _globalEventsService = globalEventsService;
            _connectionPoolService = connectionPoolService;
            _modelValuesLoadingHelper = modelValuesLoadingHelper;
            LoadValuesCommand = commandFactory.CreatePresentationCommand(OnLoadValues, CanExecuteLoadValues);

        }

        private async void OnLoadValues()
        {
            try
            {
                _isLoadingValuesInProgress = true;
                (LoadValuesCommand as IPresentationCommand)?.RaiseCanExecute();
                var device = (_model as IDevice) ?? _model.GetFirstParentOfType<IDevice>();
                await _modelValuesLoadingHelper.LoadValues(AllIecTreeItems, device);
            }
            catch (Exception e)
            {
                _loggingService.LogException(e);
            }
            finally
            {
                _isLoadingValuesInProgress = false;
                (LoadValuesCommand as IPresentationCommand)?.RaiseCanExecute();
            }
        }

        private bool CanExecuteLoadValues()
        {
            if (_isLoadingValuesInProgress)
            {
                return false;
            }
            var device = (_model as IDevice) ?? _model.GetFirstParentOfType<IDevice>();
            return _connectionPoolService.GetConnection(device.Ip).IsConnected;
        }

        public ObservableCollection<IInfoModelItemViewModel> AllIecTreeItems
        {
            get => _allIecTreeItems;
            set { SetProperty(ref _allIecTreeItems, value); }
        }

        public IInfoModelItemViewModel SelectedTreeItem
        {
            get => _selectedTreeItem;
            set { SetProperty(ref _selectedTreeItem, value, true); }
        }

        private bool IsLocalValuesShowing
        {
            get => _isLocalValuesShowing;
            set { SetProperty(ref _isLocalValuesShowing, value); }
        }

        private bool IsDeviceConnected
        {
            get => _isDeviceConnected;
            set { SetProperty(ref _isDeviceConnected, value); }
        }

        public bool IsFcSortChecked
        {
            get => _isFcSortChecked;
            set
            {
                if (_isFcSortChecked != value)
                {
                    SetProperty(ref _isFcSortChecked, value);
                    _loggingService.LogUserAction($"Пользователь выставил сортировку по FC в состояние {value}");
                    UpdateInfoModelTree();
                }

                SetProperty(ref _isFcSortChecked, value);
            }
        }
        public ICommand LoadValuesCommand { get; }

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _biscNavigationParameters = navigationContext.BiscNavigationParameters;
            UpdateInfoModelTree();
            base.OnNavigatedTo(navigationContext);
        }

        private void UpdateInfoModelTree()
        {
            var ldevice = _biscNavigationParameters.GetParameterByName<ILDevice>(
                InfoModelKeys.ModelKeys.LDeviceKey);
            if (ldevice != null)
            {
                _model = ldevice;
                AllIecTreeItems =
                    _infoModelTreeFactory.CreateLDeviceInfoModelTree(ldevice, IsFcSortChecked, AllIecTreeItems);
            }
            else if (_biscNavigationParameters.Any((parameter => parameter.ParameterName == "IED")))
            {
                _model = _biscNavigationParameters.GetParameterByName<IModelElement>("IED");
                _isHideButtons = _biscNavigationParameters.GetParameterByName<bool>("IsHideButtons");
                List<ILDevice> devices = new List<ILDevice>();
                _model.GetAllChildrenOfType(ref devices);
                AllIecTreeItems =
                    _infoModelTreeFactory.CreateFullInfoModelTree(devices, IsFcSortChecked, AllIecTreeItems);
            }
        }

        #region Overrides of NavigationViewModelBase

        public override void OnActivate()
        {
            var name = (_model as IDevice)?.Name ?? (_model as ILDevice)?.Inst;
            if (!_isHideButtons)
            {
                _userInterfaceComposingService.AddGlobalCommand(LoadValuesCommand, $"Обновить значения модели {name}", IconsKeys.UpdateIconKey, false, true);
            }
            _globalEventsService.Subscribe<ConnectionEvent>(OnConnectionChanged);
            base.OnActivate();
        }

        private void OnConnectionChanged(ConnectionEvent connectionEvent)
        {
            var device = (_model as IDevice) ?? _model.GetFirstParentOfType<IDevice>();
            if (connectionEvent.Ip == device.Ip)
            {
                (LoadValuesCommand as IPresentationCommand)?.RaiseCanExecute();
            }
        }

        public override void OnDeactivate()
        {
            _userInterfaceComposingService.DeleteGlobalCommand(LoadValuesCommand);
            _globalEventsService.Unsubscribe<ConnectionEvent>(OnConnectionChanged);
            base.OnDeactivate();
        }

        #endregion

        protected override void OnNavigatedFrom(BiscNavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);
        }
    }
}