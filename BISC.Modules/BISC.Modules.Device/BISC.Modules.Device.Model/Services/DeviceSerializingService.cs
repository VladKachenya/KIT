using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Serializing;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;
using BISC.Modules.InformationModel.Infrastucture.Services;

namespace BISC.Modules.Device.Model.Services
{
    public class DeviceSerializingService : IDeviceSerializingService
    {
        private readonly IModelElementsRegistryService _modelElementsRegistryService;
        private readonly IDeviceModelService _deviceModelService;

        public DeviceSerializingService(IModelElementsRegistryService modelElementsRegistryService, IDeviceModelService deviceModelService)
        {
            _modelElementsRegistryService = modelElementsRegistryService;
            _deviceModelService = deviceModelService;
        }

        #region Implementation of IDeviceSerializingService

        public void SerializeCidSingleDevice(IDevice device, string filePath)
        {
            var sclModel = device.GetFirstParentOfType<ISclModel>();

            var sclModelCloneString = _modelElementsRegistryService.SerializeModelElement(sclModel, SerializingType.Extended).ToString();
            var sclModelClone =
                _modelElementsRegistryService.DeserializeModelElement<ISclModel>(XElement.Parse(sclModelCloneString));
            var devicesToRemove = _deviceModelService.GetDevicesFromModel(sclModelClone);
            foreach (var deviceToRemove in devicesToRemove)
            {
                if (deviceToRemove.Name == device.Name) continue;
                _deviceModelService.DeleteDeviceFromModel(sclModelClone, deviceToRemove.Name);
            }
            OrderSclModelElements(sclModelClone);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                _modelElementsRegistryService.SerializeModelElement(sclModelClone, SerializingType.Basic).Save(fs);
            }
        }

        private void OrderSclModelElements(ISclModel sclModel)
        {
            var dataTypesElement =
                sclModel.ChildModelElements.FirstOrDefault((element => element is IDataTypeTemplates));
            var communicationElement =
                sclModel.ChildModelElements.FirstOrDefault((element => element is ISclCommunicationModel));
            var iedElement =
                sclModel.ChildModelElements.FirstOrDefault((element => element is IDevice));
            sclModel.ChildModelElements.Clear();
            sclModel.ChildModelElements.Add(communicationElement);
            sclModel.ChildModelElements.Add(iedElement);
            sclModel.ChildModelElements.Add(dataTypesElement);

        }

        #endregion
    }
}