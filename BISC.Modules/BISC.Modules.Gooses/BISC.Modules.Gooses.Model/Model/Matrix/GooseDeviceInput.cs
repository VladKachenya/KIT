using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

namespace BISC.Modules.Gooses.Model.Model.Matrix
{
    public class GooseDeviceInput : ModelElement, IGooseDeviceInput
    {
        public GooseDeviceInput()
        {
            ElementName = GooseKeys.GooseModelKeys.GooseDeviceInputKey;
        }

        #region Implementation of IGooseDeviceInput

        public string DeviceOwnerName { get; set; }
        public ChildModelsList<IGooseInputModelInfo> GooseInputModelInfoList => new ChildModelsList<IGooseInputModelInfo>(this, GooseKeys.GooseModelKeys.GooseInputModelInfoKey);
        public ChildModelProperty<IGooseMatrixFtp> GooseMatrix => new ChildModelProperty<IGooseMatrixFtp>(this, GooseKeys.GooseModelKeys.GooseMatrixFtpKey);

        #endregion
    }
}
