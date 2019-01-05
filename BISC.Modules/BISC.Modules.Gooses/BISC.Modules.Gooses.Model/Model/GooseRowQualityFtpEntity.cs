using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
