using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.Keys;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.DataSets.Infrastructure.ViewModels.Factorys;
using BISC.Modules.DataSets.Model.Services;
using BISC.Modules.DataSets.Presentation.Commands;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.BaseItems.ViewModels.Behaviors;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Presentation.Infrastructure.Keys;

namespace BISC.Modules.DataSets.Presentation.ViewModels
{
    public class DataSetsDetailsViewModel : NavigationViewModelBase
    {
        private IDevice _device;
        private List<IDataSet> _dataSets;
        private readonly IDataSetModelService _dataSetModelService;
        private readonly IDatasetViewModelFactory _datasetViewModelFactory;
        private readonly DataSetsProjectSavingCommand _dataSetsProjectSavingCommand;
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly IUserInterfaceComposingService _userInterfaceComposingService;
        private readonly INavigationService _navigationService;
        private readonly ILoggingService _loggingService;
        private readonly IDeviceWarningsService _deviceWarningsService;
        private readonly IGlobalSavingService _globalSavingService;
        private ObservableCollection<IDataSetViewModel> _dataSets1;
        private string _regionName;
        private bool _isModelShowed;
        private bool _issUpdateDataSets = true;
        private bool _isSaveСhanges = true;
        private IDataSetViewModel _selectedDataSet;

        #region C-tor

        public DataSetsDetailsViewModel(
            ICommandFactory commandFactory,
            IDataSetModelService dataSetModelService,
            IDatasetViewModelFactory datasetViewModelFactory,
            ISaveCheckingService saveCheckingService, 
            IUserInterfaceComposingService userInterfaceComposingService,
            IGlobalEventsService globalEventsService,
            INavigationService navigationService,
            ILoggingService loggingService,
            DataSetsProjectSavingCommand dataSetsProjectSavingCommandCommand, 
            IDeviceWarningsService deviceWarningsService,
            IGlobalSavingService globalSavingService)
            : base(globalEventsService)
        {
            _userInterfaceComposingService = userInterfaceComposingService;
            _navigationService = navigationService;
            _loggingService = loggingService;
            _dataSetsProjectSavingCommand = dataSetsProjectSavingCommandCommand;
            _dataSetModelService = dataSetModelService;
            _datasetViewModelFactory = datasetViewModelFactory;
            _saveCheckingService = saveCheckingService;
            _deviceWarningsService = deviceWarningsService;
            _globalSavingService = globalSavingService;

            SaveChangesCommand = commandFactory.CreatePresentationCommand(OnSaveСhangesCommand, () => _isSaveСhanges);
            ExpandModelCommand = commandFactory.CreatePresentationCommand(OnExpandModel);
            CollapseModelCommand = commandFactory.CreatePresentationCommand(OnCollapseModel);
            AddNewDataSetCommand = commandFactory.CreatePresentationCommand(OnAddNewDataSet, IsAddNewDataSet);
            DeleteDataSetViewModelCommand = commandFactory.CreatePresentationCommand<object>(OnDeleteDataSetViewModel);
            ModelRegionKey = Guid.NewGuid().ToString();
            UpdateDataSetsCommand =
                commandFactory.CreatePresentationCommand(OnUpdateDataSets, () => _issUpdateDataSets);
        }

        private async void OnUpdateDataSets()
        {
            _issUpdateDataSets = false;
            (UpdateDataSetsCommand as IPresentationCommand)?.RaiseCanExecute();
            await UpdateDataSets();
            _issUpdateDataSets = true;
            (UpdateDataSetsCommand as IPresentationCommand)?.RaiseCanExecute();
        }


        private async Task UpdateDataSets()
        {
            BlockViewModelBehavior.SetBlock("Обновление данных", true);
            try
            {
                UpdateViewModel();
                var firstDs = DataSets.FirstOrDefault();
                if (firstDs != null)
                {
                    SelectedDataSet = firstDs;
                }
            }
            finally
            {
                BlockViewModelBehavior.Unlock();
            }
        }

        private void UpdateViewModel()
        {
            _dataSets = _dataSetModelService.GetAllDataSetOfDevice(_device);
            SortDataSetsByIsDynamic();
            try
            {
                DataSets = _datasetViewModelFactory.GetDataSetsViewModel(_dataSets);
            }
            catch (Exception e)
            {
                if (DataSets == null && _dataSets != null)
                {
                    _loggingService.LogMessage(
                        $"Невозможно сформировать DataSet на основании модели устройства {_device.Name}.",
                        SeverityEnum.Critical);
                    _deviceWarningsService.SetWarningOfDevice(_device.DeviceGuid,
                        DatasetKeys.DataSetWarningKeys.DataSetLoadErrorWarningTagKey,
                        "Невозможно сформировать DataSets");
                }
                else
                {
                    _loggingService.LogMessage(e.Message, SeverityEnum.Critical);
                }

                DataSets = new ObservableCollection<IDataSetViewModel>();
                _dataSets = new List<IDataSet>();
            }

            _saveCheckingService.RemoveSaveCheckingEntityByOwner(_regionName);
            _saveCheckingService.AddSaveCheckingEntity(new SaveCheckingEntity(ChangeTracker,
                $"DataSets устройства {_device.Name}", _dataSetsProjectSavingCommand, _device.DeviceGuid, _regionName));
            AddNewDataSetCommand.RaiseCanExecute();
            ChangeTracker.AcceptChanges();
            ChangeTracker.SetTrackingEnabled(true);
        }

        private void OnCollapseModel()
        {
            IsModelShowed = false;
        }

        private void OnExpandModel()
        {
            IsModelShowed = true;
        }

        #endregion

        #region privat methods

        private void OnDeleteDataSetViewModel(object dataSetViewModel)
        {
            var element = dataSetViewModel as IDataSetViewModel;
            DataSets.Remove(element);
            _loggingService.LogUserAction(
                $"DataSet {element.SelectedParentLd + "." + element.SelectedParentLn + "." + element.EditableNamePart} удалён");
            AddNewDataSetCommand.RaiseCanExecute();
        }

        private void OnAddNewDataSet()
        {
            var newDs = _datasetViewModelFactory.CreateDataSetViewModel(
                DataSets.Select((model => model.EditableNamePart)).ToList(), _device);
            DataSets.Add(newDs);
            SelectedDataSet = newDs;
            _loggingService.LogUserAction($"Для устройства {_device.Name} добавлен новый DataSet");
            AddNewDataSetCommand.RaiseCanExecute();
        }

        private bool IsAddNewDataSet()
        {
            if (DataSets.Count() >= 30)
            {
                return false;
            }

            return true;
        }

        private void ResetAllDataSetCollections()
        {
            _dataSets.Clear();
            DataSets.Clear();
        }

        private async void OnSaveСhangesCommand()
        {
            _isSaveСhanges = false;
            (SaveChangesCommand as IPresentationCommand)?.RaiseCanExecute();
            try
            {
                _loggingService.LogUserAction($"Изменения DataSets устройства {_device.Name}");
                BlockViewModelBehavior.SetBlock("Сохранение DataSet-ов", true);
                var savingRes = await _globalSavingService.SaveСhangesToRegion(_regionName);
                if (savingRes.IsSaved)
                {
                    await UpdateDataSets();
                }
            }
            catch (Exception e)
            {
                _loggingService.LogMessage($"Ошибка записи DataSet в устройство {e.Message}", SeverityEnum.Critical);
            }
            finally
            {
                BlockViewModelBehavior.Unlock();
                _isSaveСhanges = true;
                (SaveChangesCommand as IPresentationCommand)?.RaiseCanExecute();
            }
        }

        private void SortDataSetsByIsDynamic()
        {
            List<IDataSet> isDynamicDataSets = new List<IDataSet>();
            List<IDataSet> notIsDynamicDataSets = new List<IDataSet>();
            foreach (var element in _dataSets)
            {
                if (element.IsDynamic)
                {
                    isDynamicDataSets.Add(element);
                }
                else
                {
                    notIsDynamicDataSets.Add(element);
                }
            }

            _dataSets.Clear();
            _dataSets.AddRange(notIsDynamicDataSets);
            _dataSets.AddRange(isDynamicDataSets);
        }

        #endregion


        #region public components


        public ObservableCollection<IDataSetViewModel> DataSets
        {
            get => _dataSets1;
            protected set
            {
                SetProperty(ref _dataSets1, value);
                _dataSetsProjectSavingCommand.Initialize(ref _dataSets1, _device, this.ChangeTracker);
                _dataSetsProjectSavingCommand.RefreshViewModel =
                    async () =>
                    {
                        await UpdateDataSets();
                    };
            }
        }

        public IDataSetViewModel SelectedDataSet
        {
            get => _selectedDataSet;
            set
            {
                if (_selectedDataSet != null)
                {
                    _selectedDataSet.IsSelect = false;
                }

                SetProperty(ref _selectedDataSet, value, true);
                if (_selectedDataSet != null)
                {
                    _selectedDataSet.IsSelect = true;
                    ((ViewModelBase)_selectedDataSet).IsWarning = false;
                }
            }
        }

        public string ModelRegionKey { get; }

        public ICommand SaveChangesCommand { get; }
        public ICommand ExpandModelCommand { get; }
        public ICommand CollapseModelCommand { get; }

        public ICommand UpdateDataSetsCommand { get; }

        public IPresentationCommand AddNewDataSetCommand { get; }
        public ICommand DeleteDataSetViewModelCommand { get; }

        public bool IsModelShowed
        {
            get => _isModelShowed;
            set { SetProperty(ref _isModelShowed, value, true); }
        }

        public int MaxNumberOfDataSet => 30;

        #endregion


        #region override of NavigationViewModelBase

        protected override async void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            _regionName = navigationContext.BiscNavigationParameters
                .GetParameterByName<UiEntityIdentifier>(UiEntityIdentifier.Key).ItemId.ToString();
            SetIsReadOnly(
                navigationContext.BiscNavigationParameters.GetParameterByName<bool>(KeysForNavigation
                    .NavigationParameter.IsReadOnly));
            _navigationService.NavigateViewToRegion(KeysForNavigation.RegionNames.InfoModelTreeItemDetailsViewKey, ModelRegionKey,
                new BiscNavigationParameters()
                    .AddParameterByName("IED", _device)
                    .AddParameterByName("IsHideButtons", true)
                    .AddParameterByName(KeysForNavigation.NavigationParameter.IsReadOnly, true));

            await UpdateDataSets();
            base.OnNavigatedTo(navigationContext);
        }


        public override void OnActivate()
        {
            if (!IsReadOnly)
            {
                _userInterfaceComposingService.SetCurrentSaveCommand(SaveChangesCommand,
                $"Сохранить DataSets устройства {_device.Name}", false);
                _userInterfaceComposingService.AddGlobalCommand(UpdateDataSetsCommand, $"Обновить DataSets {_device.Name}",
                    IconsKeys.UpdateIconKey, false, true);
                _userInterfaceComposingService.AddGlobalCommand(AddNewDataSetCommand, $"Добавить DataSet {_device.Name}",
                    IconsKeys.AddIconKey, false, true);
            }

            base.OnActivate();
        }

        public override void OnDeactivate()
        {
            _userInterfaceComposingService.ClearCurrentSaveCommand();
            _userInterfaceComposingService.DeleteGlobalCommand(AddNewDataSetCommand);
            _userInterfaceComposingService.DeleteGlobalCommand(UpdateDataSetsCommand);

            base.OnDeactivate();
        }

        protected override void OnDisposing()
        {
            _saveCheckingService.RemoveSaveCheckingEntityByOwner(_regionName);
            _navigationService.DisposeRegionViewModel(ModelRegionKey);
            base.OnDisposing();
        }
        #endregion
    }
}