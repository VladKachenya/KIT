using System.Collections.Generic;
using System.IO;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Keys;
using BISC.Modules.InformationModel.Infrastucture.Services;

namespace BISC.Modules.InformationModel.Model.Services
{
    public class DbValuesConfigurationParser : ConfigurationParser
    {
        private readonly IInfoModelService _infoModelService;

        public DbValuesConfigurationParser(IInfoModelService infoModelService)
        {
            _infoModelService = infoModelService;
        }

        protected override void WriteConfiguration(IEnumerable<IModelElement> modelElements, IDevice device, TextWriter streamTextWriter)
        {
            foreach (var modelElement in modelElements)
            {
                string valuePath = string.Empty;
                var parent = modelElement;
                var dai = (IDai)modelElement;
                do
                {
                    if (parent is IDai daiParent)
                    {
                        valuePath = valuePath + "$" + daiParent.Name;
                        parent = parent.ParentModelElement;
                        continue;
                    }

                    if (parent is ISdi sdi)
                    {
                        valuePath = "$" + sdi.Name + valuePath;
                        parent = parent.ParentModelElement;
                        continue;
                    }

                    if (parent is IDoi doi)
                    {
                        valuePath = "$" + doi.Name + valuePath;
                        parent = parent.ParentModelElement;
                        continue;
                    }

                    if (parent is ILogicalNode ln)
                    {
                        valuePath = "/" + ln.Prefix + ln.LnClass + ln.Inst + "$" + InformationModelKeys.DataAttributeHeaderKeys.dbFc + valuePath;
                        parent = parent.ParentModelElement;
                        continue;
                    }

                    if (parent is ILDevice ld)
                    {
                        streamTextWriter.WriteLine(ld.Inst + valuePath);
                        streamTextWriter.WriteLine(dai.Value.Value.Value);
                        break;
                    }
                } while (true);
            }
        }
    }
}