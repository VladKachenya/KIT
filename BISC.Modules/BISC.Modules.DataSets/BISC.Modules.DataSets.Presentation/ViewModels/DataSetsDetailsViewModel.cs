using BISC.Model.Infrastructure.Project;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.DataSets.Infrastructure.ViewModels.Factorys;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.DataSets.Presentation.ViewModels
{
    public class DataSetsDetailsViewModel : NavigationViewModelBase
    {
        private IDevice _device;
        private IDatasetModelService _datasetModelService;
        private IDatasetViewModelFactory _datasetViewModelFactory;

        #region C-tor

        public DataSetsDetailsViewModel(ICommandFactory commandFactory, IDeviceModelService deviceModelService,
            IBiscProject biscProject, IDatasetModelService datasetModelService, IDatasetViewModelFactory datasetViewModelFactory)
        {
            _datasetModelService = datasetModelService;
            _datasetViewModelFactory = datasetViewModelFactory;
            DeployAllExpandersCommand = commandFactory.CreatePresentationCommand(OnDeployAllExpanders);
            RollUpAllExpandersCommand = commandFactory.CreatePresentationCommand(OnRollUpAllExpanders);
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
        #endregion


        #region public components

        public ObservableCollection<IDataSetViewModel> DataSets { get; protected set; }

        public ICommand DeployAllExpandersCommand { get; }
        public ICommand RollUpAllExpandersCommand { get; }

        #endregion


        #region override of NavigationViewModelBase
        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            DataSets = _datasetViewModelFactory.GetDataSetsViewModel(_datasetModelService.GetAllDataSetOfDevice(_device));
            base.OnNavigatedTo(navigationContext);
        }
        #endregion
    }
}
