using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Services;

namespace BISC.Modules.DataSets.Model.Services
{
   public class DatasetsConflictResolver: IElementConflictResolver
    {
        #region Implementation of IElementConflictResolver

        public string ConflictName => "DataSets";
        public bool GetIfConflictsExists(string deviceName, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
