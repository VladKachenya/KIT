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
        ObservableCollection<ICustomIpSettingsViewModel> CustomIpSettingsViewModels { get; }
        ICustomIpSettingsViewModel SellectedCustomIpSettingsViewModel { get; set; }


        ICommand CloseCommand { get; }
        ICommand AddNewCustomIpSettingsCommand { get; }
        ICommand RemoveCustomIpSettingsCommand { get; }



    }
}
