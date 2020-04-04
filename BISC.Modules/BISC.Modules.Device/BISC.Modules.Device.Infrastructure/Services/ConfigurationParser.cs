using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BISC.Infrastructure.Global.Common;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public abstract class ConfigurationParser : IConfigurationParser
    {
        public OperationResult<string> GetConfiguration(IEnumerable<IModelElement> modelElements) 
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                TextWriter streamWriter = new StringWriter(sb);
                WriteConfiguration(modelElements, streamWriter);
                return new OperationResult<string>(streamWriter.ToString(), true) ;
            }
            catch (Exception e)
            {
                return new OperationResult<string>(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        protected abstract void WriteConfiguration(IEnumerable<IModelElement> modelElements, TextWriter streamTextWriter);
    }
}