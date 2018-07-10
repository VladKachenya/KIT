using System.Collections.Generic;

namespace BISC.Infrastructure.Global.Services
{
    public interface IConfigurationService
    {
        List<string> LastOpenedFiles { get; set; }
    }
}