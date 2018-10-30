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
   public class GooseControl:ModelElement, IGooseControl
    {
        public GooseControl()
        {
            ElementName = GooseKeys.GooseModelKeys.GooseControlKey;
        }


        #region Implementation of IGooseControl

        public string Name { get; set; }
        public string DataSet { get; set; }
        public int ConfRev { get; set; }
        public string AppId { get; set; }
        public bool IsDynamic { get; set; }
        public bool FixedOffs { get; set; }
        public string GooseType { get; set; }
        public ChildModelsList<ISubscriberDevice> SubscriberDevice=>new ChildModelsList<ISubscriberDevice>(this, GooseKeys.GooseModelKeys.SubscriberDeviceKey);
        #endregion

        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is IGooseControl)) return false;
            var element = obj as IGooseControl;
            if (element.Name != Name) return false;
            if (element.DataSet != DataSet) return false;
            if (element.ConfRev != ConfRev) return false;
            if (element.AppId != AppId) return false;
            if (element.IsDynamic != IsDynamic) return false;

            return true;
        }
    }
}
