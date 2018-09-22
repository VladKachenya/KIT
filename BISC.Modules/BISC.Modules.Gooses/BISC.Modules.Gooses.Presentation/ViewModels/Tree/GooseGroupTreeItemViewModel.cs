using System;
using System.Windows.Input;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Tree
{
    public class GooseGroupTreeItemViewModel : NavigationViewModelBase
    {
        private readonly ITabManagementService _tabManagementService;
        private IDevice _device;

        private TreeItemIdentifier _subscriptionIdentifier;
        private TreeItemIdentifier _matrixIdentifier;
        private TreeItemIdentifier _gooseEditIdentifier;


        public GooseGroupTreeItemViewModel(ICommandFactory commandFactory, ITabManagementService tabManagementService)
        {
            _tabManagementService = tabManagementService;
            NavigateToDetailsCommand = commandFactory.CreatePresentationCommand(OnNavigateToDetails);
            NavigateToSubscriptionCommand = commandFactory.CreatePresentationCommand(OnNavigateToSubscription);
            NavigateToMatrixCommand = commandFactory.CreatePresentationCommand(OnNavigateToMatrix);

        }

        private void OnNavigateToMatrix()
        {
            BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
            biscNavigationParameters.AddParameterByName("IED", _device);
            _tabManagementService.NavigateToTab(GooseKeys.GoosePresentationKeys.GooseMatrixTabKey, biscNavigationParameters, $"Goose матрица {_device.Name}", _matrixIdentifier);
        }

        private void OnNavigateToSubscription()
        {
            BiscNavigationParameters biscNavigationParameters=new BiscNavigationParameters();
            biscNavigationParameters.AddParameterByName("IED", _device);
            _tabManagementService.NavigateToTab(GooseKeys.GoosePresentationKeys.GooseSubscriptionTabKey, biscNavigationParameters, $"Подписка {_device.Name}", _subscriptionIdentifier);
        }

        private void OnNavigateToDetails()
        {

        }

        public ICommand NavigateToDetailsCommand { get; }
        public ICommand NavigateToSubscriptionCommand { get; }
        public ICommand NavigateToMatrixCommand { get; }

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>("IED");
            var treeItemIdentifier =
                navigationContext.BiscNavigationParameters.GetParameterByName<TreeItemIdentifier>(
                    TreeItemIdentifier.Key);
            _subscriptionIdentifier=new TreeItemIdentifier(treeItemIdentifier.ItemId,Guid.NewGuid());
            _matrixIdentifier = new TreeItemIdentifier(treeItemIdentifier.ItemId, Guid.NewGuid());
            _gooseEditIdentifier = new TreeItemIdentifier(treeItemIdentifier.ItemId, Guid.NewGuid());

            base.OnNavigatedTo(navigationContext);
        }
       
    }
}