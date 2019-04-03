using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Presentation.Infrastructure.Services;
using System.Linq;

namespace BISC.Modules.Gooses.Model.Model.Matrix
{
    public class GooseMatrixFtp : ModelElement, IGooseMatrixFtp
    {
        public GooseMatrixFtp()
        {
            ElementName = GooseKeys.GooseModelKeys.GooseMatrixFtpKey;
        }

        public ChildModelsList<IMacAddressEntity> MacAddressList => new ChildModelsList<IMacAddressEntity>(this, GooseKeys.GooseModelKeys.MacAddressEntityKey);
        public ChildModelsList<IGoCbFtpEntity> GoCbFtpEntities => new ChildModelsList<IGoCbFtpEntity>(this, GooseKeys.GooseModelKeys.GoCbFtpEntityKey);
        public ChildModelsList<IGooseRowFtpEntity> GooseRowFtpEntities => new ChildModelsList<IGooseRowFtpEntity>(this, GooseKeys.GooseModelKeys.GooseRowFtpEntityKey);
        public ChildModelsList<IGooseRowQualityFtpEntity> GooseRowQualityFtpEntities => new ChildModelsList<IGooseRowQualityFtpEntity>(this, GooseKeys.GooseModelKeys.GooseRowQualityFtpEntityKey);

        #region private methods

        
        #endregion


    }
}