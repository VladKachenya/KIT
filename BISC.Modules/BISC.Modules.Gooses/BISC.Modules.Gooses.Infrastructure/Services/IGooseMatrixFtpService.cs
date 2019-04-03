using System;
using System.Collections.Generic;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

namespace BISC.Modules.Gooses.Infrastructure.Services
{
    public interface IGooseMatrixFtpService
    {
        void DeleteGooseSubscription(IGooseMatrixFtp gooseMatrixFtp, string goCdReference);
        void RegularizeMatrix(IGooseMatrixFtp gooseMatrixFtp);
        void AddMacAddressToMatrix(IGooseMatrixFtp gooseMatrixFtp, string macAddress);

        /// <summary>
        /// Adding gooseEntity to matrix if need.
        /// You also need to add mac address throw "AddMacAddressToMatrix" method.
        /// </summary>
        /// <param name="gooseMatrixFtp"></param>
        /// <param name="goCdRef"></param>
        /// <param name="goAppId"></param>
        void AddGooseCdFtpEntityToMatrix(IGooseMatrixFtp gooseMatrixFtp, string goCdRef, string goAppId);

        IGooseMatrixFtp GetGooseMatrixFtpForDevice(IDevice device);

        void SetGooseMatrixFtpForDevice(IDevice device, IGooseMatrixFtp gooseMatrixFtp);

        void SetSubscriptionRowsToMatrix(IGooseMatrixFtp gooseMatrixFtp,
            List<Tuple<string, IGooseRowFtpEntity>> subscriptionEntity);

    }
}