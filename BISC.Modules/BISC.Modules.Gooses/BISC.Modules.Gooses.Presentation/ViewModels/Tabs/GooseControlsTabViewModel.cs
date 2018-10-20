using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Presentation.Factories;
using BISC.Modules.Gooses.Presentation.Services;
using BISC.Modules.Gooses.Presentation.ViewModels.GooseControls;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Tabs
{
    public class GooseControlsTabViewModel : NavigationViewModelBase
    {
        private readonly GooseControlViewModelFactory _gooseControlViewModelFactory;
        private readonly IGoosesModelService _goosesModelService;
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly ICommandFactory _commandFactory;
        private readonly ILoggingService _loggingService;
        private readonly GooseControlSavingService _gooseControlSavingService;
        private IDevice _device;
        private ObservableCollection<GooseControlViewModel> _gooseControlViewModels;
        private string _regionName;

        public GooseControlsTabViewModel(GooseControlViewModelFactory gooseControlViewModelFactory, IGoosesModelService goosesModelService,
            ISaveCheckingService saveCheckingService, ICommandFactory commandFactory, ILoggingService loggingService, GooseControlSavingService gooseControlSavingService)
        {
            _gooseControlViewModelFactory = gooseControlViewModelFactory;
            _goosesModelService = goosesModelService;
            _saveCheckingService = saveCheckingService;
            _commandFactory = commandFactory;
            _loggingService = loggingService;
            _gooseControlSavingService = gooseControlSavingService;
            SaveCommand = commandFactory.CreatePresentationCommand(OnSaveChangesCommand);
            DeleteGooseCommand = commandFactory.CreatePresentationCommand<object>(OnDeleteGoose);
        }

        private void OnDeleteGoose(object obj)
        {
            var gooseCbVm = obj as GooseControlViewModel;
            if (gooseCbVm == null) return;
            _loggingService.LogUserAction($"Пользователь удаляет Goose CB {gooseCbVm.Name} (устройство {_device.Name})");
            GooseControlViewModels.Remove(gooseCbVm);
        }

        private async void OnSaveChangesCommand()
        {
            _loggingService.LogUserAction($"Пользователь сохряняет изменения в Goose CB устройства {_device.Name})");
            await _gooseControlSavingService.SaveGooseControls(GooseControlViewModels.ToList());
            UpdateViewModels();
            ChangeTracker.AcceptChanges();
        }


        #region Overrides of NavigationViewModelBase

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>("IED");
            _regionName = navigationContext.BiscNavigationParameters
                .GetParameterByName<TreeItemIdentifier>(TreeItemIdentifier.Key).ItemId.ToString();
            UpdateViewModels();
            ChangeTracker.SetTrackingEnabled(true);
            _saveCheckingService.AddSaveCheckingEntity(new SaveCheckingEntity(ChangeTracker, $"Блоки управления GOOSE {_device.Name}", SaveCommand, _regionName));
            base.OnNavigatedTo(navigationContext);
        }

        private void UpdateViewModels()
        {
            var gooseControls = _goosesModelService.GetGooseControlsOfDevice(_device);
            GooseControlViewModels = new ObservableCollection<GooseControlViewModel>(_gooseControlViewModelFactory.CreateGooseControlViewModel(_device, gooseControls));
        }


        public ObservableCollection<GooseControlViewModel> GooseControlViewModels
        {
            get => _gooseControlViewModels;
            set { SetProperty(ref _gooseControlViewModels, value); }
        }
        public ICommand DeleteGooseCommand { get; }

        public ICommand SaveCommand { get; }
        public override void OnActivate()
        {
            base.OnActivate();
        }

        public override void OnDeactivate()
        {
            base.OnDeactivate();
        }

        #endregion
    }
}
