using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Factorys;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Model.Model;
using BISC.Modules.Gooses.Model.Model.Matrix;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BISC.Modules.Gooses.Model.Services
{
    public class GooseMatrixFtpService : IGooseMatrixFtpService
    {
        private readonly IGoosesModelService _goosesModelService;
        private readonly IBiscProject _biscProject;
        private readonly IGoCbFtpEntityFactory _goCbFtpEntityFactory; 

        public GooseMatrixFtpService(
            IGoosesModelService goosesModelService, 
            IBiscProject biscProject, 
            IGoCbFtpEntityFactory goCbFtpEntityFactory)
        {
            _goosesModelService = goosesModelService;
            _biscProject = biscProject;
            _goCbFtpEntityFactory = goCbFtpEntityFactory;
        }


        #region implementation of IGooseMatrixFtpService

        public void DeleteGooseSubscription(IGooseMatrixFtp gooseMatrixFtp, IGoCbFtpEntity goCd)
        {
            var forErasure = gooseMatrixFtp.GoCbFtpEntities.FirstOrDefault(el => el.ModelElementCompareTo(goCd));
            if (forErasure == null) { return; }
            gooseMatrixFtp.GoCbFtpEntities.Remove(forErasure);
            ErasureStates(gooseMatrixFtp, forErasure);
            ErasureQuality(gooseMatrixFtp, forErasure);
            RegularizeMatrix(gooseMatrixFtp);
        }

        public void RegularizeMatrix(IGooseMatrixFtp gooseMatrixFtp)
        {
            for (int i = 1; i <= gooseMatrixFtp.GoCbFtpEntities.Count; i++)
            {
                var goCbFtpEntity = gooseMatrixFtp.GoCbFtpEntities[i - 1];
                var stateEntitys =
                    gooseMatrixFtp.GooseRowFtpEntities.Where(el => el.IndexOfGoose == goCbFtpEntity.IndexOfGoose).ToList();
                var qualityEntitys =
                    gooseMatrixFtp.GooseRowQualityFtpEntities.Where(el => el.IndexOfGoose == goCbFtpEntity.IndexOfGoose).ToList();

                goCbFtpEntity.IndexOfGoose = i;
                stateEntitys.ForEach(el => el.IndexOfGoose = i);
                qualityEntitys.ForEach(el => el.IndexOfGoose = i);
            }
        }

        public void AddMacAddressToMatrix(IGooseMatrixFtp gooseMatrixFtp, string macAddress)
        {
            if (StaticStringValidationService.MacAddressValidation(macAddress) && !gooseMatrixFtp.MacAddressList.Any(el => el.MacAddress == macAddress))
            {
                gooseMatrixFtp.MacAddressList.Add(new MacAddressEntity() { MacAddress = macAddress });
            }
        }

        public void AddGooseCdFtpEntityToMatrix(IGooseMatrixFtp gooseMatrixFtp, string goCdRef, uint goAppId, int confRev)
        {
            if (gooseMatrixFtp.GoCbFtpEntities.Any(el => el.GoCbReference == goCdRef && el.AppId == goAppId.ToString("D") && el.ConfRev == confRev)) { return; }
            var goCdEntity = gooseMatrixFtp.GoCbFtpEntities;
            var i = goCdEntity.Count == 0 ? 1 : goCdEntity[goCdEntity.Count - 1].IndexOfGoose + 1;
            gooseMatrixFtp.GoCbFtpEntities.Add(_goCbFtpEntityFactory.GetGoCbFtpEntity(i, goCdRef, goAppId, confRev));
        }

        public IGooseMatrixFtp GetGooseMatrixFtpForDevice(IDevice device, IBiscProject biscProject = null)
        {
            if (biscProject == null)
            {
                biscProject = _biscProject;
            }

            IGooseMatrixFtp gooseMatrixFtp;
            var inputEntity = _goosesModelService.GetGooseDeviceInputOfProject(biscProject, device);
            if (inputEntity.GooseMatrix.Value == null)
            {
                gooseMatrixFtp = new GooseMatrixFtp();
            }
            else
            {
                gooseMatrixFtp = inputEntity.GooseMatrix.Value;
            }
            inputEntity.GooseMatrix.Value = gooseMatrixFtp;
            return gooseMatrixFtp;
        }

        public void SetGooseMatrixFtpForDevice(IDevice device, IGooseMatrixFtp gooseMatrixFtp, IBiscProject biscProject = null)
        {
            if (biscProject == null)
            {
                biscProject = _biscProject;
            }
            var gooseDeviceInput = _goosesModelService.GetGooseDeviceInputOfProject(biscProject, device);
            gooseDeviceInput.GooseMatrix.Value = gooseMatrixFtp;
        }

        public void SetSubscriptionRowsToMatrix(IGooseMatrixFtp gooseMatrixFtp,
            List<Tuple<IGoCbFtpEntity, IGooseRowFtpEntity>> subscriptionEntity)
        {
            gooseMatrixFtp.GooseRowFtpEntities.Clear();
            gooseMatrixFtp.GooseRowQualityFtpEntities.Clear();
            foreach (var tuple in subscriptionEntity)
            {
                var goCdFtpEntity = gooseMatrixFtp.GoCbFtpEntities.First(el => el.ModelElementCompareTo(tuple.Item1));
                tuple.Item2.IndexOfGoose = goCdFtpEntity.IndexOfGoose;
                if (tuple.Item2 is IGooseRowQualityFtpEntity)
                {
                    gooseMatrixFtp.GooseRowQualityFtpEntities.Add(tuple.Item2 as IGooseRowQualityFtpEntity);
                    continue;
                }
                gooseMatrixFtp.GooseRowFtpEntities.Add(tuple.Item2);
            }
        }

        public List<IGooseRowFtpEntity> GetAllRowEntitiesOfGoCd(IGooseMatrixFtp gooseMatrixFtp,
            IGoCbFtpEntity goCbFtpEntity)
        {
            var res = new List<IGooseRowFtpEntity>();
            var goCdFtpEntityInMatrix = gooseMatrixFtp.GoCbFtpEntities.FirstOrDefault( el => el.ModelElementCompareTo(goCbFtpEntity));
            if (goCdFtpEntityInMatrix == null)
            {
                throw new Exception("Объект goCbFtpEntity не найдет в матрице");
            }

            foreach (var rowFtpEntityS in gooseMatrixFtp.GooseRowFtpEntities)
            {
                if (rowFtpEntityS.IndexOfGoose == goCdFtpEntityInMatrix.IndexOfGoose)
                {
                    res.Add(rowFtpEntityS);
                }
            }
            foreach (var rowFtpEntityQ in gooseMatrixFtp.GooseRowFtpEntities)
            {
                if (rowFtpEntityQ.IndexOfGoose == goCdFtpEntityInMatrix.IndexOfGoose)
                {
                    res.Add(rowFtpEntityQ);
                }
            }
            return res;
        }

        #endregion

        #region private methods

        private void ErasureStates(IGooseMatrixFtp gooseMatrixFtp, IGoCbFtpEntity goCbFtpEntity)
        {
            var statsForDelet = gooseMatrixFtp.GooseRowFtpEntities.Where(el => el.IndexOfGoose == goCbFtpEntity.IndexOfGoose).ToList();
            if (statsForDelet.Count == 0) { return; }
            statsForDelet.ForEach(el => gooseMatrixFtp.GooseRowFtpEntities.Remove(el));
        }

        private void ErasureQuality(IGooseMatrixFtp gooseMatrixFtp, IGoCbFtpEntity goCbFtpEntity)
        {
            var statsForDelet = gooseMatrixFtp.GooseRowQualityFtpEntities.Where(el => el.IndexOfGoose == goCbFtpEntity.IndexOfGoose).ToList();
            if (statsForDelet.Count == 0) { return; }
            statsForDelet.ForEach(el => gooseMatrixFtp.GooseRowQualityFtpEntities.Remove(el));
        }

        #endregion
    }
}