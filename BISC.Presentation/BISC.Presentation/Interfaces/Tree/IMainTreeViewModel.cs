using System.Collections.ObjectModel;
using BISC.Presentation.Infrastructure.Tree;

namespace BISC.Presentation.Interfaces.Tree
{
    public interface IMainTreeViewModel
    {
        ObservableCollection<IMainTreeItem> MainTreeItems { get; }
    }
}