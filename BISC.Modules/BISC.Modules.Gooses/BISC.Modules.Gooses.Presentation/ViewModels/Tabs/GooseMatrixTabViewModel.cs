using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Tabs
{
    public class GooseMatrixTabViewModel:NavigationViewModelBase
    {
        private readonly INavigationService _navigationService;
        private bool _isGooseControlAssignmentSelected;
        private IDevice _device;


        public GooseMatrixTabViewModel(ICommandFactory commandFactory,INavigationService navigationService)
        {
            _navigationService = navigationService;
            SelectMatrixEditCommand = commandFactory.CreatePresentationCommand(OnSelectMatrixEdit);
            SelectGooseControlAssignmentCommand =
                commandFactory.CreatePresentationCommand(OnSelectGooseControlAssignment);

        }
        public ICommand SelectGooseControlAssignmentCommand { get; }
        public ICommand SelectMatrixEditCommand { get; }
        private void OnSelectGooseControlAssignment()
        {
            IsGooseControlAssignmentSelected = true;
            BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
            biscNavigationParameters.AddParameterByName("IED", _device);
            _navigationService.NavigateViewToRegion(GooseKeys.GoosePresentationKeys.GooseControlAssignmentViewKey,GooseKeys.GoosePresentationKeys.GooseMatrixTabFieldKey,biscNavigationParameters);
        }
        private void OnSelectMatrixEdit()
        {
            IsGooseControlAssignmentSelected = false;
            BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
            biscNavigationParameters.AddParameterByName("IED", _device);
            _navigationService.NavigateViewToRegion(GooseKeys.GoosePresentationKeys.GooseMatrixViewKey, GooseKeys.GoosePresentationKeys.GooseMatrixTabFieldKey, biscNavigationParameters);
        }


        public bool IsGooseControlAssignmentSelected
        {
            get { return _isGooseControlAssignmentSelected; }
            set { SetProperty(ref _isGooseControlAssignmentSelected, value); }
        }

        #region Overrides of NavigationViewModelBase

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>("IED");
            OnSelectGooseControlAssignment();
            base.OnNavigatedTo(navigationContext);
        }

        #endregion
    }
}
