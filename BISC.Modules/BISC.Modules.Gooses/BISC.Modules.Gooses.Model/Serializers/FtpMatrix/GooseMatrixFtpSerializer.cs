using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Model.Model.Matrix;

namespace BISC.Modules.Gooses.Model.Serializers.FtpMatrix
{
  public  class GooseMatrixFtpSerializer:DefaultModelElementSerializer<GooseMatrixFtp>
    {
        public GooseMatrixFtpSerializer()
        {
            //RegisterProperty(nameof(GooseMatrixFtp.DeviceOwnerName), "deviceOwnerName");
        }
        
        public override IModelElement GetConcreteObject()
        {
            return new GooseMatrixFtp();
        }
    }
}
