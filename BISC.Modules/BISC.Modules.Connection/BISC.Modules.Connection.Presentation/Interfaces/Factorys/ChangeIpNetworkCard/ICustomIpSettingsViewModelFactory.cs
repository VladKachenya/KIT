using System.Collections.Generic;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel.ChangeIpNetworkCard;

namespace BISC.Modules.Connection.Presentation.Interfaces.Factorys.ChangeIpNetworkCard
{
    public interface ICustomIpSettingsViewModelFactory
    {
        ICustomIpSettingsViewModel CreateCustomIpSettingsViewModel(List<string> existingSettingsName, string nameBody = "NewIpSettings");
    }
}