using BISC.Model.Iec61850Ed2;
using BISC.Model.Iec61850Ed2.DataTypeTemplates;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.DataSets.Infrastructure.ViewModels.Factorys;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Model.Elements;
using BISC.Modules.InformationModel.Presentation.ViewModels.Base;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using GongSolutions.Wpf.DragDrop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Infrastructure.Events;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Presentation.Infrastructure.ChangeTracker;
using BISC.Presentation.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.Factorys;
using BISC.Modules.DataSets.Presentation.Services.Interfaces;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.DataSets.Model.Services;
using BISC.Presentation.Infrastructure.Commands;

namespace BISC.Modules.DataSets.Presentation.ViewModels
{
    public class DataSetsDetailsViewModel : NavigationViewModelBase
    {
        private IDevice _device;
        private List<IDataSet> _dataSets;
        private IDatasetModelService _datasetModelService;
        private IDatasetViewModelFactory _datasetViewModelFactory;
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly IUserInterfaceComposingService _userInterfaceComposingService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly IDataSetSavingService _dataSetSavingService;
        private readonly INavigationService _navigationService;
        private readonly IProjectService _projectService;
        private readonly ILoggingService _loggingService;
        private readonly DatasetsLoadingService _datasetsLoadingService;
        private IBiscProject _biscProject;
        private ObservableCollection<IDataSetViewModel> _dataSets1;
        private string _regionName;
        private bool _isModelShowed;
        #region C-tor

        public DataSetsDetailsViewModel(ICommandFactory commandFactory, IDeviceModelService deviceModelService,
            IBiscProject biscProject, IDatasetModelService datasetModelService, IDatasetViewModelFactory datasetViewModelFactory,
            ISaveCheckingService saveCheckingService, IUserInterfaceComposingService userInterfaceComposingService,
            IConnectionPoolService connectionPoolService, IGlobalEventsService globalEventsService, IDataSetSavingService dataSetSavingService, 
            INavigationService navigationService, IProjectService projectService,ILoggingService loggingService, DatasetsLoadingService  datasetsLoadingService)
        {
            _userInterfaceComposingService = userInterfaceComposingService;
            _connectionPoolService = connectionPoolService;
            _globalEventsService = globalEventsService;
            _dataSetSavingService = dataSetSavingService;
            _navigationService = navigationService;
            _projectService = projectService;
            _loggingService = loggingService;
            _datasetsLoadingService = datasetsLoadingService;
            _biscProject = biscProject;
            _datasetModelService = datasetModelService;
            _datasetViewModelFactory = datasetViewModelFactory;
            _saveCheckingService = saveCheckingService;
            DeployAllExpandersCommand = commandFactory.CreatePresentationCommand(OnDeployAllExpanders);
            RollUpAllExpandersCommand = commandFactory.CreatePresentationCommand(OnRollUpAllExpanders);
            SaveСhangesCommand = commandFactory.CreatePresentationCommand(OnSaveСhanges);
            ExpandModelCommand = commandFactory.CreatePresentationCommand(OnExpandModel);
            CollapseModelCommand = commandFactory.CreatePresentationCommand(OnCollapseModel);
            AddNewDataSetCommand = commandFactory.CreatePresentationCommand(OnAddNewDataSet, IsAddNewDataSet);
            DeleteDataSetViewModelCommand = commandFactory.CreatePresentationCommand<object>(OnDeleteDataSetViewModel);
            ModelRegionKey = Guid.NewGuid().ToString();
            UpdateDataSetsCommand = commandFactory.CreatePresentationCommand(OnUpdateDataSets);
        }

        private async void OnUpdateDataSets()
        {
          await UpdateDataSets(true);
        }


        private async Task UpdateDataSets(bool updateFromDevice)
        {
            BlockViewModelBehavior.SetBlock("Обновление данных",true);
            if (updateFromDevice && _connectionPoolService.GetConnection(_device.Ip).IsConnected)
            {
                await _datasetsLoadingService.EstimateProgress(_device);
                await _datasetsLoadingService.Load(_device, null, _biscProject.MainSclModel.Value, new CancellationToken());
            }
            _dataSets = _datasetModelService.GetAllDataSetOfDevice(_device);
            SortDataSetsByIsDynamic();
            DataSets = _datasetViewModelFactory.GetDataSetsViewModel(_dataSets);
            _saveCheckingService.RemoveSaveCheckingEntityByOwner(_regionName);
            _saveCheckingService.AddSaveCheckingEntity(new SaveCheckingEntity(ChangeTracker,
                $"DataSets устройства {_device.Name}", SaveСhangesCommand,_device.Name, _regionName));
            AddNewDataSetCommand.RaiseCanExecute();
            ChangeTracker.AcceptChanges();
            ChangeTracker.SetTrackingEnabled(true);
            await Task.Delay(500);
            BlockViewModelBehavior.Unlock();
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
            _loggingService.LogUserAction($"Пользователь удаляет DataSet {element.SelectedParentLd + "." + element.SelectedParentLn+"."+element.EditableNamePart}");
            DataSets.Remove(element);
            AddNewDataSetCommand.RaiseCanExecute();
        }
        private void OnAddNewDataSet()
        {
            _loggingService.LogUserAction($"Пользователь добавляет новый датасет в устройстве {_device.Name}");
            DataSets.Add(_datasetViewModelFactory.CreateDataSetViewModel(DataSets.Select((model => model.EditableNamePart)).ToList(), _device));
            AddNewDataSetCommand.RaiseCanExecute();
        }

        private bool IsAddNewDataSet()
        {
            var isEditebleDs = DataSets.Where(ds => ds.IsEditeble);
            if (isEditebleDs.Count() >= 10) return false;
            return true;
        }
        private void OnDeployAllExpanders()
        {
            foreach (var element in DataSets)
                element.IsExpanded = true;
        }

        private void OnRollUpAllExpanders()
        {
            foreach (var element in DataSets)
                element.IsExpanded = false;
        }

        private void ResetAllDataSetCollections()
        {
            _dataSets.Clear();
            DataSets.Clear();
        }

        private async void OnSaveСhanges()
        {

            BlockViewModelBehavior.SetBlock("Сохранение DataSet-ов",true);
            await Task.Delay(500);
            _loggingService.LogUserAction($"Пользователь сохраняет изменения DataSets устройства {_device.Name}");
            await _dataSetSavingService.SaveDataSets(DataSets.ToList(), _device, _connectionPoolService.GetConnection(_device.Ip).IsConnected);
            ResetAllDataSetCollections();
            _dataSets = _datasetModelService.GetAllDataSetOfDevice(_device);
            SortDataSetsByIsDynamic();
            DataSets = _datasetViewModelFactory.GetDataSetsViewModel(_dataSets);
            ChangeTracker.AcceptChanges();
            BlockViewModelBehavior.Unlock();
        }



        private void SortDataSetsByIsDynamic()
        {
            List<IDataSet> isDynamicDataSets = new List<IDataSet>();
            List<IDataSet> notIsDynamicDataSets = new List<IDataSet>();
            foreach (var element in _dataSets)
            {
                if (element.IsDynamic)
                    isDynamicDataSets.Add(element);
                else
                    notIsDynamicDataSets.Add(element);
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
            protected set { SetProperty(ref _dataSets1, value); }
        }
        public string ModelRegionKey { get; }
        public ICommand DeployAllExpandersCommand { get; }
        public ICommand RollUpAllExpandersCommand { get; }
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
           
            _userInterfaceComposingService.SetCurrentSaveCommand(SaveСhangesCommand, $"Сохранить DataSets устройства { _device.Name}", _connectionPoolService.GetConnection(_device.Ip).IsConnected);
            _userInterfaceComposingService.AddGlobalCommand(UpdateDataSetsCommand, $"Обновить DataSets {_device.Name}", IconsKeys.UpdateIconKey, false, true);
            _userInterfaceComposingService.AddGlobalCommand(AddNewDataSetCommand,$"Добавить DataSet {_device.Name}",IconsKeys.AddIconKey,false,true);

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
