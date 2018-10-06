using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Docking;
using BISC.Presentation.Infrastructure.Events;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Interfaces;

namespace BISC.Presentation.ViewModels.Tab
{
    public class TabViewModel : ViewModelBase, ITabViewModel
    {
        private readonly ITabManagementService _tabManagementService;
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly IGlobalEventsService _globalEventsService;
        private string _tabRegionName;
        private string _tabHeader;
        private bool _isHaveChanges;


        public TabViewModel(ICommandFactory commandFactory, ITabManagementService tabManagementService, ISaveCheckingService saveCheckingService, IGlobalEventsService globalEventsService)
        {
            _tabManagementService = tabManagementService;
            _saveCheckingService = saveCheckingService;
            _globalEventsService = globalEventsService;
            CloseFragmentCommand = commandFactory.CreatePresentationCommand((async () =>
            {
                if (await saveCheckingService.GetIsRegionCanBeClosed(_tabRegionName))
                {
                    _tabManagementService.CloseTab(TabRegionName);
                }
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

        #endregion
    }
}
