using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Model.Model;

namespace BISC.Modules.Gooses.Model.Serializers.FtpMatrix
{
    public class GooseRowFtpEntitySerializer : DefaultModelElementSerializer<GooseRowFtpEntity>
    {
        public GooseRowFtpEntitySerializer()
        {
            RegisterProperty(nameof(GooseRowFtpEntity.BitIndex), "bitIndex");
            RegisterProperty(nameof(GooseRowFtpEntity.IndexOfGoose), "indexOfGoose");
            RegisterProperty(nameof(GooseRowFtpEntity.NumberOfFcdaInDataSetOfGoose), "numberOfFcdaInDataSetOfGoose");
        }

        public override IModelElement GetConcreteObject()
        {
            return new GooseRowFtpEntity();
        }
    }
}
