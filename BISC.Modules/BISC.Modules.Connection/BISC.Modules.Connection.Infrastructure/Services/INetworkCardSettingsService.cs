using System.Collections.Generic;

namespace BISC.Modules.Connection.Infrastructure.Services
{
    public interface INetworkCardSettingsService
    {
        List<string> GetNamesAvailableNetworkCards();
    }
}