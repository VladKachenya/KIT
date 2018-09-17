using System.Windows.Input;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Tree
{
    public class GooseGroupTreeItemViewModel : NavigationViewModelBase
    {
        private readonly ITabManagementService _tabManagementService;

        public GooseGroupTreeItemViewModel(ICommandFactory commandFactory, ITabManagementService tabManagementService)
        {
            _tabManagementService = tabManagementService;
            NavigateToDetailsCommand = commandFactory.CreatePresentationCommand(OnNavigateToDetails);
        }

        private void OnNavigateToDetails()
        {

        }

        public ICommand NavigateToDetailsCommand { get; }

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
        }
    }
}