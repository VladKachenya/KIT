using System;
using System.Collections.ObjectModel;
using BISC.Presentation.Infrastructure.ChangeTracker;
using BISC.Presentation.Infrastructure.Tree;

namespace BISC.Presentation.Interfaces.Tree
{
    public interface IMainTreeViewModel:IObjectWithChangeTracker
    {
        ObservableCollection<ITreeItemViewModel> ChildItemViewModels { get; }
    }
}