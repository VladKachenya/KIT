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

        #endregion
    }
}
