using System.Collections.Generic;

namespace BISC.Infrastructure.Global.Services
{
    public interface IConfigurationService
    {
        List<string> LastOpenedFiles { get; set; }

        List<string> LastIpAddresses { get; set; }
        List<string> LastConnectedIpAddresses { get; set; }
        string LastProjectPath { get; set; }
    }
}