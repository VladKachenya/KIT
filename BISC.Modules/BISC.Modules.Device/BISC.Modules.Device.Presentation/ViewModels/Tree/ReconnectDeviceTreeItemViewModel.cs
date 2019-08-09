using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Modules.Device.Presentation.ViewModels.Tree
{
    public class ReconnectDeviceTreeItemViewModel:NavigationViewModelBase
    {
        public ReconnectDeviceTreeItemViewModel()
            : base(null)
        {
            
        }

        #region Overrides of NavigationViewModelBase

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
        }

        #endregion
    }
}
