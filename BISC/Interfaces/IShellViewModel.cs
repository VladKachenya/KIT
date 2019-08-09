using System.Windows;
using System.Windows.Input;
using BISC.Presentation.Infrastructure.ViewModel;

namespace BISC.Interfaces
{
    public interface IShellViewModel: IApplicationTitle
    {
        bool IsNotificationsExpanded { get; set; }
        GridLength ExpanderRowHeight { get; set; }
        ICommand ShellClosingCommand { get; }
    }
}