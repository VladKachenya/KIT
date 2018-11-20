using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.IoC;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Services.Helpers;
using BISC.Modules.Device.Presentation.ViewModels.Factories;
using BISC.Presentation.BaseItems.Commands;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.BaseItems.ViewModels.Behaviors;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Modules.Device.Presentation.ViewModels.Conflicts
{
    public class DeviceConflictsViewModel : NavigationViewModelBase
    {
        private readonly ICommandFactory _commandFactory;
        private readonly DeviceConflictFactory _deviceConflictFactory;
        private readonly List<IElementConflictResolver> _elementConflictResolvers;
        private bool _isApplyButtonEnabled;
        private DeviceConflictEntity _conflictEntity;

        public DeviceConflictsViewModel(ICommandFactory commandFactory,IInjectionContainer injectionContainer, DeviceConflictFactory deviceConflictFactory)
        {
            _commandFactory = commandFactory;
            _deviceConflictFactory = deviceConflictFactory;
            ApplyCommand = commandFactory.CreatePresentationCommand(OnApply);
            DeviceConflictViewModels=new ObservableCollection<DeviceConflictViewModel>();
            _elementConflictResolvers = injectionContainer.ResolveAll(typeof(IElementConflictResolver)).Cast<IElementConflictResolver>().ToList();
            CancelCommand = commandFactory.CreatePresentationCommand(OnCancel);
            BlockViewModelBehavior=new BlockViewModelBehavior();
        }

        private void OnCancel()
        {
            DialogCommands.CloseDialogCommand.Execute(null,null);
        }

        private async void OnApply()
        {
            BlockViewModelBehavior.SetBlock("Обновление" ,true);
            foreach (var deviceConflictViewModel in DeviceConflictViewModels)
            {
                if(deviceConflictViewModel.IsConflictOk)continue;
                var res= await _elementConflictResolvers.FirstOrDefault((resolver =>
                        resolver.ConflictName == deviceConflictViewModel.ConflictTitle))?
                    .ResolveConflict(deviceConflictViewModel.IsConflictResolvedAsFromDevice, _conflictEntity.DeviceName,
                        _conflictEntity.SclModelDevice, _conflictEntity.SclModelProject);
                if (res.IsRestartNeeded)
                {
                    _conflictEntity.IsRestartNeeded = true;
                }
            }

            await Task.Delay(1000);
            BlockViewModelBehavior.Unlock();
            DialogCommands.CloseDialogCommand.Execute(null,null);
        }

        public ObservableCollection<DeviceConflictViewModel> DeviceConflictViewModels { get; }

        #region Overrides of NavigationViewModelBase

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            _conflictEntity =
                navigationContext.BiscNavigationParameters.GetParameterByName<DeviceConflictEntity>(DeviceKeys
                    .DeviceConflictEntityKey);
            foreach (var elementConflictResolver in _elementConflictResolvers)
            {
               var conflictViewModel= _deviceConflictFactory.CreateDeviceConflictViewModel(_conflictEntity,elementConflictResolver);
                DeviceConflictViewModels.Add(conflictViewModel);
            }
            foreach (DeviceConflictViewModel deviceConflictViewModel in DeviceConflictViewModels)
            {
                deviceConflictViewModel.PropertyChanged += DeviceConflictViewModel_PropertyChanged;
            }
        }


        private void DeviceConflictViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsApplyButtonEnabled = DeviceConflictViewModels.All((model => model.IsConflictResolved));
        }

        public ICommand CancelCommand { get; }

        public ICommand ApplyCommand { get; }

        public bool IsApplyButtonEnabled
        {
            get => _isApplyButtonEnabled;
            set => SetProperty(ref _isApplyButtonEnabled , value);
        }

        #region Overrides of ViewModelBase

        protected override void OnDisposing()
        {
            base.OnDisposing();
        }

        public override void OnDeactivate()
        {
            base.OnDeactivate();
        }

        protected override void OnNavigatedFrom(BiscNavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);
        }

        #endregion

        #endregion
    }
}