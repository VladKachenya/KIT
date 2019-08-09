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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.Device.Presentation.ViewModels.Conflicts
{
    public class DeviceConflictsViewModel : NavigationViewModelBase
    {
        private readonly ICommandFactory _commandFactory;
        private readonly DeviceConflictFactory _deviceConflictFactory;
        private readonly List<IElementConflictResolver> _elementConflictResolvers;
        private bool _isApplyButtonEnabled;
        private DeviceConflictContext _conflictContext;

        public DeviceConflictsViewModel(ICommandFactory commandFactory, IInjectionContainer injectionContainer, DeviceConflictFactory deviceConflictFactory)
            : base(null)
        {
            _commandFactory = commandFactory;
            _deviceConflictFactory = deviceConflictFactory;
            ApplyCommand = commandFactory.CreatePresentationCommand(OnApply);
            DeviceConflictViewModels = new ObservableCollection<DeviceConflictViewModel>();
            _elementConflictResolvers = injectionContainer.ResolveAll(typeof(IElementConflictResolver)).Cast<IElementConflictResolver>().ToList();
            CancelCommand = commandFactory.CreatePresentationCommand(OnCancel);
            BlockViewModelBehavior = new BlockViewModelBehavior();
        }

        private void OnCancel()
        {
            DialogCommands.CloseDialogCommand.Execute(null, null);
        }

        private async void OnApply()
        {
            BlockViewModelBehavior.SetBlock("Обновление", true);
            try
            {


                foreach (var deviceConflictViewModel in DeviceConflictViewModels)
                {
                    bool isConflictResolvedFromDevice = false;

                    if (deviceConflictViewModel is DeviceManualConflictViewModel deviceManualConflictViewModel)
                    {
                        isConflictResolvedFromDevice = deviceManualConflictViewModel.IsConflictResolvedAsFromDevice;
                    }

                    if (deviceConflictViewModel.IsConflictOk)
                    {
                        continue;
                    }

                    var res = await _elementConflictResolvers.FirstOrDefault((resolver =>
                        resolver.ConflictName == deviceConflictViewModel.ConflictTitle))?
                    .ResolveConflict(isConflictResolvedFromDevice,
                        _conflictContext.DeviceGuid,
                        _conflictContext.SclModelDevice, _conflictContext.SclModelProject);
                    if (res.IsRestartNeeded)
                    {
                        _conflictContext.IsRestartNeeded = true;
                    }

                }
            }
            finally
            {
                await Task.Delay(1000);
                BlockViewModelBehavior.Unlock();
                DialogCommands.CloseDialogCommand.Execute(null, null);
            }
        }

        public ObservableCollection<DeviceConflictViewModel> DeviceConflictViewModels { get; }

        #region Overrides of NavigationViewModelBase

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            _conflictContext =
                navigationContext.BiscNavigationParameters.GetParameterByName<DeviceConflictContext>(DeviceKeys
                    .DeviceConflictContextKey);
            foreach (var elementConflictResolver in _elementConflictResolvers)
            {
                var conflictViewModel = _deviceConflictFactory.CreateDeviceConflictViewModel(_conflictContext, elementConflictResolver);
                DeviceConflictViewModels.Add(conflictViewModel);
            }
            foreach (ViewModelBase viewModelBase in DeviceConflictViewModels)
            {
                viewModelBase.PropertyChanged += DeviceConflictViewModel_PropertyChanged;
            }
            DeviceConflictViewModel_PropertyChanged(null, null);
        }


        private void DeviceConflictViewModel_PropertyChanged(object sender,
            System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsApplyButtonEnabled = DeviceConflictViewModels.All((model =>
            {
                if (model is DeviceManualConflictViewModel deviceManualConflictViewModel)
                {
                    if (!deviceManualConflictViewModel.IsConflictResolved)
                    {
                        return false;
                    }
                }
                return true;
            }));
        }

        public ICommand CancelCommand { get; }

        public ICommand ApplyCommand { get; }

        public bool IsApplyButtonEnabled
        {
            get => _isApplyButtonEnabled;
            set => SetProperty(ref _isApplyButtonEnabled, value);
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