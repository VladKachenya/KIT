using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Modularity;
using BISC.Infrastructure.Global.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Interfaces.Menu;

namespace BISC.Presentation.ViewModels
{
   public class ToolBarMenuViewModel:ViewModelBase, IToolBarMenuViewModel
    {
        private readonly IUserInterfaceComposingService _userInterfaceComposingService;

        public ToolBarMenuViewModel(IUserInterfaceComposingService userInterfaceComposingService)
        {
            _userInterfaceComposingService = userInterfaceComposingService;
            GlobalCommands = _userInterfaceComposingService.GetToolBarCommands();
        }
        public ObservableCollection<IGlobalCommand> GlobalCommands { get; }

    }
}
