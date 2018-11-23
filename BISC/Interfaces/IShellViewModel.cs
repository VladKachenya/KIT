using BISC.Infrastructure.Shell.ViewModels;
using System.Windows;

namespace BISC.Interfaces
{
    public interface IShellViewModel: IApplicationTitle
    {
        bool IsNotificationsExpanded { get; set; }
        GridLength ExpanderRowHeight { get; set; }
    }
}