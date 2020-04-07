using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Serializing;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Model.Model.Matrix;

namespace BISC.Modules.Gooses.Model.Services.ConfigurationServices
{
    public class GooseInputModelInfosConfigurationParser : ConfigurationParser
    {
        private readonly IModelElementsRegistryService _modelElementsRegistryService;

        public GooseInputModelInfosConfigurationParser(IModelElementsRegistryService modelElementsRegistryService)
        {
            _modelElementsRegistryService = modelElementsRegistryService;
        }

        protected override void WriteConfiguration(IEnumerable<IModelElement> modelElements, IDevice device, TextWriter streamTextWriter)
        {
            var gooseModelInfos = modelElements.Cast<IGooseInputModelInfo>();
            var gooseDeviceInput = new GooseDeviceInput();
            foreach (var gooseInputModelInfo in gooseModelInfos)
            {
                gooseDeviceInput.GooseInputModelInfoList.Add(gooseInputModelInfo);
            }
            streamTextWriter.WriteLine(_modelElementsRegistryService.SerializeModelElement(gooseDeviceInput, SerializingType.Extended).ToString());
        }
    }
}