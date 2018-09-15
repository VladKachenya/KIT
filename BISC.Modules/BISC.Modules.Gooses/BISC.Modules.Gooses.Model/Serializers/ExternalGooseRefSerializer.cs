using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Model.Model;

namespace BISC.Modules.Gooses.Model.Serializers
{
   public class ExternalGooseRefSerializer:DefaultModelElementSerializer<IExternalGooseRef>
    {
        public ExternalGooseRefSerializer()
        {
            RegisterProperty(nameof(IExternalGooseRef.DaName),"daName");
            RegisterProperty(nameof(IExternalGooseRef.DoName), "doName");
            RegisterProperty(nameof(IExternalGooseRef.LnClass), "lnClass");
            RegisterProperty(nameof(IExternalGooseRef.LnInst), "lnInst");
            RegisterProperty(nameof(IExternalGooseRef.Prefix), "prefix");
            RegisterProperty(nameof(IExternalGooseRef.IedName), "iedName");
            RegisterProperty(nameof(IExternalGooseRef.LdInst), "ldInst");

        }

        #region Overrides of DefaultModelElementSerializer<IExternalGooseRef>

        public override IModelElement GetConcreteObject()
        {
            return new ExternalGooseRef();
        }

        #endregion
    }

}
