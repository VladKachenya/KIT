using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public abstract class ConfigurationParser : IConfigurationParser
    {
        public OperationResult<string> GetConfiguration(IEnumerable<IModelElement> modelElements, IDevice device) 
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string configuration;
                using (TextWriter streamWriter = new StringWriter(sb))
                {
                    WriteConfiguration(modelElements, device, streamWriter);
                    configuration = streamWriter.ToString();
                }
                return new OperationResult<string>(configuration, true);
            }
            catch (Exception e)
            {
                return new OperationResult<string>(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        protected abstract void WriteConfiguration(IEnumerable<IModelElement> modelElements, IDevice device, TextWriter streamTextWriter);
    }
}