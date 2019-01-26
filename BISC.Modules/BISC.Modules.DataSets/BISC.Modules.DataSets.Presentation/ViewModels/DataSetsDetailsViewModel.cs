using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Events;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.DataSets.Presentation.ViewModels
{
    public class DataSetsDetailsViewModel : NavigationViewModelBase
    {
        private IDevice _device;
        private List<IDataSet> _dataSets;
        private IDatasetModelService _datasetModelService;
        private IDatasetViewModelFactory _datasetViewModelFactory;
        private DatasetsSavingByFtpCommand _datasetsSavingCommand;
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly IUserInterfaceComposingService _userInterfaceComposingService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly INavigationService _navigationService;
        private readonly ILoggingService _loggingService;
        private readonly IDeviceWarningsService _deviceWarningsService;
        private readonly IGlobalSavingService _globalSavingService;

        private readonly DatasetsLoadingService _datasetsLoadingService;
        private IBiscProject _biscProject;
        private ObservableCollection<IDataSetViewModel> _dataSets1;
        private string _regionName;
        private bool _isModelShowed;
        private bool _issUpdateDataSets = true;
        private bool _isSaveСhanges = true;
        #region C-tor

        public DataSetsDetailsViewModel(ICommandFactory commandFactory,
            IBiscProject biscProject, IDatasetModelService datasetModelService, IDatasetViewModelFactory datasetViewModelFactory,
            ISaveCheckingService saveCheckingService, IUserInterfaceComposingService userInterfaceComposingService,
            IConnectionPoolService connectionPoolService, IGlobalEventsService globalEventsService,
            INavigationService navigationService, ILoggingService loggingService, DatasetsLoadingService datasetsLoadingService,
            DatasetsSavingByFtpCommand datasetsSavingByFtpCommandCommand, IDeviceWarningsService deviceWarningsService, IGlobalSavingService globalSavingService)
        {
            _userInterfaceComposingService = userInterfaceComposingService;
            _connectionPoolService = connectionPoolService;
            _globalEventsService = globalEventsService;
            _navigationService = navigationService;
            _loggingService = loggingService;
            _datasetsLoadingService = datasetsLoadingService;
            _datasetsSavingCommand = datasetsSavingByFtpCommandCommand;
            _biscProject = biscProject;
            _datasetModelService = datasetModelService;
            _datasetViewModelFactory = datasetViewModelFactory;
            _saveCheckingService = saveCheckingService;
            _deviceWarningsService = deviceWarningsService;
            _globalSavingService = globalSavingService;

            SaveСhangesCommand = commandFactory.CreatePresentationCommand(OnSaveСhangesCommand, () => _isSaveСhanges);
            ExpandModelCommand = commandFactory.CreatePresentationCommand(OnExpandModel);
            CollapseModelCommand = commandFactory.CreatePresentationCommand(OnCollapseModel);
            AddNewDataSetCommand = commandFactory.CreatePresentationCommand(OnAddNewDataSet, IsAddNewDataSet);
            DeleteDataSetViewModelCommand = commandFactory.CreatePresentationCommand<object>(OnDeleteDataSetViewModel);
            ModelRegionKey = Guid.NewGuid().ToString();
            UpdateDataSetsCommand = commandFactory.CreatePresentationCommand(OnUpdateDataSets, () => _issUpdateDataSets);
        }

        private async void OnUpdateDataSets()
        {
            _issUpdateDataSets = false;
            (UpdateDataSetsCommand as IPresentationCommand)?.RaiseCanExecute();
            await UpdateDataSets(true);
            _issUpdateDataSets = true;
            (UpdateDataSetsCommand as IPresentationCommand)?.RaiseCanExecute();
        }




        private async Task UpdateDataSets(bool updateFromDevice)
        {
            BlockViewModelBehavior.SetBlock("Обновление данных", true);
            try
            {
                if (updateFromDevice && _connectionPoolService.GetConnection(_device.Ip).IsConnected)
                {
                    try
                    {
                        await _datasetsLoadingService.EstimateProgress(_device);
                        await _datasetsLoadingService.Load(_device, null, _biscProject.MainSclModel.Value,
                            new CancellationToken());
                        _loggingService.LogMessage($"DataSets устройства {_device.Name} вычитанны успешно", SeverityEnum.Info);
                    }
                    catch (Exception e)
                    {
                        _loggingService.LogMessage($"Ошибка вычитывания DataSets устройства {_device.Name}", SeverityEnum.Warning);
                    }
                }
                UpdateCurentChengeTracker();
                await Task.Delay(500);
            }
            finally
            {
                BlockViewModelBehavior.Unlock();
            }
        }

        private void UpdateCurentChengeTracker()
        {
            _dataSets = _datasetModelService.GetAllDataSetOfDevice(_device);
            SortDataSetsByIsDynamic();
            try
            {
                DataSets = _datasetViewModelFactory.GetDataSetsViewModel(_dataSets);
            }
            catch (Exception e)
            {
                if (DataSets == null && _dataSets != null)
                {
                    _loggingService.LogMessage($"Невозможно сформировать DataSet на основании модели устройства {_device.Name}.", SeverityEnum.Critical);
                    _deviceWarningsService.SetWarningOfDevice(_device.Name, DatasetKeys.DataSetWarningKeys.DataSetLoadErrorWarningTagKey, "Невозможно сформировать DataSets");


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
                $"DataSets устройства {_device.Name}", _datasetsSavingCommand, _device.Name, _regionName));
            AddNewDataSetCommand.RaiseCanExecute();
            ChangeTracker.AcceptChanges();
            ChangeTracker.SetTrackingEnabled(true);
            _datasetsSavingCommand.Initialize(DataSets, _device, this.ChangeTracker.GetIsModifiedRecursive());

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
            _loggingService.LogUserAction($"Пользователь удаляет DataSet {element.SelectedParentLd + "." + element.SelectedParentLn + "." + element.EditableNamePart}");
            DataSets.Remove(element);
            AddNewDataSetCommand.RaiseCanExecute();
            _datasetsSavingCommand.Initialize(DataSets, _device, this.ChangeTracker.GetIsModifiedRecursive());
        }
        private void OnAddNewDataSet()
        {
            _loggingService.LogUserAction($"Пользователь добавляет новый датасет в устройстве {_device.Name}");
            DataSets.Add(_datasetViewModelFactory.CreateDataSetViewModel(DataSets.Select((model => model.EditableNamePart)).ToList(), _device));
            AddNewDataSetCommand.RaiseCanExecute();
            _datasetsSavingCommand.Initialize(DataSets, _device, this.ChangeTracker.GetIsModifiedRecursive());
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
            (SaveСhangesCommand as IPresentationCommand)?.RaiseCanExecute();
            try
            {
                _loggingService.LogUserAction($"Пользователь сохраняет изменения DataSets устройства {_device.Name}");
                BlockViewModelBehavior.SetBlock("Сохранение DataSet-ов", true);
                var savingRes = await _globalSavingService.SaveСhangesToRegion(_regionName);
                if (savingRes.IsSaved)
                {
                    await UpdateDataSets(true);
                }
            }
            catch (Exception e)
            {
                _loggingService.LogUserAction($"Ошибка записи DataSet в устройство {e.Message}");
            }
            finally
            {
                BlockViewModelBehavior.Unlock();
                _isSaveСhanges = true;
                (SaveСhangesCommand as IPresentationCommand)?.RaiseCanExecute();
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
            }
        }
        public string ModelRegionKey { get; }

        public ICommand SaveСhangesCommand { get; }
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

        public int MaxNambOfDataSet => 30;

        #endregion


        #region override of NavigationViewModelBase

        protected override async void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            _regionName = navigationContext.BiscNavigationParameters
                .GetParameterByName<TreeItemIdentifier>(TreeItemIdentifier.Key).ItemId.ToString();
            _navigationService.NavigateViewToRegion(InfoModelKeys.InfoModelTreeItemDetailsViewKey, ModelRegionKey,
                new BiscNavigationParameters().AddParameterByName("IED", _device));
            await UpdateDataSets(false);
            base.OnNavigatedTo(navigationContext);
        }



        public override void OnActivate()
        {
            //ShowFtpBlockMessageIfNeeded();
            _userInterfaceComposingService.SetCurrentSaveCommand(SaveСhangesCommand, $"Сохранить DataSets устройства { _device.Name}", _connectionPoolService.GetConnection(_device.Ip).IsConnected);
            _userInterfaceComposingService.AddGlobalCommand(UpdateDataSetsCommand, $"Обновить DataSets {_device.Name}", IconsKeys.UpdateIconKey, false, true);
            _userInterfaceComposingService.AddGlobalCommand(AddNewDataSetCommand, $"Добавить DataSet {_device.Name}", IconsKeys.AddIconKey, false, true);

            _globalEventsService.Subscribe<ConnectionEvent>(OnConnectionChanged);

            base.OnActivate();

        }

        private void OnConnectionChanged(ConnectionEvent connectionEvent)
        {
            if (connectionEvent.Ip == _device.Ip)
            {
                _userInterfaceComposingService.SetCurrentSaveCommand(SaveСhangesCommand, $"Сохранить DataSets устройства { _device.Name}", connectionEvent.IsConnected);

            }
        }

        public override void OnDeactivate()
        {
            _userInterfaceComposingService.ClearCurrentSaveCommand();
            _userInterfaceComposingService.DeleteGlobalCommand(AddNewDataSetCommand);
            _userInterfaceComposingService.DeleteGlobalCommand(UpdateDataSetsCommand);
            _globalEventsService.Unsubscribe<ConnectionEvent>(OnConnectionChanged);

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
