using System.Collections.ObjectModel;

namespace BISC.Interfaces
{
    public interface IShellViewModel
    {
       ObservableCollection<IGlobalCommand> GlobalCommands { get; }
    }
}