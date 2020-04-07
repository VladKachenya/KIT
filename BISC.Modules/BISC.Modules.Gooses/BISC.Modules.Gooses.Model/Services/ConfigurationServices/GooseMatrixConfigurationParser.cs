using System.Collections.Generic;
using System.IO;
using System.Linq;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Factorys;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

namespace BISC.Modules.Gooses.Model.Services.ConfigurationServices
{
    public class GooseMatrixConfigurationParser : ConfigurationParser
    {
        private readonly IGooseMatrixParsersFactory _gooseMatrixParsersFactory;

        public GooseMatrixConfigurationParser(IGooseMatrixParsersFactory gooseMatrixParsersFactory)
        {
            _gooseMatrixParsersFactory = gooseMatrixParsersFactory;
        }
        protected override void WriteConfiguration(IEnumerable<IModelElement> modelElements, IDevice device, TextWriter streamTextWriter)
        {
            var gooseMatrix = modelElements.First() as IGooseMatrixFtp;
            _gooseMatrixParsersFactory.GetGooseMatrixParser(device)
                .GetFileStringFromMatrixModel(gooseMatrix, streamTextWriter);
        }
    }
}