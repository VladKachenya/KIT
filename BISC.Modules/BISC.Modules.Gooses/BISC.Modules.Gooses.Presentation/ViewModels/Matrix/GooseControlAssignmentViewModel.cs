using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Presentation.Commands;
using BISC.Modules.Gooses.Presentation.Interfaces.Factories;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Entities;
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Matrix
{
    public class GooseControlAssignmentViewModel : NavigationViewModelBase
    {
     
    
        private readonly IGooseControlBlockAssignmentItemFactory _gooseControlBlockAssignmentItemFactory;
        private readonly GooseControlAssignmentSavingCommand _gooseControlAssignmentSavingCommand;
        private IDevice _device;


        public GooseControlAssignmentViewModel(
            ICommandFactory commandFactory, IGooseControlBlockAssignmentItemFactory gooseControlBlockAssignmentItemFactory, GooseControlAssignmentSavingCommand gooseControlAssignmentSavingCommand)
        {
          
            _gooseControlBlockAssignmentItemFactory = gooseControlBlockAssignmentItemFactory;
            _gooseControlAssignmentSavingCommand = gooseControlAssignmentSavingCommand;
            SaveChangesCommand = commandFactory.CreatePresentationCommand(OnSaveChanges);
            GooseControlBlockAssignmentItems = new ObservableCollection<GooseControlBlockAssignmentItem>();

        }

        private void OnSaveChanges()
        {
            _gooseControlAssignmentSavingCommand.Initialize(GooseControlBlockAssignmentItems,_device);
          
            _gooseControlAssignmentSavingCommand.SaveAsync();
        }


        protected override void OnNavigatedFrom(BiscNavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);
        }


        public ICommand SaveChangesCommand { get; }

        public ObservableCollection<GooseControlBlockAssignmentItem> GooseControlBlockAssignmentItems { get; }
        #region Overrides of NavigationViewModelBase

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            GooseControlBlockAssignmentItems.Clear();
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>("IED");
            foreach (var gooseControlBlockAssignmentItem in _gooseControlBlockAssignmentItemFactory.CreateGooseControlBlockAssignmentItems(_device))
            {
                GooseControlBlockAssignmentItems.Add(gooseControlBlockAssignmentItem);
            }
          
            base.OnNavigatedTo(navigationContext);
        }



        #endregion
    }
}