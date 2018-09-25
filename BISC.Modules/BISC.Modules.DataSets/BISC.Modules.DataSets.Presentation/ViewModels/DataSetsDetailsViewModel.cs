using BISC.Model.Infrastructure.Project;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.DataSets.Presentation.ViewModels
{
    public class DataSetsDetailsViewModel : NavigationViewModelBase
    {
        private IDevice _device;
        private IDatasetModelService _datasetModelService;

        #region C-tor

        public DataSetsDetailsViewModel(ICommandFactory commandFactory, IDeviceModelService deviceModelService, 
            IBiscProject biscProject, IDatasetModelService datasetModelService)
        {
            _datasetModelService = datasetModelService;
        }

        #endregion


        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            var resalt = _datasetModelService.GetAllDataSetOfDevice(_device);
            base.OnNavigatedTo(navigationContext);
        }
    }
}
