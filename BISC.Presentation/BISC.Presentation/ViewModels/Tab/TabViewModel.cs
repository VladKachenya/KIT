using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Docking;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Interfaces;

namespace BISC.Presentation.ViewModels.Tab
{
   public class TabViewModel:ViewModelBase, ITabViewModel
    {
        private readonly ITabManagementService _tabManagementService;
        private string _tabRegionName;
        private string _tabHeader;


        public TabViewModel(ICommandFactory commandFactory,ITabManagementService tabManagementService)
        {
            _tabManagementService = tabManagementService;
            CloseFragmentCommand = commandFactory.CreatePresentationCommand((() =>
            {
                _tabManagementService.CloseTab(TabRegionName);
            }));
        }

        #region Implementation of ITabViewModel

        public string TabRegionName
        {
            get => _tabRegionName;
            set => SetProperty(ref _tabRegionName,value);
        }

        public string TabHeader
        {
            get => _tabHeader;
            set => SetProperty(ref _tabHeader,value);
        }

        public ICommand CloseFragmentCommand { get; }

        #endregion
    }
}
