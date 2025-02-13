﻿using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Model.Model.Matrix;
using BISC.Modules.InformationModel.Infrastucture.Services;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BISC.Modules.Gooses.Model.Services
{
    public class GoosesModelService : IGoosesModelService
    {
        private readonly IInfoModelService _infoModelService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly IBiscProject _biscProject;
        private readonly ISclCommunicationModelService _sclCommunicationModelService;
        private readonly IDataSetModelService _dataSetModelService;


        public GoosesModelService(
            IInfoModelService infoModelService,
            IDeviceModelService deviceModelService,
            IBiscProject biscProject,
            ISclCommunicationModelService sclCommunicationModelService,
            IDataSetModelService dataSetModelService)
        {
            _infoModelService = infoModelService;
            _deviceModelService = deviceModelService;
            _biscProject = biscProject;
            _sclCommunicationModelService = sclCommunicationModelService;
            _dataSetModelService = dataSetModelService;
        }
        public void AddGseControl(string lnName, string ldName, IModelElement devcice, IGooseControl gooseControl)
        {
            var devices = _infoModelService.GetLDevicesFromDevices(devcice);

            var ld = devices.FirstOrDefault((device => device.Inst == ldName));
            if (ld != null)
            {
                if (lnName == "LLN0")
                {
                    ld.LogicalNodeZero.Value.ChildModelElements.Add(gooseControl);
                    gooseControl.ParentModelElement = ld.LogicalNodeZero.Value;
                }
                else
                {
                    var ln = ld.LogicalNodes.FirstOrDefault((node => node.Name == lnName));
                    ln?.ChildModelElements.Add(gooseControl);
                }
            }
        }

        public List<IGooseControl> GetGooseControlsOfDevice(IDevice device)
        {
            var ldevices = _infoModelService.GetLDevicesFromDevices(device);
            List<IGooseControl> gooseControls = new List<IGooseControl>();
            foreach (var lDevice in ldevices)
            {
                foreach (var childModelElement in lDevice.LogicalNodeZero.Value.ChildModelElements)
                {
                    if (childModelElement is IGooseControl findedGooseControl)
                    {
                        gooseControls.Add(findedGooseControl);
                    }
                }
            }

            return gooseControls;
        }

        public IEnumerable<IGooseControl> GetGooseControls(Guid deviceGuid, ISclModel sclModel)
        {
            return GetGooseControlsOfDevice(_deviceModelService.GetDeviceByGuid(sclModel, deviceGuid));
        }

        public void DeleteAllDeviceReferencesInGooseControlsInModel(IBiscProject biscProject, string iedName)
        {
            var customElements = biscProject.CustomElements?.Value?.ChildModelElements?.ToList();
            if (customElements != null)
            {
                foreach (var customElement in customElements)
                {
                    if (customElement is IGooseDeviceInput gooseDeviceInput)
                    {
                        if (gooseDeviceInput.DeviceOwnerName == iedName)
                        {
                            biscProject.CustomElements.Value.ChildModelElements.Remove(customElement);
                        }
                    }
                }
            }
        }

        public IGooseDeviceInput GetGooseDeviceInputOfProject(IBiscProject biscProject, IDevice device)
        {
            var gooseDeviceInputForDevice = biscProject.CustomElements?.Value?.ChildModelElements?.FirstOrDefault(element =>
                (element is IGooseDeviceInput gooseDeviceInput) && gooseDeviceInput.DeviceOwnerName == device.Name) as IGooseDeviceInput;
            if (gooseDeviceInputForDevice == null)
            {
                gooseDeviceInputForDevice = new GooseDeviceInput() { DeviceOwnerName = device.Name };
                biscProject.CustomElements?.Value?.ChildModelElements?.Add(gooseDeviceInputForDevice);
            }
            return gooseDeviceInputForDevice;
        }

        public List<IGooseInputModelInfo> GetGooseInputModelInfos(IDevice device, IBiscProject biscProject = null)
        {
            if (biscProject == null)
            {
                biscProject = _biscProject;
            }
            var res = new List<IGooseInputModelInfo>();
            var input = GetGooseDeviceInputOfProject(biscProject, device);
            foreach (var inputInfo in input.GooseInputModelInfoList)
            {
                res.Add(inputInfo);
            }
            return res;
        }

        public void SetGooseInputModelInfosToProject(IBiscProject biscProject, IDevice device, List<IGooseInputModelInfo> gooseInputModelInfos)
        {
            var inputs = GetGooseDeviceInputOfProject(biscProject, device);
            inputs.GooseInputModelInfoList.Clear();
            gooseInputModelInfos.ForEach(el => inputs.GooseInputModelInfoList.Add(el));
        }


        public void DeleteGooseCbAndGseByName(string name, IDevice device)
        {
            var ldevices = _infoModelService.GetLDevicesFromDevices(device);
            IGooseControl findedGooseControlToDelete = null;
            foreach (var lDevice in ldevices)
            {
                foreach (var childModelElement in lDevice.LogicalNodeZero.Value.ChildModelElements)
                {
                    if (childModelElement is IGooseControl findedGooseControl && findedGooseControl.Name == name)
                    {
                        findedGooseControlToDelete = findedGooseControl;
                        break;
                    }
                }
                if (findedGooseControlToDelete != null)
                {
                    lDevice.LogicalNodeZero.Value.ChildModelElements.Remove(findedGooseControlToDelete);
                }
            }
            if (findedGooseControlToDelete == null)
            {
                return;
            }

            _sclCommunicationModelService.DeleteGseOfDevice(device.Name, name, device.GetFirstParentOfType<ISclModel>());

            var datasets = _dataSetModelService.GetAllDataSetOfDevice(device);
            var datasetOfGoose = datasets.FirstOrDefault((set => set.Name == findedGooseControlToDelete.DataSet));
            if (datasetOfGoose != null)
            {
                foreach (var fcda in datasetOfGoose.FcdaList)
                {
                    var devices = _deviceModelService.GetDevicesFromModel(device.GetFirstParentOfType<ISclModel>());
                    foreach (var anotherDevice in devices)
                    {
                        if (anotherDevice == device)
                        {
                            continue;
                        }

                        var inputs = GetGooseInputsOfDevice(anotherDevice);
                        foreach (var input in inputs)
                        {
                            IExternalGooseRef externalGooseRefToDelete = null;
                            foreach (var externalGooseReference in input.ExternalGooseReferences)
                            {
                                if (CompareFcdaAndExtRef(externalGooseReference, fcda))
                                {
                                    externalGooseRefToDelete = externalGooseReference;
                                    break;
                                }
                            }

                            if (externalGooseRefToDelete != null)
                            {
                                input.ExternalGooseReferences.Remove(externalGooseRefToDelete);
                            }
                        }
                    }
                }
            }
        }
        public bool CompareFcdaAndExtRef(IExternalGooseRef externalGooseRef, IFcda fcda)
        {
            if (externalGooseRef.Prefix != fcda.Prefix)
            {
                return false;
            }

            if (externalGooseRef.DaName != fcda.DaName)
            {
                return false;
            }

            if (externalGooseRef.DoName != fcda.DoName)
            {
                return false;
            }

            if (externalGooseRef.LdInst != fcda.LdInst)
            {
                return false;
            }

            if (externalGooseRef.LnInst != fcda.LnInst)
            {
                return false;
            }

            if (externalGooseRef.LnClass != fcda.LnClass)
            {
                return false;
            }

            return true;
        }

        public void DeleteAllGoosesFromDevice(IDevice device)
        {
            var ldevices = _infoModelService.GetLDevicesFromDevices(device);
            List<IGooseControl> findedAllGooseControlsToDelete = new List<IGooseControl>();

            foreach (var lDevice in ldevices)
            {
                List<IGooseControl> findedGooseControlsToDelete = new List<IGooseControl>();

                foreach (var childModelElement in lDevice.LogicalNodeZero.Value.ChildModelElements)
                {
                    if (childModelElement is IGooseControl findedGooseControl)
                    {
                        findedGooseControlsToDelete.Add(findedGooseControl);
                        findedAllGooseControlsToDelete.Add(findedGooseControl);
                    }
                }

                findedGooseControlsToDelete.ForEach((control =>
                {
                    lDevice.LogicalNodeZero.Value.ChildModelElements.Remove(control);
                }));
            }

            if (!findedAllGooseControlsToDelete.Any())
            {
                return;
            }

            foreach (var control in findedAllGooseControlsToDelete)
            {
                _sclCommunicationModelService.DeleteGseOfDevice(device.Name, control.Name, device.GetFirstParentOfType<ISclModel>());
            }
        }

        public List<IGooseInput> GetGooseInputsOfDevice(IDevice device)
        {
            var ldevices = _infoModelService.GetLDevicesFromDevices(device);
            List<IGooseInput> gooseInputs = new List<IGooseInput>();
            foreach (var lDevice in ldevices)
            {
                foreach (var childModelElement in lDevice.LogicalNodeZero.Value.ChildModelElements)
                {
                    if (childModelElement is IGooseInput findedGooseInput)
                    {
                        gooseInputs.Add(findedGooseInput);
                    }
                }
            }

            return gooseInputs;
        }

        public void ChengeGooseDeviceInputOwner(IBiscProject biscProject, IDevice device, string newDeviceOwnerName)
        {
            var gooseInputInfo = GetGooseDeviceInputOfProject(biscProject, device);
            gooseInputInfo.DeviceOwnerName = newDeviceOwnerName;
        }




    }
}
