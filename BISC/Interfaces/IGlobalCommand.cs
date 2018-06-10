using System.Windows.Input;

namespace BISC.Interfaces
{
    public interface IGlobalCommand
    {
        string CommandName { get; set; }
        ICommand Command { get; set; }
    }
}