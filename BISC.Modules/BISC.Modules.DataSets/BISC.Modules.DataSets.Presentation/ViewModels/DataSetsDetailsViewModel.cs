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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Infrastructure.Events;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Presentation.Infrastructure.ChangeTracker;
using BISC.Presentation.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.Factorys;

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
        private IBiscProject _biscProject;
        private ObservableCollection<IDataSetViewModel> _dataSets1;
        private List<IDataSet> _dataSetsDeletsElements;
        private IDataSetFactory _dataSetFactory;
        private string _regionName;

        #region C-tor

        public DataSetsDetailsViewModel(ICommandFactory commandFactory, IDeviceModelService deviceModelService,
            IBiscProject biscProject, IDatasetModelService datasetModelService, IDatasetViewModelFactory datasetViewModelFactory,
            ISaveCheckingService saveCheckingService, IDataSetFactory dataSetFactory, IUserInterfaceComposingService userInterfaceComposingService,
            IConnectionPoolService connectionPoolService,IGlobalEventsService globalEventsService)
        {
            _userInterfaceComposingService = userInterfaceComposingService;
            _connectionPoolService = connectionPoolService;
            _globalEventsService = globalEventsService;
            _biscProject = biscProject;
            _datasetModelService = datasetModelService;
            _datasetViewModelFactory = datasetViewModelFactory;
            _saveCheckingService = saveCheckingService;
            _dataSetFactory = dataSetFactory;
            DeployAllExpandersCommand = commandFactory.CreatePresentationCommand(OnDeployAllExpanders);
            RollUpAllExpandersCommand = commandFactory.CreatePresentationCommand(OnRollUpAllExpanders);
            SaveСhangesCommand = commandFactory.CreatePresentationCommand(OnSaveСhanges);
            AddNewDataSetCommand = commandFactory.CreatePresentationCommand(OnAddNewDataSet);
            DeleteDataSetViewModelCommand = commandFactory.CreatePresentationCommand<object>(OnDeleteDataSetViewModel);
            _dataSetsDeletsElements = new List<IDataSet>();
        }

        #endregion

        #region privat methods
        private void OnDeleteDataSetViewModel(object dataSetViewModel)
        {
            var element = dataSetViewModel as IDataSetViewModel;
            DataSets.Remove(element);
            _dataSetsDeletsElements.Add(element.GetModel());
        }
        private void OnAddNewDataSet()
        {
            var parient = _dataSets[0]?.ParentModelElement;
            IDataSet newDataSet = _dataSetFactory.GetDataSet(parient, GetUniqueNameOfDataSet());
            DataSets.Add(_datasetViewModelFactory.GetDataSetViewModel(newDataSet));
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
            _dataSetsDeletsElements.Clear();
            DataSets.Clear();
        }

        private void OnSaveСhanges()
        {
            foreach(var deletedDataSet in _dataSetsDeletsElements)
            {
                if (deletedDataSet.ParentModelElement != null)
                    _datasetModelService.DeleteDatasetFromDevice(deletedDataSet, _device);
                else
                    _datasetModelService.DeleteDatasetFromDevice(deletedDataSet, _device, "LD0", "LLN0");
            }
            foreach (var dataSetVM in DataSets)
            {
                var model = dataSetVM.GetModel();
                if(model.ParentModelElement != null)
                    _datasetModelService.AddDatasetToDevice(dataSetVM.GetModel(), _device);
                else
                    _datasetModelService.AddDatasetToDevice(dataSetVM.GetModel(), _device, "LD0", "LLN0");
            }
            ResetAllDataSetCollections();
            _dataSets = _datasetModelService.GetAllDataSetOfDevice(_device);
            SortDataSetsByIsDynamic();
            DataSets = _datasetViewModelFactory.GetDataSetsViewModel(_dataSets);
            ChangeTracker.AcceptChanges();
        }

        private string GetUniqueNameOfDataSet()
        {
            string nameBody = "NewDataSet";
            string result;
            int i = 0;
            bool isFind ;
            do
            {
                i++;
                result = nameBody + i.ToString();
                isFind = false;
                foreach (var element in DataSets)
                {
                    if(result == element.FixedNamePart+element.EditableNamePart)
                        isFind = true;
                }
            } while (isFind);

            return result;
        }

        private void SortDataSetsByIsDynamic()
        {
            List<IDataSet> isDynamicDataSets = new List<IDataSet>();
            List<IDataSet> notIsDynamicDataSets = new List<IDataSet>();
            foreach(var element in _dataSets)
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
            protected set { SetProperty(ref _dataSets1 ,value); }
        }

        public ICommand DeployAllExpandersCommand { get; }
        public ICommand RollUpAllExpandersCommand { get; }
        public ICommand SaveСhangesCommand { get; }

        public ICommand AddNewDataSetCommand { get; }
        public ICommand DeleteDataSetViewModelCommand { get; }
        #endregion


        #region override of NavigationViewModelBase
        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            _dataSets = _datasetModelService.GetAllDataSetOfDevice(_device);
            SortDataSetsByIsDynamic();
            DataSets = _datasetViewModelFactory.GetDataSetsViewModel(_dataSets);
            _regionName = navigationContext.BiscNavigationParameters
                .GetParameterByName<TreeItemIdentifier>(TreeItemIdentifier.Key).ItemId.ToString();
            _saveCheckingService.AddSaveCheckingEntity(new SaveCheckingEntity(ChangeTracker, $"DataSets устройства {_device.Name}",SaveСhangesCommand,_regionName ));
            ChangeTracker.SetTrackingEnabled(true);
       
            base.OnNavigatedTo(navigationContext);
        }

  

        public override void OnActivate()
        {
            
                _userInterfaceComposingService.SetCurrentSaveCommand(SaveСhangesCommand, $"Сохранить DataSets устройства { _device.Name}", _connectionPoolService.GetConnection(_device.Ip).IsConnected);
            _globalEventsService.Subscribe<ConnectionEvent>(OnConnectionChanged);

            base.OnActivate();

        }

        private void OnConnectionChanged(ConnectionEvent connectionEvent)
        {
            if (connectionEvent.Ip == _device.Ip)
            {
                _userInterfaceComposingService.SetCurrentSaveCommand(SaveСhangesCommand, $"Сохранить DataSets устройства { _device.Name}", _connectionPoolService.GetConnection(_device.Ip).IsConnected);

            }
        }

        public override void OnDeactivate()
        {
            _userInterfaceComposingService.ClearCurrentSaveCommand();
            _globalEventsService.Unsubscribe<ConnectionEvent>(OnConnectionChanged);

            base.OnDeactivate();
        }

    

        protected override void OnDisposing()
        {
          _saveCheckingService.RemoveSaveCheckingEntityByOwner(_regionName);
            base.OnDisposing();
        }


        #endregion

    }
}
