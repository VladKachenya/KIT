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
   public class GooseQualityRowFtpEntitySerializer : DefaultModelElementSerializer<GooseRowQualityFtpEntity>
    {
        public GooseQualityRowFtpEntitySerializer()
        {
            RegisterProperty(nameof(GooseRowQualityFtpEntity.BitIndex), "bitIndex");
            RegisterProperty(nameof(GooseRowQualityFtpEntity.IndexOfGoose), "indexOfGoose");
            RegisterProperty(nameof(GooseRowQualityFtpEntity.NumberOfFcdaInDataSetOfGoose), "numberOfFcdaInDataSetOfGoose");
            RegisterProperty(nameof(GooseRowQualityFtpEntity.IsValiditySelected), "isValiditySelected");

        }

        public override IModelElement GetConcreteObject()
        {
            return new GooseRowQualityFtpEntity();
        }
    }
}
