using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Modularity;
using BISC.Infrastructure.Global.Services;
using BISC.Presentation.Interfaces.Menu;

namespace BISC.Presentation.ViewModels
{
   public class HamburgerMenuViewModel: IHamburgerMenuViewModel
    {
        public HamburgerMenuViewModel(IUserInterfaceComposingService userInterfaceComposingService)
        {
            GlobalCommands = userInterfaceComposingService.GetMenuCommands();
            GlobalCommandGroups = userInterfaceComposingService.GetToolBarCommandGroups();

        }
        public ObservableCollection<IGlobalCommand> GlobalCommands { get; }

        public ObservableCollection<IGlobalCommandGroup> GlobalCommandGroups { get; }

    }
}
