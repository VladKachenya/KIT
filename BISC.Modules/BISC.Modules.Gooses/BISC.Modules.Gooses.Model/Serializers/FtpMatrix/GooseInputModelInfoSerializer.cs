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
	public class GooseInputModelInfoSerializer:DefaultModelElementSerializer<IGooseInputModelInfo>
	{
		public GooseInputModelInfoSerializer()
		{
			RegisterProperty(nameof(GooseInputModelInfo.EmittingDeviceName), "emittingDeviceName");
		    RegisterProperty(nameof(GooseInputModelInfo.GocbRef), "gocbRef");
        }

        public override IModelElement GetConcreteObject()
		{
			return new GooseInputModelInfo();
		}
	}
}
