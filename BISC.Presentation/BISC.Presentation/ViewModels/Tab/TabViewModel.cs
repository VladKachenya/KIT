using BISC.Infrastructure.Global.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Events;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Interfaces;
using System.Windows.Input;

namespace BISC.Presentation.ViewModels.Tab
{
    public class TabViewModel : ViewModelBase, ITabViewModel
    {
        private readonly ITabManagementService _tabManagementService;
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly IGlobalSavingService _globalSavingService;
        private string _tabRegionName;
        private string _tabHeader;
        private bool _isHaveChanges;


        public TabViewModel(ICommandFactory commandFactory, ITabManagementService tabManagementService, ISaveCheckingService saveCheckingService,
            IGlobalEventsService globalEventsService, IGlobalSavingService globalSavingService)
        {
            _tabManagementService = tabManagementService;
            _saveCheckingService = saveCheckingService;
            _globalEventsService = globalEventsService;
            _globalSavingService = globalSavingService;
            CloseFragmentCommand = commandFactory.CreatePresentationCommand((async () =>
            {
                if (!(await _globalSavingService.SaveСhangesToRegion(_tabRegionName, true)).IsCancelled)
                {
                    _tabManagementService.CloseTab(TabRegionName);
                }
            }));
            SaveChangesCommand = commandFactory.CreatePresentationCommand((async () =>
            {
                await _globalSavingService.SaveСhangesToRegion(_tabRegionName, true);
            }));
            _globalEventsService.Subscribe<SaveCheckEvent>(OnSaveCheck);
        }

        private async void OnSaveCheck(SaveCheckEvent saveCheckEvent)
        {
            IsHaveChanges = !await _saveCheckingService.GetIsRegionSaved(TabRegionName);
        }

        #region Implementation of ITabViewModel

        public string TabRegionName
        {
            get => _tabRegionName;
            set => SetProperty(ref _tabRegionName, value, true);
        }

        public string TabHeader
        {
            get => _tabHeader;
            set => SetProperty(ref _tabHeader, value, true);
        }

        public bool IsHaveChanges
        {
            get => _isHaveChanges;
            set { SetProperty(ref _isHaveChanges, value, true); }
        }

        public ICommand CloseFragmentCommand { get; }
        public ICommand SaveChangesCommand { get; }

        #region Overrides of ViewModelBase

        protected override void OnDisposing()
        {
            _globalEventsService.Unsubscribe<SaveCheckEvent>(OnSaveCheck);

            base.OnDisposing();

        }

        #endregion

        #endregion
    }
}
