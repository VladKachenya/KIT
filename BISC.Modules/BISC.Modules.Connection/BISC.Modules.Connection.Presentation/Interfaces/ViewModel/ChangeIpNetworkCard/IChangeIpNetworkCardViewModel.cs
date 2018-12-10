using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.Connection.Presentation.Interfaces.ViewModel.ChangeIpNetworkCard
{
    public interface IChangeIpNetworkCardViewModel
    {

        ObservableCollection<ICurrentCardConfigurationViewModel> CurrentCardConfigurationViewModels { get; set; }
        ICurrentCardConfigurationViewModel SellectedCardConfigurationViewModel { get; set; }
        ObservableCollection<INetworkCardSettingsViewModel> NetworkCardSettingsViewModels { get; }
        INetworkCardSettingsViewModel SellectedNetworkCardSettingsViewModel { get; set; }

        ICommand CloseCommand { get; }
        ICommand AddNewNetworkCardSettingsViewModelCommand { get; }
        ICommand RemoveNetworkCardSettingsViewModelCommand { get; }



    }
}
