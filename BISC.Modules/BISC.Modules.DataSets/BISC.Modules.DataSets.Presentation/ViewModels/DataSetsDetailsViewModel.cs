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
using BISC.Presentation.Infrastructure.ChangeTracker;
using BISC.Presentation.Infrastructure.Services;

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
        private IBiscProject _biscProject;
        private ObservableCollection<IDataSetViewModel> _dataSets1;

        #region C-tor

        public DataSetsDetailsViewModel(ICommandFactory commandFactory, IDeviceModelService deviceModelService,IUserInterfaceComposingService userInterfaceComposingService,
            IBiscProject biscProject, IDatasetModelService datasetModelService, IDatasetViewModelFactory datasetViewModelFactory,ISaveCheckingService saveCheckingService)
        {
            _userInterfaceComposingService = userInterfaceComposingService;
            _biscProject = biscProject;
            _datasetModelService = datasetModelService;
            _datasetViewModelFactory = datasetViewModelFactory;
            _saveCheckingService = saveCheckingService;
            DeployAllExpandersCommand = commandFactory.CreatePresentationCommand(OnDeployAllExpanders);
            RollUpAllExpandersCommand = commandFactory.CreatePresentationCommand(OnRollUpAllExpanders);
            SaveСhangesCommand = commandFactory.CreatePresentationCommand(OnSaveСhanges);
        }

        #endregion

        #region privat methods
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

        private void OnSaveСhanges()
        {
            _dataSets.Clear();
            foreach (var dataSetVM in DataSets)
            {
                _dataSets.Add(dataSetVM.GetModel());
            }
            ChangeTracker.AcceptChanges();
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

        #endregion


        #region override of NavigationViewModelBase
        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            _dataSets = _datasetModelService.GetAllDataSetOfDevice(_device);
            DataSets = _datasetViewModelFactory.GetDataSetsViewModel(_dataSets);
            _saveCheckingService.AddSaveCheckingEntity(new SaveCheckingEntity(ChangeTracker, $"DataSets устройства {_device.Name}",SaveСhangesCommand, navigationContext.BiscNavigationParameters.GetParameterByName<TreeItemIdentifier>(TreeItemIdentifier.Key).ItemId.ToString()));
            ChangeTracker.SetTrackingEnabled(true);
          
            base.OnNavigatedTo(navigationContext);
        }

  

        public override void OnActivate()
        {
            _userInterfaceComposingService.SetCurrentSaveCommand(SaveСhangesCommand, $"Сохранить DataSets устройства { _device.Name}");
            base.OnActivate();
        }

        public override void OnDeactivate()
        {
            _userInterfaceComposingService.ClearCurrentSaveCommand();
            base.OnDeactivate();
        }

    

        protected override void OnDisposing()
        {
          
            base.OnDisposing();
        }


        #endregion

    }
}
