using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.DataSets.Presentation.ViewModels
{
    public class DataSetsTreeItemViewModel : NavigationViewModelBase, IDataSetsTreeItemViewModel
    {
        private readonly ITabManagementService _tabManagementService;
        private IDevice _device;

        #region C-tor
        public DataSetsTreeItemViewModel(ICommandFactory commandFactory, ITabManagementService tabManagementService)
        {
            _tabManagementService = tabManagementService;
            NavigateToDetailsCommand = commandFactory.CreatePresentationCommand(OnNavigateToDetails);
        }
        #endregion


        #region private filds
        private void OnNavigateToDetails()
        {

        }
        #endregion




        #region Implementation of IDataSetsTreeItemViewModel
        public ICommand NavigateToDetailsCommand { get; }

        #endregion
    }
}
