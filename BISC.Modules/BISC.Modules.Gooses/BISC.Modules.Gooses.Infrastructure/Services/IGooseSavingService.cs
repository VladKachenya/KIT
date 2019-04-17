using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

namespace BISC.Modules.Gooses.Infrastructure.Services
{
    public interface IGooseSavingService
    {
        Task SaveSubscriptionGooces(IDevice subscribingDevice, List<IGooseInputModelInfo> acceptedGooses);
        Task SaveSubscriptionMatrix(IDevice device, List<Tuple<IGoCbFtpEntity, IGooseRowFtpEntity>> subscriptionEntity);

    }
}