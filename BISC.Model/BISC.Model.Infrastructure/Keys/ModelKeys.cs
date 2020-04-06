using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Model.Infrastructure.Keys
{
    public static class InfrastructureKeys
    {
        public static class ModelKeys
        {
            public static string BiscProjectKey => "BiscProject";
            public static string SclModelKey => "SCL";
            public static string CustomElementsKey => "CustomElements";

            public static string CommunicationModelKey => "Communication";
            public static string DurationInMillisecMinTimeKey => "MinTime";
            public static string DurationInMillisecMaxTimeKey => "MaxTime";
            public static string GseKey => "GSE";
            public static string SubNetworkKey => "SubNetwork";
            public static string ConnectedAccessPointKey => "ConnectedAP";
            public static string AddressPropertyKey => "P";
            public static string SclAddressKey => "Address";
        }

        public static class ModulesKeys
        {
            public static string DataSetModule = nameof(DataSetModule);
            public static string ReportModule = nameof(ReportModule);

        }
    }
}
