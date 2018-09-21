using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Tabs
{
    public class GooseMatrixTabViewModel:NavigationViewModelBase
    {
        private readonly INavigationService _navigationService;
        private bool _isGooseControlAssignmentSelected;

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
        }
        private void OnSelectMatrixEdit()
        {
            IsGooseControlAssignmentSelected = false;
        }


        public bool IsGooseControlAssignmentSelected
        {
            get { return _isGooseControlAssignmentSelected; }
            set { SetProperty(ref _isGooseControlAssignmentSelected, value); }
        }
    }
}
