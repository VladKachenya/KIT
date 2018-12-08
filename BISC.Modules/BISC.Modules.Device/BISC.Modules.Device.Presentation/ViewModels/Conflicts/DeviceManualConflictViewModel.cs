using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Modules.Device.Presentation.ViewModels.Conflicts
{
    public class DeviceManualConflictViewModel: DeviceConflictViewModel
    {
        private bool _isConflictResolvedAsFromProject;
        private bool _isConflictResolvedAsFromDevice;

        public DeviceManualConflictViewModel(ICommandFactory commandFactory)
        {
            SelectDeviceOptionCommand = commandFactory.CreatePresentationCommand(OnSelectDeviceOption);
            SelectProjectOptionCommand = commandFactory.CreatePresentationCommand(OnSelectProjectOption);
            CancelSelectionCommand = commandFactory.CreatePresentationCommand(OnCancelSelection);
            
        }
        private bool _isConflictResolved;
        public bool IsConflictResolved
        {
            get => _isConflictResolved;
            set => SetProperty(ref _isConflictResolved, value);
        }
        private void OnCancelSelection()
        {
            IsConflictResolved = false;
            IsConflictResolvedAsFromDevice = false;
            IsConflictResolvedAsFromProject = false;
        }

        private void OnSelectProjectOption()
        {
            IsConflictResolved = true;
            IsConflictResolvedAsFromProject = true;
        }

        private void OnSelectDeviceOption()
        {
            IsConflictResolved = true;
            IsConflictResolvedAsFromDevice = true;
        }


        public bool IsConflictResolvedAsFromDevice
        {
            get => _isConflictResolvedAsFromDevice;
            set => SetProperty(ref _isConflictResolvedAsFromDevice, value);
        }

        public bool IsConflictResolvedAsFromProject
        {
            get => _isConflictResolvedAsFromProject;
            set => SetProperty(ref _isConflictResolvedAsFromProject, value);
        }
        

        public ICommand SelectDeviceOptionCommand { get; }
        public ICommand SelectProjectOptionCommand { get;  }
        public ICommand CancelSelectionCommand { get; }
    }
}