using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Model.Model;
using BISC.Modules.Gooses.Model.Model.Matrix;

namespace BISC.Modules.Gooses.Model.Serializers.FtpMatrix
{
   public class GoCbFtpEntitySerializer:DefaultModelElementSerializer<GoCbFtpEntity>
    {
        public GoCbFtpEntitySerializer()
        {
            RegisterProperty(nameof(GoCbFtpEntity.IndexOfGoose), "indexOfGoose");
            RegisterProperty(nameof(GoCbFtpEntity.GoCbReference), "goCbReference");
            RegisterProperty(nameof(GoCbFtpEntity.AppId), "appId");
            RegisterProperty(nameof(GoCbFtpEntity.ConfRev), "confRev");

        }

        public override IModelElement GetConcreteObject()
        {
            return new GoCbFtpEntity();
        }
    }
}
