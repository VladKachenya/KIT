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
using BISC.Modules.InformationModel.Presentation.ViewModels.InfoModelTree;

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
        private bool _isLoadingAllValuesInProgress;
        private bool _isLoadingDoiValuesInProgress;
        private bool _isHideButtons;

        public InfoModelTreeItemDetailsViewModel(
            IInfoModelTreeFactory infoModelTreeFactory,
            ILoggingService loggingService,
            IUserInterfaceComposingService userInterfaceComposingService,
            ICommandFactory commandFactory,
            IGlobalEventsService globalEventsService,
            IConnectionPoolService connectionPoolService,
            ModelValuesLoadingHelper modelValuesLoadingHelper)
        : base(globalEventsService)
        {
            _infoModelTreeFactory = infoModelTreeFactory;
            _loggingService = loggingService;
            _userInterfaceComposingService = userInterfaceComposingService;
            _globalEventsService = globalEventsService;
            _connectionPoolService = connectionPoolService;
            _modelValuesLoadingHelper = modelValuesLoadingHelper;
            LoadAllValuesCommand = commandFactory.CreatePresentationCommand(OnLoadAllValues, CanExecuteLoadAllValues);
            LoadDoiValuesCommand = commandFactory.CreatePresentationCommand<DoiInfoModelItemViewModel>(OnLoadDoiValues, CanExecuteLoadDoiValues);
        }

        private async void OnLoadAllValues()
        {
            try
            {
                _isLoadingAllValuesInProgress = true;
                (LoadAllValuesCommand as IPresentationCommand)?.RaiseCanExecute();
                var device = (_model as IDevice) ?? _model.GetFirstParentOfType<IDevice>();
                await _modelValuesLoadingHelper.LoadValues(AllIecTreeItems, device);
            }
            catch (Exception e)
            {
                _loggingService.LogException(e);
            }
            finally
            {
                _isLoadingAllValuesInProgress = false;
                (LoadAllValuesCommand as IPresentationCommand)?.RaiseCanExecute();
            }
        }

        private async void OnLoadDoiValues(DoiInfoModelItemViewModel viewModel)
        {
            try
            {
                _isLoadingDoiValuesInProgress = true;
                (LoadDoiValuesCommand as IPresentationCommand)?.RaiseCanExecute();
                var device = (_model as IDevice) ?? _model.GetFirstParentOfType<IDevice>();
                await _modelValuesLoadingHelper.UpdateDoiViewModelValues(device, viewModel);
            }
            catch (Exception e)
            {
                _loggingService.LogException(e);
            }
            finally
            {
                _isLoadingDoiValuesInProgress = false;
                (LoadDoiValuesCommand as IPresentationCommand)?.RaiseCanExecute();
            }
        }

        private bool CanExecuteLoadAllValues()
        {
            if (_isLoadingAllValuesInProgress)
            {
                return false;
            }
            var device = (_model as IDevice) ?? _model.GetFirstParentOfType<IDevice>();
            return _connectionPoolService.GetConnection(device.Ip).IsConnected;
        }

        private bool CanExecuteLoadDoiValues(DoiInfoModelItemViewModel vieIInfoModelItemViewModel)
        {
            if (_isLoadingDoiValuesInProgress)
            {
                return false;
            }
            var device = (_model as IDevice) ?? _model.GetFirstParentOfType<IDevice>();
            //var doi = vieIInfoModelItemViewModel?.Model as IDoi;
            return _connectionPoolService.GetConnection(device.Ip).IsConnected; //&& doi != null;
        }

        public ObservableCollection<IInfoModelItemViewModel> AllIecTreeItems
        {
            get => _allIecTreeItems;
            set { SetProperty(ref _allIecTreeItems, value); }
        }

        public IInfoModelItemViewModel SelectedTreeItem
        {
            get => _selectedTreeItem;
            set { SetProperty(ref _selectedTreeItem, value, true);}
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
                    if (value)
                    {
                        _loggingService.LogUserAction($"Включена сортировка по FC");
                    }
                    else
                    {
                        _loggingService.LogUserAction($"Отключена сортировка по FC");
                    }
                    UpdateInfoModelTree();
                }

                SetProperty(ref _isFcSortChecked, value);
            }
        }
        public ICommand LoadAllValuesCommand { get; }
        public ICommand LoadDoiValuesCommand { get; }


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
                _userInterfaceComposingService.AddGlobalCommand(LoadAllValuesCommand, $"Прочитать значения модели {name} из устройства", IconsKeys.ArrowDownBoldCircleIcon, false, true);
            }
            _globalEventsService.Subscribe<ConnectionEvent>(OnConnectionChanged);
            base.OnActivate();
        }

        private void OnConnectionChanged(ConnectionEvent connectionEvent)
        {
            var device = (_model as IDevice) ?? _model.GetFirstParentOfType<IDevice>();
            if (connectionEvent.Ip == device.Ip)
            {
                (LoadAllValuesCommand as IPresentationCommand)?.RaiseCanExecute();
            }
        }

        public override void OnDeactivate()
        {
            _userInterfaceComposingService.DeleteGlobalCommand(LoadAllValuesCommand);
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