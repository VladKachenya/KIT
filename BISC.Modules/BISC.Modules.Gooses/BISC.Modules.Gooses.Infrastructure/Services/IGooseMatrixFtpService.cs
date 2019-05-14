using System;
using System.Collections.Generic;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

namespace BISC.Modules.Gooses.Infrastructure.Services
{
    public interface IGooseMatrixFtpService
    {
        void DeleteGooseSubscription(IGooseMatrixFtp gooseMatrixFtp, IGoCbFtpEntity goCd);
        void RegularizeMatrix(IGooseMatrixFtp gooseMatrixFtp);
        void AddMacAddressToMatrix(IGooseMatrixFtp gooseMatrixFtp, string macAddress);

        /// <summary>
        /// Adding gooseEntity to matrix if need.
        /// You also need to add mac address throw "AddMacAddressToMatrix" method.
        /// </summary>
        /// <param name="gooseMatrixFtp"></param>
        /// <param name="goCdRef"></param>
        /// <param name="goAppId"></param>
        /// <param name="confRev"></param>
        void AddGooseCdFtpEntityToMatrix(IGooseMatrixFtp gooseMatrixFtp, string goCdRef, string goAppId, int confRev);

        IGooseMatrixFtp GetGooseMatrixFtpForDevice(IDevice device, IBiscProject biscProject = null);

        void SetGooseMatrixFtpForDevice(IDevice device, IGooseMatrixFtp gooseMatrixFtp, IBiscProject biscProject = null);

        void SetSubscriptionRowsToMatrix(IGooseMatrixFtp gooseMatrixFtp,
            List<Tuple<IGoCbFtpEntity, IGooseRowFtpEntity>> subscriptionEntity);

        List<IGooseRowFtpEntity> GetAllRowEntitiesOfGoCd(IGooseMatrixFtp gooseMatrixFtp, IGoCbFtpEntity goCbFtpEntity);

    }
}