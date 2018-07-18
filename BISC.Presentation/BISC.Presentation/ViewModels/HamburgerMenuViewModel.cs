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
        private readonly IUserInterfaceComposingService _userInterfaceComposingService;

        public HamburgerMenuViewModel(IUserInterfaceComposingService userInterfaceComposingService)
        {
            _userInterfaceComposingService = userInterfaceComposingService;
            GlobalCommands = _userInterfaceComposingService.GetAllCommands();
        }
        public ObservableCollection<IGlobalCommand> GlobalCommands { get; }
    }
}
