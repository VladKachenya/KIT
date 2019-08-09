using System;
using System.Collections.ObjectModel;
using BISC.Presentation.Infrastructure.Tree;

namespace BISC.Presentation.Interfaces.Tree
{
    public interface ITreeItemViewModel
    {
        ObservableCollection<ITreeItemViewModel> ChildItemViewModels { get; }
        Guid DynamicRegionId { get; set; }
        bool IsSelected { get; set; }
        bool IsExpanded { get; set; }
    }

}