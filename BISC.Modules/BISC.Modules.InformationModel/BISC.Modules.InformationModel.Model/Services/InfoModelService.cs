using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.InformationModel.Model.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;
using BISC.Modules.InformationModel.Infrastucture.Keys;
using BISC.Modules.InformationModel.Model.Helpers;

namespace BISC.Modules.InformationModel.Model.Services
{
    public class InfoModelService : IInfoModelService
    {
        private readonly ISclCommunicationModelService _sclCommunicationModelService;
        private readonly IDataTypeTemplatesModelService _dataTypeTemplatesModelService;
        private readonly IBiscProject _biscProject;

        public InfoModelService(ISclCommunicationModelService sclCommunicationModelService,IDataTypeTemplatesModelService dataTypeTemplatesModelService,IBiscProject biscProject)
        {
            _sclCommunicationModelService = sclCommunicationModelService;
            _dataTypeTemplatesModelService = dataTypeTemplatesModelService;
            _biscProject = biscProject;
        }


        public List<string> GetAllFcs(List<IDai> dais, List<ISdi> sdis)
        {
            List<string> fcList = new List<string>();
            foreach (var dai in dais)
            {
                var da = _dataTypeTemplatesModelService.GetDaOfDai(dai, _biscProject.MainSclModel.Value);
                if (da == null)
                {
                    continue;
                }
                fcList.Add(da.Fc);
            }

            foreach (var sdi in sdis)
            {
                fcList.AddRange(GetAllFcs(sdi.DaiCollection.ToList(), sdi.SdiCollection.ToList()));
            }

            return fcList;
        }
        public List<Tuple<string,IDai>> GetAllFcsWithDai(List<IDai> dais, List<ISdi> sdis, ISclModel sclModel = null)
        {
            sclModel = sclModel ?? _biscProject.MainSclModel.Value;
            List < Tuple < string,IDai >> fcTuplesList=new List<Tuple<string, IDai>>();
            foreach (var dai in dais)
            {
                var da = _dataTypeTemplatesModelService.GetDaOfDai(dai, sclModel);
                if (da == null)
                {
                    continue;
                }
                fcTuplesList.Add(new Tuple<string, IDai>(da.Fc,dai));
            }

            foreach (var sdi in sdis)
            {
                fcTuplesList.AddRange(GetAllFcsWithDai(sdi.DaiCollection.ToList(), sdi.SdiCollection.ToList(), sclModel));
            }

            return fcTuplesList;
        }

        public void AddOrReplaceLDevice(IDeviceAccessPoint deviceAccessPoint, ILDevice lDevice)
        {
            var existingLdevice =
                deviceAccessPoint.DeviceServer.Value.LDevicesCollection.FirstOrDefault((deviceExisting => deviceExisting.Inst == lDevice.Inst));

            if (existingLdevice != null)
            {
                deviceAccessPoint.DeviceServer.Value.LDevicesCollection.Remove(existingLdevice);
                deviceAccessPoint.DeviceServer.Value.ChildModelElements.Remove(existingLdevice);

            }

            deviceAccessPoint.DeviceServer.Value.LDevicesCollection.Add(lDevice);

        }

        public void InitializeInfoModel(IModelElement device, string deviceName, ISclModel sclModel)
        {
            IDeviceAccessPoint deviceAccessPoint = new DeviceAccessPoint();

            deviceAccessPoint.DeviceServer.Value = new DeviceServer();

            deviceAccessPoint.Name =
                _sclCommunicationModelService.GetConnectedAccessPoint(sclModel, deviceName).ApName;
            device.ChildModelElements.Add(deviceAccessPoint);
            deviceAccessPoint.ParentModelElement = device;
        }

        public bool ContainsDbRecursive(IModelElement modelElement)
        {
            if (modelElement is IDai dai)
            {
                if (String.Equals(dai.Name, InformationModelKeys.DataAttributeHeaderKeys.db,
                        StringComparison.CurrentCultureIgnoreCase) ||
                    String.Equals(dai.Name, InformationModelKeys.DataAttributeHeaderKeys.zeroDb,
                        StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }

                return false;
            }
            else
            {
                foreach (var child in modelElement.ChildModelElements)
                {
                    if (ContainsDbRecursive(child))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public IEnumerable<IDoi> GetAllDoiWithDbRecursive(IModelElement modelElement)
        {
            var res = new List<IDoi>();
            foreach (var child in modelElement.ChildModelElements)
            {
                if (!ContainsDbRecursive(child))
                {
                    continue;
                }

                if (child is IDoi doi)
                {
                    res.Add(doi);
                }
                else
                {
                    res.AddRange(GetAllDoiWithDbRecursive(child));
                }
            }
            return res;
        }

        public List<ILDevice> GetLDevicesFromDevices(IModelElement device)
        {
            var childModelProperty = (device.ChildModelElements.First((element => element is DeviceAccessPoint)) as DeviceAccessPoint)?.DeviceServer;
            if (childModelProperty?.Value != null)
            {
                return childModelProperty.Value.LDevicesCollection.ToList();
            }

            return new List<ILDevice>();
        }

        public ILDevice GetZeroLDevicesOfDevice(IDevice device)
        {
            var lDevices = GetLDevicesFromDevices(device);
            return lDevices.FirstOrDefault(ld => ld.Inst == "LD0");
        }

        public ILDevice GetParentLDevice(IModelElement childElement)
        {
            var parientElement = childElement.ParentModelElement;
            switch (parientElement)
            {
                case null:
                    return null;
                case ILDevice _:
                    return parientElement as ILDevice;
                default:
                    return GetParentLDevice(parientElement);
            }
        }

        public ILogicalNode GetParentLogicalNode(IModelElement childElement)
        {
            var parientElement = childElement.ParentModelElement;
            switch (parientElement)
            {
                case null:
                    return null;
                case ILogicalNode _:
                    return parientElement as ILogicalNode;
                default:
                    return GetParentLogicalNode(parientElement);
            }
        }


        public List<ISettingControl> GetSettingControlsOfDevice(IModelElement device)
        {
            List<ISettingControl> settingControls = new List<ISettingControl>();
            device.GetAllChildrenOfType(ref settingControls);
            return settingControls;
        }

        public string GetFullNameOfLogicalNode(ILogicalNode logicalNode)
        {
            return logicalNode.Prefix + logicalNode.LnClass + logicalNode.Inst;
        }

        public void UpdateLnTypesOfDevice(IDevice device, string newDeviceName)
        {
            if (device == null) { throw new ArgumentNullException(); }
            if (string.IsNullOrWhiteSpace(newDeviceName)) { throw new ArgumentException(); }

            var replaser = new IdeNameInStringReplacer();

            var lDevices = GetLDevicesFromDevices(device);
            foreach (var lDevice in lDevices)
            {
                foreach (var lNode in lDevice.AlLogicalNodes)
                {
                    lNode.LnType =
                        replaser.ReplaseIdeNameInStringWithExeption(lNode.LnType, device.Name, newDeviceName);
                }
            }
        }


    }
}