using System.Collections.ObjectModel;
using System.Windows;

namespace BISC.Interfaces
{
    public interface IShellViewModel
    {
        bool IsNotificationsExpanded { get; set; }
        GridLength ExpanderRowHeight { get; set; }
        string ApplicationTitle { get; set; }
    }
}