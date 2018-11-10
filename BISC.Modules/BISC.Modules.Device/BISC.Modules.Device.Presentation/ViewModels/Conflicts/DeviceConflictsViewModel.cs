using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Modules.Device.Presentation.ViewModels.Conflicts
{
    public class DeviceConflictsViewModel : NavigationViewModelBase
    {
        public DeviceConflictsViewModel()
        {
            DeviceConflictViewModels=new ObservableCollection<DeviceConflictViewModel>();
        }

        public ObservableCollection<DeviceConflictViewModel> DeviceConflictViewModels { get; }

        #region Overrides of NavigationViewModelBase

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
        }

        #endregion
    }
}