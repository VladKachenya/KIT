using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

namespace BISC.Modules.Gooses.Model.Model.Matrix
{
	public class GooseMatrixFtp :ModelElement, IGooseMatrixFtp
	{
		public GooseMatrixFtp()
		{
			ElementName = GooseKeys.GooseModelKeys.GooseMatrixFtpKey;
		}
		public string DeviceOwnerName { get; set; }
		public ChildModelsList<IMacAddressEntity> MacAddressList =>new ChildModelsList<IMacAddressEntity>(this,GooseKeys.GooseModelKeys.MacAddressEntityKey);
		public ChildModelsList<IGoCbFtpEntity> GoCbFtpEntities =>new ChildModelsList<IGoCbFtpEntity>(this,GooseKeys.GooseModelKeys.GoCbFtpEntityKey);
		public ChildModelsList<IGooseRowFtpEntity> GooseRowFtpEntities =>new ChildModelsList<IGooseRowFtpEntity>(this,GooseKeys.GooseModelKeys.GooseRowFtpEntityKey);
	    public ChildModelsList<IGooseRowQualityFtpEntity> GooseRowQualityFtpEntities => new ChildModelsList<IGooseRowQualityFtpEntity>(this, GooseKeys.GooseModelKeys.GooseRowQualityFtpEntityKey);
    }
}