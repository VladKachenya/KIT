using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

namespace BISC.Modules.Gooses.Model.Model
{
   public class GooseRowQualityFtpEntity:GooseRowFtpEntity, IGooseRowQualityFtpEntity
    {

        public GooseRowQualityFtpEntity()
        {
            ElementName = GooseKeys.GooseModelKeys.GooseRowQualityFtpEntityKey;
        }
        #region Implementation of IGooseRowQualityFtpEntity

        public bool IsValiditySelected { get; set; }

        #endregion
        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is IGooseRowQualityFtpEntity)) return false;
            var element = obj as IGooseRowQualityFtpEntity;
            if (element.IsValiditySelected != IsValiditySelected) return false;
            return true;
        }
    }
}
