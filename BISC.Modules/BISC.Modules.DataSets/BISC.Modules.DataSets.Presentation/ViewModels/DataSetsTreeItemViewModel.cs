using BISC.Modules.DataSets.Infrastructure.Keys;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
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

        private TreeItemIdentifier _dataSetDetailsIdentifier;
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
            BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
            biscNavigationParameters.AddParameterByName("IED", _device);
            _tabManagementService.NavigateToTab(DatasetKeys.DatasetViewModelKeys.DataSetsDetailsView, biscNavigationParameters, $"Data sets{_device.Name}", _dataSetDetailsIdentifier);
        }
        #endregion




        #region Implementation of IDataSetsTreeItemViewModel
        public ICommand NavigateToDetailsCommand { get; }

        #endregion

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>("IED");
            var treeItemIdentifier =
                navigationContext.BiscNavigationParameters.GetParameterByName<TreeItemIdentifier>(
                    TreeItemIdentifier.Key);
            _dataSetDetailsIdentifier = new TreeItemIdentifier(treeItemIdentifier.ItemId, Guid.NewGuid());

            base.OnNavigatedTo(navigationContext);
        }
    }
}
