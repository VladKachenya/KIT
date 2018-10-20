using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Model;

namespace BISC.Modules.Gooses.Model.Model
{
  public  class ExternalGooseRef:ModelElement,IExternalGooseRef
    {
        public ExternalGooseRef()
        {
            ElementName = GooseKeys.GooseModelKeys.ExternalGooseRefKey;
        }

        #region Implementation of IExternalGooseRef

        public string IedName { get; set; }
        public string LdInst { get; set; }
        public string Prefix { get; set; }
        public string LnClass { get; set; }
        public string LnInst { get; set; }
        public string DoName { get; set; }
        public string DaName { get; set; }
        public string AsString()
        {
            return IedName +"/"+ LdInst+"/"+
                   Prefix + LnClass + LnInst +"/"+
                   DoName + "." + DaName;
        }

        #endregion
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is IExternalGooseRef)) return false;
            var element = obj as IExternalGooseRef;
            if (element.AsString() != AsString()) return false;
            return true;
        }
    }
}
