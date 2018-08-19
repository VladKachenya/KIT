using System.Windows.Input;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.InformationModel.Presentation.Interfaces
{
    public interface IInfoModelTreeItemViewModel
    {
        ICommand NavigateToDetailsCommand { get; }
    }
}