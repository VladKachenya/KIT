using System.Windows.Input;

namespace BISC.Infrastructure.Global.Modularity
{
    public interface IGlobalCommand
    {
        string CommandName { get; set; }
        ICommand Command { get; set; }
        string IconId { get; set; }
    }
}