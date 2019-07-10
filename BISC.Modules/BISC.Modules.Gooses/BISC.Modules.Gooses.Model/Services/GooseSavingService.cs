using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Serializing;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Factorys;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BISC.Modules.Gooses.Model.Services
{
    public class GooseSavingService : IGooseSavingService
    {
        private readonly IGooseInputModelInfoFactory _gooseInputModelIngoFactory;
        private readonly IGoosesModelService _goosesModelService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IBiscProject _biscProject;
        private readonly Func<IFtpGooseModelService> _ftpGooseModelService;
        private readonly IGooseMatrixFtpService _gooseMatrixFtpService;
        private readonly IGoCbFtpEntityFactory _cbFtpEntityFactory;

        public GooseSavingService(IGooseInputModelInfoFactory gooseInputModelIngoFactory, IGoosesModelService goosesModelService,
            IConnectionPoolService connectionPoolService, IBiscProject biscProject,
            Func<IFtpGooseModelService> ftpGooseModelService, IGooseMatrixFtpService gooseMatrixFtpService, IGoCbFtpEntityFactory cbFtpEntityFactory)
        {
            _gooseInputModelIngoFactory = gooseInputModelIngoFactory;
            _goosesModelService = goosesModelService;
            _connectionPoolService = connectionPoolService;
            _biscProject = biscProject;
            _ftpGooseModelService = ftpGooseModelService;
            _gooseMatrixFtpService = gooseMatrixFtpService;
            _cbFtpEntityFactory = cbFtpEntityFactory;
        }

        #region Implementation of IGooseSavingService



        public async Task SaveSubscriptionGooces(IDevice subscribingDevice, List<IGooseInputModelInfo> acceptedGooses)
        {
            WriteGooseInputsModelInfoToMainProjectIDevice(subscribingDevice, acceptedGooses);
            var gooseMatrix = _gooseMatrixFtpService.GetGooseMatrixFtpForDevice(subscribingDevice);
            var gooseInputModelInfoList = _goosesModelService.GetGooseDeviceInputOfProject(_biscProject, subscribingDevice).GooseInputModelInfoList.ToList();

            MatchMatrixToInputs(gooseMatrix, gooseInputModelInfoList);

            //if (_connectionPoolService.GetConnection(subscribingDevice.Ip).IsConnected)
            //{
            //    await WriteGooseInputsModelInfoToDevice(subscribingDevice, acceptedGooses);
            //    await WriteGooseMatrixToDevice(subscribingDevice, gooseMatrix);
            //}
        }

        public async Task SaveSubscriptionMatrix(IDevice device, List<Tuple<IGoCbFtpEntity, IGooseRowFtpEntity>> subscriptionEntity)
        {
            var subscriptionMatrix = _gooseMatrixFtpService.GetGooseMatrixFtpForDevice(device);
            _gooseMatrixFtpService.SetSubscriptionRowsToMatrix(subscriptionMatrix, subscriptionEntity);
            //if (_connectionPoolService.GetConnection(device.Ip).IsConnected)
            //{
            //    await _ftpGooseModelService().WriteGooseMatrixFtpToDevice(device, subscriptionMatrix);
            //}
        }
        #endregion

        #region private region

        private void MatchMatrixToInputs(IGooseMatrixFtp gooseMatrix, List<IGooseInputModelInfo> gooseInputModelInfoList)
        {
            var goosesToMatrixRefs = gooseMatrix.GoCbFtpEntities;
            var goosesToInputModelInfoRef = gooseInputModelInfoList.Select(el => _cbFtpEntityFactory.GetIGoCbFtpEntityFromGooseInputModelInfo(el)).ToList();
            var macAddressesToInputs = gooseInputModelInfoList.Select(el => el.EmittingGse.Value.MacAddress).ToList();

            // Удаление неотмеченых 
            //var forEnumeration = ;
            foreach (var gooseRef in new List<IGoCbFtpEntity>(goosesToMatrixRefs))
            {
                if (!goosesToInputModelInfoRef.Any(el => el.ModelElementCompareTo(gooseRef)))
                {
                    _gooseMatrixFtpService.DeleteGooseSubscription(gooseMatrix, gooseRef);
                }
            }
            _gooseMatrixFtpService.RegularizeMatrix(gooseMatrix);

            // Добавление отмеченых
            foreach (var element in gooseInputModelInfoList)
            {
                _gooseMatrixFtpService.AddGooseCdFtpEntityToMatrix(gooseMatrix, element.GocbRef, uint.Parse(element.EmittingGse.Value.AppId, NumberStyles.HexNumber),
                    element.EmittingGooseControl.Value.ConfRev);
                _gooseMatrixFtpService.AddMacAddressToMatrix(gooseMatrix, element.EmittingGse.Value.MacAddress);
            }

            //Сопоставление мак адресов
            gooseMatrix.MacAddressList.Clear();
            macAddressesToInputs.ForEach(el => _gooseMatrixFtpService.AddMacAddressToMatrix(gooseMatrix, el));

        }

        private void WriteGooseInputsModelInfoToMainProjectIDevice(IDevice device,
            List<IGooseInputModelInfo> gooseInputModelInfos)
        {
            var gooseDeviseInput = _goosesModelService.GetGooseDeviceInputOfProject(_biscProject, device);
            gooseDeviseInput.GooseInputModelInfoList?.Clear();
            gooseInputModelInfos.ForEach(gimi => gooseDeviseInput.GooseInputModelInfoList.Add(gimi));
        }

        //private async Task WriteGooseInputsModelInfoToDevice(IDevice device,
        //    List<IGooseInputModelInfo> gooseInputModelInfos)
        //{
        //    var writingResult =
        //        await _ftpGooseModelService().WriteGooseDeviceInputFromDevice(device.Ip, gooseInputModelInfos);
        //    if (!writingResult.IsSucceed)
        //    {
        //        throw new Exception($"Ошибка при записи файла из устройства {device.Name}");
        //    }
        //}

        //private async Task WriteGooseMatrixToDevice(IDevice device, IGooseMatrixFtp gooseMatrixFtp)
        //{

        //    var writingResult =
        //        await _ftpGooseModelService().WriteGooseMatrixFtpToDevice(device, gooseMatrixFtp);
        //    if (!writingResult.IsSucceed)
        //    {
        //        throw new Exception($"Ошибка при записи файла подписок в устройство {device.Name}");
        //    }
        //}
        #endregion
    }
}