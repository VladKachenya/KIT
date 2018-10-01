using System.Collections.ObjectModel;

namespace BISC.Interfaces
{
    public interface IShellViewModel
    {
        bool IsNotificationExpanded { get; set; }
        string ApplicationTitle { get; set; }
    }
}