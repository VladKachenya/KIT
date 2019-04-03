using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Model.Model.Matrix;

namespace BISC.Modules.Gooses.Model.Serializers.FtpMatrix
{
    public class GooseDeviceInputSerializer:DefaultModelElementSerializer<IGooseDeviceInput>
    {
        public GooseDeviceInputSerializer()
        {
            RegisterProperty(nameof(GooseDeviceInput.DeviceOwnerName), "deviceOwnerName");
        }

        #region Overrides of DefaultModelElementSerializer<IGooseDeviceInput>

        public override IModelElement GetConcreteObject()
        {
            return new GooseDeviceInput();
        }

        #endregion
    }
}
