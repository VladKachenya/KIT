using System.Collections.Generic;

namespace BISC.Infrastructure.Global.Services
{
    public interface IConfigurationService
    {
        List<string> LastOpenedFiles { get; set; }

        List<string> LastIpAddresses { get; set; }
        List<string> LastConnectedIpAddresses { get; set; }
        List<string> GetIpsCollection(string key);
        void SetIpsCollection(string key, List<string> setCollection);

        string LastProjectPath { get; set; }
        bool IsUserLoggingEnabled { get; set; }

        bool IsAutoEnabledValidityInGooseReceiving { get; set; }
        bool IsAutoEnabledQualityInGooseReceiving { get; set; }
        int MmsQueryDelay { get; set; }

        int FtpTimeOutDelay { get; set; }

        int MaxResponseTime { get; set; }
    }
}