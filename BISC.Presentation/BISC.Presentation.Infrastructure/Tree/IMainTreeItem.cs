using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Remoting.Contexts;
using System.Windows.Input;
using BISC.Presentation.Infrastructure.Tree.TreeCommands;

namespace BISC.Presentation.Infrastructure.Tree
{
    public interface IMainTreeItem
    {
        string TreeItemName { get; set; }
        ObservableCollection<IMainTreeItem> ChildTreeItems { get; }
    }
}