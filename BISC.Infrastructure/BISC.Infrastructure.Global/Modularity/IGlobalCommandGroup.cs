using System.Collections.ObjectModel;

namespace BISC.Infrastructure.Global.Modularity
{
    public interface IGlobalCommandGroup
    {
        ObservableCollection<IGlobalCommand> GlobalCommandsGroup { get; }
        string CommandsName { get; set; }
        string IconId { get; set; }
    }
}