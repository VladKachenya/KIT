using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Model.Model;

namespace BISC.Modules.DataSets.Model.Serializers
{
   public class FcdaSerializer:DefaultModelElementSerializer<IFcda>
    {
        
        public FcdaSerializer()
        {
            RegisterProperty(nameof(IFcda.LdInst),"ldInst");
            RegisterProperty(nameof(IFcda.Prefix), "prefix");
            RegisterProperty(nameof(IFcda.LnClass), "lnClass");
            RegisterProperty(nameof(IFcda.LnInst), "lnInst");
            RegisterProperty(nameof(IFcda.DoName), "doName");
            RegisterProperty(nameof(IFcda.DaName), "daName");
            RegisterProperty(nameof(IFcda.Fc), "fc");
        }

        #region Overrides of DefaultModelElementSerializer<IFcda>

        public override IModelElement GetConcreteObject()
        {
            return new Fcda();
        }

        #endregion
    }
}
