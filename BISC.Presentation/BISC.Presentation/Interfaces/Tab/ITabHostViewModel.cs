using System.Collections.ObjectModel;

namespace BISC.Presentation.Interfaces
{
    public interface ITabHostViewModel
    {
        ObservableCollection<ITabViewModel> TabViewModels { get; }
        ITabViewModel ActiveTabViewModel { get; set; }
    }
}