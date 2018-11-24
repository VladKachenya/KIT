using System.Windows;
using BISC.Presentation.Infrastructure.ViewModel;

namespace BISC.Interfaces
{
    public interface IShellViewModel: IApplicationTitle
    {
        bool IsNotificationsExpanded { get; set; }
        GridLength ExpanderRowHeight { get; set; }
    }
}