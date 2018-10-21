using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Keys;
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

namespace BISC.Modules.Reports.Presentation.ViewModels
{
    public class ReportsTreeItemViewModel : NavigationViewModelBase
    {
        private readonly ITabManagementService _tabManagementService;
        private IDevice _device;

        private TreeItemIdentifier _reportsDetailsIdentifier;
        #region C-tor
        public ReportsTreeItemViewModel(ICommandFactory commandFactory, ITabManagementService tabManagementService)
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
            _tabManagementService.NavigateToTab(ReportsKeys.ReportsPresentationKeys.ReportsDetailsView, biscNavigationParameters, $"Reports {_device.Name}", _reportsDetailsIdentifier);
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
            _reportsDetailsIdentifier = new TreeItemIdentifier(Guid.NewGuid(), treeItemIdentifier);

            base.OnNavigatedTo(navigationContext);
        }
    }
}
