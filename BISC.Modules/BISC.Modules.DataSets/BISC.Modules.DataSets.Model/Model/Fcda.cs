using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Modules.DataSets.Infrastructure.Keys;
using BISC.Modules.DataSets.Infrastructure.Model;

namespace BISC.Modules.DataSets.Model.Model
{
   public class Fcda:ModelElement,IFcda
    {
        public Fcda()
        {
            ElementName = DatasetKeys.DatasetModelKeys.FcdaModelKey;
        }
        #region Implementation of IFcda

        public string LdInst { get; set; }
        public string Prefix { get; set; }
        public string LnClass { get; set; }
        public string LnInst { get; set; }
        public string DoName { get; set; }
        public string DaName { get; set; }
        public string Fc { get; set; }

        #endregion
    }
}
