using System.Windows.Input;
using BISC.Presentation.Infrastructure.Tree;

namespace BISC.Modules.Device.Presentation.Interfaces
{
    public interface IDeviceTreeItemViewModel:IMainTreeItem
    {
        ICommand NavigateToDetailsCommand { get; set; }
    }
}