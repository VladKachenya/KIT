using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;
using BISC.Modules.InformationModel.Infrastucture.Services;
using System;
using System.Linq;
using BISC.Modules.Gooses.Infrastructure.Services;

namespace BISC.Modules.Device.Model.Services
{
    public class DeviceIdentificationService : IDeviceIdentificationService
    {
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly ISclCommunicationModelService _sclCommunicationModelService;
        private readonly IIpValidationService _ipValidationService;
        private readonly IInfoModelService _infoModelService;
        private readonly IDataTypeTemplatesModelService _dataTypeTemplatesModelService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly IBiscProject _biscProject;
        private readonly IGoosesModelService _goosesModelService;

        #region Ctor

        public DeviceIdentificationService(IConnectionPoolService connectionPoolService,
            ISclCommunicationModelService sclCommunicationModelService, IIpValidationService ipValidationService,
            IInfoModelService infoModelService, IDataTypeTemplatesModelService dataTypeTemplatesModelService,
            IDeviceModelService deviceModelService, IBiscProject biscProject, IGoosesModelService goosesModelService)
        {
            _connectionPoolService = connectionPoolService;
            _sclCommunicationModelService = sclCommunicationModelService;
            _ipValidationService = ipValidationService;
            _infoModelService = infoModelService;
            _dataTypeTemplatesModelService = dataTypeTemplatesModelService;
            _deviceModelService = deviceModelService;
            _biscProject = biscProject;
            _goosesModelService = goosesModelService;
        }

        #endregion

        #region implementetion of IDeviceIdentificationService
        public void ChengeDeviceIp(IDevice device, string settableIp, ISclModel sclModel = null)
        {
            if (sclModel == null)
            {
                sclModel = _biscProject.MainSclModel.Value;
            }
            if (_deviceModelService.GetDevicesFromModel(sclModel).
                Any(d => _sclCommunicationModelService.GetIpOfDevice(d.Name, sclModel) == settableIp))
            {
                throw new ArgumentException($"Устройство с Ip {settableIp} уже существует в проекте");
            }

            if (!_ipValidationService.IsExactFormIpAddress(settableIp)) { throw new ArgumentException($"Not valid IP{settableIp}"); }
            if (device.Manufacturer != DeviceKeys.DeviceManufacturer.BemnManufacturer) { throw new ArgumentException($"Недопустимый производитель {device.Manufacturer}"); }
            string newDeviceName = device.Name.Split('N')[0] + 'N' + settableIp.Split('.')[3];

            _goosesModelService.ChengeGooseDeviceInputOwner(_biscProject, device, newDeviceName);
            _sclCommunicationModelService.ReplaceAccessPointIp(device.GetFirstParentOfType<ISclModel>(), device.Name, settableIp);
            _sclCommunicationModelService.ReplaceAccessPointIdeName(device.GetFirstParentOfType<ISclModel>(), device.Name, newDeviceName);

            _infoModelService.UpdateLnTypesOfDevice(device, newDeviceName);
            _dataTypeTemplatesModelService.UpdateTemplatesUnderIdeName(device.GetFirstParentOfType<ISclModel>(), device.Name, newDeviceName);

            device.Ip = settableIp;
            device.Name = newDeviceName;
        }

        public void ChengeDeviceName(IDevice device, string settableName)
        {

        }

        #endregion

        #region private filds

        #endregion

    }
}