using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.DataSets.Infrastructure.Model;
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
   public class  GoosesModelService: IGoosesModelService
    {
        private readonly IInfoModelService _infoModelService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly IBiscProject _biscProject;

        public GoosesModelService(IInfoModelService infoModelService,IDeviceModelService deviceModelService,IBiscProject biscProject)
        {
            _infoModelService = infoModelService;
            _deviceModelService = deviceModelService;
            _biscProject = biscProject;
        }
        public void AddGseControl(string lnName, string ldName, IModelElement devcice, IGooseControl gooseControl)
        {
            var devices = _infoModelService.GetLDevicesFromDevices(devcice);

            var ld = devices.FirstOrDefault((device => device.Inst == ldName));
            if (ld != null)
            {
                if (lnName == "LLN0")
                {
                    ld.LogicalNodeZero.ChildModelElements.Add(gooseControl);
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
           var ldevices= _infoModelService.GetLDevicesFromDevices(device);
            List<IGooseControl> gooseControls=new List<IGooseControl>();
            foreach (var lDevice in ldevices)
            {
                foreach (var childModelElement in lDevice.LogicalNodeZero.ChildModelElements)
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
            foreach (var customElement in _biscProject.CustomElements.ChildModelElements)
            {
                if (customElement is IGooseMatrix gooseMatrixInModel)
                {
                    if (gooseMatrix.RelatedIedName == iedName)
                    {
                        gooseMatrix = gooseMatrixInModel;
                    }
                }
            }
            if (gooseMatrix != null)
            {
                _biscProject.CustomElements.ChildModelElements.Remove(gooseMatrix);
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

        public void AddGooseExternalReferenceToDevice(IFcda fcda, IDevice device,string deviceNameOfFcda)
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
                GooseInput gooseInput=new GooseInput();
                gooseInput.ExternalGooseReferences.Add(extRef);
                ldevices.First().LogicalNodeZero.ChildModelElements.Add(gooseInput);
            }
        }

        public List<Tuple<IDevice, IGooseControl>> GetGooseControlsSubscribed(IDevice deviceSubscriber, ISclModel sclModel)
        {
            List<Tuple<IDevice, IGooseControl>> result=new List<Tuple<IDevice, IGooseControl>>();
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

        public IGooseMatrix GetGooseMatrixForDevice(IDevice device)
        {
            foreach (var customElement in _biscProject.CustomElements.ChildModelElements)
            {
                if (customElement is IGooseMatrix gooseMatrixInModel)
                {
                    if (gooseMatrixInModel.RelatedIedName == device.Name)
                    {
                        return gooseMatrixInModel;
                    }
                }
            }
            IGooseMatrix gooseMatrix=new GooseMatrix();
            gooseMatrix.RelatedIedName = device.Name;
            _biscProject.CustomElements.ChildModelElements.Add(gooseMatrix);
            return gooseMatrix;
        }


        public List<IGooseInput> GetGooseInputsOfDevice(IDevice device)
        {
            var ldevices = _infoModelService.GetLDevicesFromDevices(device);
            List<IGooseInput> gooseInputs = new List<IGooseInput>();
            foreach (var lDevice in ldevices)
            {
                foreach (var childModelElement in lDevice.LogicalNodeZero.ChildModelElements)
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
