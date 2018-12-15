using System.Collections.ObjectModel;
using System.Linq;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel.ChangeIpNetworkCard;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.Connection.Presentation.ViewModels.ChangeIpNetworkCard
{
    public class CustomIpSettingsViewModel : ViewModelBase, ICustomIpSettingsViewModel
    {
        #region private filds

        private string _settingsNamе;
        private ICustomNetworkCardSettingsViewModel _selectedNetworkCardSettingsViewModel;

        #endregion

        #region Ctor

        public CustomIpSettingsViewModel()
        {
            NetworkCardSettingsViewModels = new ObservableCollection<ICustomNetworkCardSettingsViewModel>();
        }

        #endregion

        #region Implementatoin of ICustomIpSettings

        public string SettingsNamе
        {
            get => _settingsNamе;
            set => SetProperty(ref _settingsNamе, value);
        }

        public ObservableCollection<ICustomNetworkCardSettingsViewModel> NetworkCardSettingsViewModels { get; }

        public ICustomNetworkCardSettingsViewModel SelectedNetworkCardSettingsViewModel
        {
            get
            {
                if (_selectedNetworkCardSettingsViewModel == null && NetworkCardSettingsViewModels.Count > 0)
                    SelectedNetworkCardSettingsViewModel = NetworkCardSettingsViewModels.First();
              return _selectedNetworkCardSettingsViewModel;
            }
            set => SetProperty(ref _selectedNetworkCardSettingsViewModel, value);
        }

        #endregion

    }
}