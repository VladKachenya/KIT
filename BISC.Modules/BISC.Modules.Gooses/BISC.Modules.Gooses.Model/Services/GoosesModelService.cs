using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Model.Model;
using BISC.Modules.Gooses.Model.Model.Matrix;
using BISC.Modules.InformationModel.Infrastucture.Services;

namespace BISC.Modules.Gooses.Model.Services
{
    public class GoosesModelService : IGoosesModelService
    {
        private readonly IInfoModelService _infoModelService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly IBiscProject _biscProject;
        private readonly ISclCommunicationModelService _sclCommunicationModelService;
        private readonly IDatasetModelService _datasetModelService;

        public GoosesModelService(IInfoModelService infoModelService, IDeviceModelService deviceModelService, IBiscProject biscProject,
            ISclCommunicationModelService sclCommunicationModelService, IDatasetModelService datasetModelService)
        {
            _infoModelService = infoModelService;
            _deviceModelService = deviceModelService;
            _biscProject = biscProject;
            _sclCommunicationModelService = sclCommunicationModelService;
            _datasetModelService = datasetModelService;
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

        public void DeleteAllDeviceReferencesInGooseControlsInModel(ISclModel sclModel, string iedName)
        {
            var devices = _deviceModelService.GetDevicesFromModel(sclModel);
            foreach (var device in devices)
            {
                var gooses = GetGooseControlsOfDevice(device);
                foreach (var goose in gooses)
                {
                    var deviceSubsriber =
                        goose.SubscriberDevice.FirstOrDefault((subscriberDevice =>
                            subscriberDevice.DeviceName == iedName));
                    if (deviceSubsriber != null)
                    {
                        goose.SubscriberDevice.Remove(deviceSubsriber);
                    }
                }
            }

            IGooseMatrix gooseMatrix = null;
            foreach (var customElement in _biscProject.CustomElements.Value.ChildModelElements)
            {
                if (customElement is IGooseMatrix gooseMatrixInModel)
                {
                    if (gooseMatrixInModel.RelatedIedName == iedName)
                    {
                        gooseMatrix = gooseMatrixInModel;
                    }
                }
            }
            if (gooseMatrix != null)
            {
                _biscProject.CustomElements.Value.ChildModelElements.Remove(gooseMatrix);
            }
        }

        public void SetGooseControlSubscriber(bool isSubscribed, IGooseControl gooseControl, IDevice device)
        {
            var subscriber = gooseControl.SubscriberDevice.FirstOrDefault((subscriberDevice =>
                subscriberDevice.DeviceName == device.Name));
            if (!isSubscribed)
            {
                if (subscriber != null)
                {
                    gooseControl.SubscriberDevice.Remove(subscriber);
                }
            }
            else
            {
                if (subscriber == null)
                {
                    gooseControl.SubscriberDevice.Add(new SubscriberDevice()
                    {
                        DeviceName = device.Name,
                    });
                }
            }
        }

        public void AddGooseExternalReferenceToDevice(IFcda fcda, IDevice device, string deviceNameOfFcda)
        {
            var inputs = GetGooseInputsOfDevice(device);
            var extRef = new ExternalGooseRef()
            {
                DaName = fcda.DaName,
                DoName = fcda.DoName,
                LdInst = fcda.LdInst,
                LnClass = fcda.LnClass,
                LnInst = fcda.LnInst,
                Prefix = fcda.Prefix,
                IedName = deviceNameOfFcda
            };
            if (inputs.Any())
            {
                inputs.First().ExternalGooseReferences.Add(extRef);
            }
            else
            {
                var ldevices = _infoModelService.GetLDevicesFromDevices(device);
                GooseInput gooseInput = new GooseInput();
                gooseInput.ExternalGooseReferences.Add(extRef);
                ldevices.First().LogicalNodeZero.Value.ChildModelElements.Add(gooseInput);
            }
        }

        public List<Tuple<IDevice, IGooseControl>> GetGooseControlsSubscribed(IDevice deviceSubscriber, ISclModel sclModel)
        {
            List<Tuple<IDevice, IGooseControl>> result = new List<Tuple<IDevice, IGooseControl>>();
            var devices = _deviceModelService.GetDevicesFromModel(sclModel);
            foreach (var device in devices)
            {
                if (device == deviceSubscriber)
                {
                    continue;
                }
                var gooseControls = GetGooseControlsOfDevice(device);
                foreach (var gooseControl in gooseControls)
                {
                    if (gooseControl.SubscriberDevice.Any((subscriberDevice =>
                        subscriberDevice.DeviceName == deviceSubscriber.Name)))
                    {
                        result.Add(new Tuple<IDevice, IGooseControl>(device, gooseControl));
                    }
                }
            }
            return result;

        }

        public void SetGooseMatrixForDevice(IDevice device, IGooseMatrix gooseMatrix)
        {
            IGooseMatrix existing = null;
            foreach (var customElement in _biscProject.CustomElements.Value.ChildModelElements)
            {
                if (customElement is IGooseMatrix gooseMatrixInModel)
                {
                    if (gooseMatrixInModel.RelatedIedName == device.Name)
                    {
                        existing = gooseMatrixInModel;
                    }
                }
            }
            if (existing != null)
            {
                if (existing == gooseMatrix) return;
                existing.GooseRows.Clear();
                existing.GooseRows.AddRange(gooseMatrix.GooseRows);
            }
            else
            {
                gooseMatrix.RelatedIedName = device.Name;
                _biscProject.CustomElements.Value.ChildModelElements.Add(gooseMatrix);
            }
        }

        public IGooseMatrix GetGooseMatrixForDevice(IDevice device)
        {
            foreach (var customElement in _biscProject.CustomElements.Value.ChildModelElements)
            {
                if (customElement is IGooseMatrix gooseMatrixInModel)
                {
                    if (gooseMatrixInModel.RelatedIedName == device.Name)
                    {
                        return gooseMatrixInModel;
                    }
                }
            }
            IGooseMatrix gooseMatrix = new GooseMatrix();
            gooseMatrix.RelatedIedName = device.Name;
            _biscProject.CustomElements.Value.ChildModelElements.Add(gooseMatrix);
            return gooseMatrix;
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
            if (findedGooseControlToDelete == null) return;
            _sclCommunicationModelService.DeleteGseOfDevice(device.Name, name, device.GetFirstParentOfType<ISclModel>());

            var datasets = _datasetModelService.GetAllDataSetOfDevice(device);
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
            if (externalGooseRef.Prefix != fcda.Prefix) return false;
            if (externalGooseRef.DaName != fcda.DaName) return false;
            if (externalGooseRef.DoName != fcda.DoName) return false;
            if (externalGooseRef.LdInst != fcda.LdInst) return false;
            if (externalGooseRef.LnInst != fcda.LnInst) return false;
            if (externalGooseRef.LnClass != fcda.LnClass) return false;
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

            if (!findedAllGooseControlsToDelete.Any()) return;
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

    }
}
