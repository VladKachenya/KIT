using System.Collections.ObjectModel;

namespace BISC.Modules.Connection.Presentation.Interfaces.ViewModel.ChangeIpNetworkCard
{
    public interface ICustomIpSettingsViewModel
    {
        string SettingsNamе { get; set; }

        ObservableCollection<ICustomNetworkCardSettingsViewModel> NetworkCardSettingsViewModels { get; }
        ICustomNetworkCardSettingsViewModel SelectedNetworkCardSettingsViewModel { get; set; }
    }
}