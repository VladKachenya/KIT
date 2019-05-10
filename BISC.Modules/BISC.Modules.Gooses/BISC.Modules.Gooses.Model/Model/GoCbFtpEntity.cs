using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

namespace BISC.Modules.Gooses.Model.Model
{
  public  class GoCbFtpEntity:ModelElement, IGoCbFtpEntity
    {

        public GoCbFtpEntity()
        {
            ElementName = GooseKeys.GooseModelKeys.GoCbFtpEntityKey;
        }
        #region Implementation of IGoCbFtpEntity

        public int IndexOfGoose { get; set; }
        public string GoCbReference { get; set; }
        public string AppId { get; set; }
        public int? ConfRev { get; set; }
        #endregion
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is IGoCbFtpEntity)) return false;
            var element = obj as IGoCbFtpEntity;
            if (element.GoCbReference != GoCbReference) return false;
            if (element.AppId != AppId) return false;
            if (ConfRev.HasValue && element.ConfRev.HasValue && (element.ConfRev != ConfRev)) return false;
            return true;
        }

        public override string ToString()
        {
            return GoCbReference;
        }
    }
}
