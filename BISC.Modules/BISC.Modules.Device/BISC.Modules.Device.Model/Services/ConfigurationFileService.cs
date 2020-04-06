using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.IoC;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;

namespace BISC.Modules.Device.Model.Services
{
    public class ConfigurationFileService : IConfigurationFileService
    {
        private readonly IInjectionContainer _injectionContainer;

        public ConfigurationFileService(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }
        public async Task<OperationResult> SaveConfigurationToFile(ISclModel sclModel, IDevice device, string path)
        {
            try
            {
                var configGetters = _injectionContainer.ResolveAll(typeof(IConfigurableModelElementsGetter));
                foreach (IConfigurableModelElementsGetter getter in configGetters)
                {
                    var configuration = getter.ModuleName + '\n';
                    var parsingRes = _injectionContainer.ResolveType<IConfigurationParser>(getter.ModuleName)
                        .GetConfiguration(getter.GetConfigurableModelElements(device.DeviceGuid, sclModel));
                    configuration += parsingRes.Item;
                    using (StreamWriter sw = new StreamWriter(path, true, Encoding.Default))
                    {
                        await sw.WriteAsync(configuration);
                    }
                }
            }
            catch (Exception e)
            {
                return new OperationResult(e.Message);
            }

            return OperationResult.SucceedResult;
        }
    }
}