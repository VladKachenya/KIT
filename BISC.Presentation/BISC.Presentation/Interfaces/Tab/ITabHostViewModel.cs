using System.Collections.ObjectModel;
using BISC.Presentation.Infrastructure.ViewModel;

namespace BISC.Presentation.Interfaces
{
    public interface ITabHostViewModel
    {
        ObservableCollection<ITabViewModel> TabViewModels { get; }
        ITabViewModel ActiveTabViewModel { get; set; }
    }
}