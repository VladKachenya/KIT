using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

namespace BISC.Modules.Gooses.Model.Model
{
   public class GooseRowFtpEntity:ModelElement, IGooseRowFtpEntity
    {
        public GooseRowFtpEntity()
        {
            ElementName = GooseKeys.GooseModelKeys.GooseRowFtpEntityKey;
        }


        #region Implementation of IGooseRowFtpEntity

        public int IndexOfGoose { get; set; }
        public int NumberOfFcdaInDataSetOfGoose { get; set; }
        public int BitIndex { get; set; }

        #endregion
    }
}
