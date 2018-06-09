using System.Windows.Input;

namespace BISC.Presentation.Infrastructure.Tree.TreeCommands
{
    public interface ITreeCommand
    {
        string TreeCommandName { get; set; }
        ICommand TreeCommand { get; set; }
    }
}