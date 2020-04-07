using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Factorys;
using BISC.Modules.Gooses.Infrastructure.Model;

namespace BISC.Modules.Gooses.Model.Services.ConfigurationServices
{
    public class GooseConfigurationParser : ConfigurationParser
    {
        private readonly IGooseControlToGooseFtpDotsConverter _gooseFtpDotsConverter;

        public GooseConfigurationParser(IGooseControlToGooseFtpDotsConverter gooseFtpDotsConverter)
        {
            _gooseFtpDotsConverter = gooseFtpDotsConverter;
        }
        protected override void WriteConfiguration(IEnumerable<IModelElement> modelElements, TextWriter streamTextWriter)
        {
            if(!modelElements.Any()) return;
            var gooseControlToParse = modelElements.Cast<IGooseControl>();
            var device = gooseControlToParse.First().GetFirstParentOfType<IDevice>();
            var gooseDtosToParse = _gooseFtpDotsConverter.ConvertToDots(gooseControlToParse);
            streamTextWriter.WriteLine($"Dev({device.Type})");
            foreach (var gooseDtoObj in gooseDtosToParse)
            {
                string ld = gooseDtoObj.LdInst;
                string goCBName = gooseDtoObj.Name;
                string goId = gooseDtoObj.GoId;
                string dataSet = gooseDtoObj.SelectedDataset;
                uint confRev = (uint)gooseDtoObj.ConfRev;
                string fixedOffs = gooseDtoObj.FixedOffs ? "1" : "0";
                string minTime = gooseDtoObj.MinTime.ToString();
                string maxTime = gooseDtoObj.MaxTime.ToString();
                string VlanPriority = gooseDtoObj.VlanPriority.ToString();
                string VlanId = gooseDtoObj.VlanId.ToString();
                string AppId = gooseDtoObj.AppId.ToString();
                string MacAddress = gooseDtoObj.MacAddress.Replace("-", String.Empty);

                streamTextWriter.WriteLine(
                    $"GoCB({ld} {goCBName} {goId} {dataSet} {confRev} {fixedOffs} {minTime} {maxTime})");
                streamTextWriter.WriteLine($"GoDA({VlanPriority} {VlanId} {AppId} {MacAddress})");
                streamTextWriter.WriteLine();
            }
        }
    }
}