using System.Collections.ObjectModel;
using BISC.Infrastructure.Global.Modularity;

namespace BISC.Presentation.Interfaces.Menu
{
    public interface IHamburgerMenuViewModel
    {
        ObservableCollection<IGlobalCommand> GlobalCommands { get; }
    }
}