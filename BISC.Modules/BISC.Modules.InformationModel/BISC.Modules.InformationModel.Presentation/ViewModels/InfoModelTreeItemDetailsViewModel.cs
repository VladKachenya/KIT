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
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Modules.InformationModel.Presentation.ViewModels.InfoModelTree;
using BISC.Presentation.Infrastructure.Keys;

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
        private readonly ModelValueWritingHelper _modelValueWritingHelper;
        private bool _isDeviceConnected;
        private bool _isLocalValuesShowing;
        private ObservableCollection<IInfoModelItemViewModel> _allIecTreeItems;
        private IInfoModelItemViewModel _selectedTreeItem;
        private bool _isFcSortChecked;
        private IModelElement _model;
        private BiscNavigationParameters _biscNavigationParameters;
        private bool _isLoadingAllValuesInProgress;
        private bool _isLoadingDoiValuesInProgress;
        private bool _isWritingValuesInProgress;
        private bool _isHideButtons;
        private bool _isFromDeviceDetails;

        private List<DaiInfoModelItemViewModel> _daiViewModelsToSave = new List<DaiInfoModelItemViewModel>();
        private IDevice _device;
        private bool _isDbFilterEnable;


        public InfoModelTreeItemDetailsViewModel(
            IInfoModelTreeFactory infoModelTreeFactory,
            ILoggingService loggingService,
            IUserInterfaceComposingService userInterfaceComposingService,
            ICommandFactory commandFactory,
            IGlobalEventsService globalEventsService,
            IConnectionPoolService connectionPoolService,
            ModelValuesLoadingHelper modelValuesLoadingHelper,
            ModelValueWritingHelper modelValueWritingHelper)
        : base(globalEventsService)
        {
            _infoModelTreeFactory = infoModelTreeFactory;
            _loggingService = loggingService;
            _userInterfaceComposingService = userInterfaceComposingService;
            _globalEventsService = globalEventsService;
            _connectionPoolService = connectionPoolService;
            _modelValuesLoadingHelper = modelValuesLoadingHelper;
            _modelValueWritingHelper = modelValueWritingHelper;
            LoadAllValuesCommand = commandFactory.CreatePresentationCommand(OnLoadAllValues, CanExecuteLoadAllValues);
            LoadDoiValuesCommand = commandFactory.CreatePresentationCommand<DoiInfoModelItemViewModel>(OnLoadDoiValues, CanExecuteLoadDoiValues);
            WriteDbValueOfDaiCommand = commandFactory.CreatePresentationCommand<DaiInfoModelItemViewModel>(OnWriteDbValueOfDai);
            AddItemToSaveCommand = commandFactory.CreatePresentationCommand<DaiInfoModelItemViewModel>(OnAddItemToSave);
            WriteAllDbValuesCommand = commandFactory.CreatePresentationCommand(OnWriteAllDbValues, CanExecuteWriteAllDbValues);
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

        public bool IsSortingVisible
        {
            get => !_isFromDeviceDetails;
            set => SetProperty(ref _isFromDeviceDetails, !value, true);
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

        public bool IsDbFilterEnable
        {
            get => _isDbFilterEnable;
            set
            {
                if (_isDbFilterEnable != value)
                {
                    SetProperty(ref _isDbFilterEnable, value);
                    if (value)
                    {
                        _loggingService.LogUserAction($"Включена фильтрация по DB");
                    }
                    else
                    {
                        _loggingService.LogUserAction($"Отключена фильтрация по DB");
                    }
                    UpdateInfoModelTree(null);
                }

                SetProperty(ref _isDbFilterEnable, value);
            }
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
                    UpdateInfoModelTree(AllIecTreeItems);
                }

                SetProperty(ref _isFcSortChecked, value);
            }
        }
        public ICommand LoadAllValuesCommand { get; }
        public ICommand LoadDoiValuesCommand { get; }

        public ICommand WriteDbValueOfDaiCommand { get; set; }
        public ICommand AddItemToSaveCommand { get; set; }
        public ICommand WriteAllDbValuesCommand { get; set; }

        #region command implementation

        private async void OnWriteAllDbValues()
        {
            try
            {
                _isWritingValuesInProgress = true;
                foreach (var daiViewModel in _daiViewModelsToSave)
                {
                    await WriteDbValueOfDaiAsync(daiViewModel);
                }
            }
            finally
            {
                _daiViewModelsToSave.Clear();
                _isWritingValuesInProgress = false;
                (WriteAllDbValuesCommand as IPresentationCommand)?.RaiseCanExecute();
            }
        }

        private void OnAddItemToSave(DaiInfoModelItemViewModel daiInfoModelItemViewModel)
        {
            if(daiInfoModelItemViewModel == null) return;
            if (daiInfoModelItemViewModel.IsValueChanged)
            {
                if (!_daiViewModelsToSave.Contains(daiInfoModelItemViewModel))
                {
                    _daiViewModelsToSave.Add(daiInfoModelItemViewModel);
                }
            }
            else
            {
                if (_daiViewModelsToSave.Contains(daiInfoModelItemViewModel))
                {
                    _daiViewModelsToSave.Remove(daiInfoModelItemViewModel);
                }
            }
            (WriteAllDbValuesCommand as IPresentationCommand)?.RaiseCanExecute();
        }

        private async void OnWriteDbValueOfDai(DaiInfoModelItemViewModel daiInfoModelItemViewModel)
        {
            await WriteDbValueOfDaiAsync(daiInfoModelItemViewModel);
        }

        private async Task WriteDbValueOfDaiAsync(DaiInfoModelItemViewModel daiInfoModelItemViewModel)
        {
            try
            {
                await _modelValueWritingHelper.WriteValue(daiInfoModelItemViewModel, _device);
            }
            catch (Exception e)
            {
                _loggingService.LogException(e);
            }
        }

        private async void OnLoadAllValues()
        {
            try
            {
                _isLoadingAllValuesInProgress = true;
                (LoadAllValuesCommand as IPresentationCommand)?.RaiseCanExecute();

                await _modelValuesLoadingHelper.LoadValues(AllIecTreeItems, _device);
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

                await _modelValuesLoadingHelper.UpdateDoiViewModelValues(_device, viewModel);
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

        private bool CanExecuteWriteAllDbValues()
        {
            return _connectionPoolService.GetConnection(_device.Ip).IsConnected && _daiViewModelsToSave.Count != 0 && !_isWritingValuesInProgress;
        }

        private bool CanExecuteLoadAllValues()
        {
            if (_isLoadingAllValuesInProgress)
            {
                return false;
            }
            return _connectionPoolService.GetConnection(_device.Ip).IsConnected;
        }

        private bool CanExecuteLoadDoiValues(DoiInfoModelItemViewModel vieIInfoModelItemViewModel)
        {
            if (_isLoadingDoiValuesInProgress)
            {
                return false;
            }
            var doi = vieIInfoModelItemViewModel?.Model as IDoi;
            return _connectionPoolService.GetConnection(_device.Ip).IsConnected && doi != null;
        }

        #endregion


        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _biscNavigationParameters = navigationContext.BiscNavigationParameters;
            UpdateInfoModelTree(AllIecTreeItems);
            base.OnNavigatedTo(navigationContext);
        }

        private void UpdateInfoModelTree(ObservableCollection<IInfoModelItemViewModel> defaultCollection)
        {
            var ldevice = _biscNavigationParameters.GetParameterByName<ILDevice>(
                InfoModelKeys.ModelKeys.LDeviceKey);
            if (ldevice != null)
            {
                _model = ldevice;
                _device = _model.GetFirstParentOfType<IDevice>();
                AllIecTreeItems =
                    _infoModelTreeFactory.CreateLDeviceInfoModelTree(ldevice, IsFcSortChecked, IsDbFilterEnable, defaultCollection);
            }
            else if (_biscNavigationParameters.Any((parameter => parameter.ParameterName == "IED")))
            {
                _model = _biscNavigationParameters.GetParameterByName<IModelElement>("IED");
                _device = _model as IDevice;
                _isHideButtons = _biscNavigationParameters.GetParameterByName<bool>("IsHideButtons");
                _isFromDeviceDetails = _biscNavigationParameters.GetParameterByName<bool>(KeysForNavigation.NavigationParameter.IsFromDeviceDetails);
                SetIsReadOnly(_biscNavigationParameters.GetParameterByName<bool>(KeysForNavigation.NavigationParameter.IsReadOnly));
                _isHideButtons = _isHideButtons || IsReadOnly;
                if (_isFromDeviceDetails)
                {
                    _isDbFilterEnable = true;
                    _isFcSortChecked = false;
                }
                List<ILDevice> devices = new List<ILDevice>();
                _model.GetAllChildrenOfType(ref devices);
                AllIecTreeItems =
                    _infoModelTreeFactory.CreateFullInfoModelTree(devices, IsFcSortChecked, IsDbFilterEnable, defaultCollection);
            }
        }

        #region Overrides of NavigationViewModelBase

        public override void OnActivate()
        {
            var name = (_model as IDevice)?.Name ?? (_model as ILDevice)?.Inst;
            if (!_isHideButtons || !IsReadOnly)
            {
                _userInterfaceComposingService.AddGlobalCommand(LoadAllValuesCommand, $"Прочитать значения модели {name} из устройства", IconsKeys.ArrowDownBoldCircleIcon, false, true);
                _userInterfaceComposingService.AddGlobalCommand(WriteAllDbValuesCommand, $"Записать значения модели {name} в устройства", IconsKeys.UploadNetworkKey, false, true);
            }
            _globalEventsService.Subscribe<ConnectionEvent>(OnConnectionChanged);
            base.OnActivate();
        }

        private void OnConnectionChanged(ConnectionEvent connectionEvent)
        {
            if (connectionEvent.Ip == _device.Ip)
            {
                (LoadAllValuesCommand as IPresentationCommand)?.RaiseCanExecute();
                (WriteAllDbValuesCommand as IPresentationCommand)?.RaiseCanExecute();
            }
        }
        
        public override void OnDeactivate()
        {
            _userInterfaceComposingService.DeleteGlobalCommand(LoadAllValuesCommand);
            _userInterfaceComposingService.DeleteGlobalCommand(WriteAllDbValuesCommand);
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