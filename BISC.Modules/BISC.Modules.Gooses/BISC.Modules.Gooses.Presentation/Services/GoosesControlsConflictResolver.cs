using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Project.Communication;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Device.Infrastructure.HelpClasses;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Presentation.Factories;
using BISC.Modules.Gooses.Presentation.ViewModels.GooseControls;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Gooses.Model.Services
{
    public class GoosesControlsConflictResolver : IElementConflictResolver
    {
        private readonly IDeviceModelService _deviceModelService;
        private readonly IGoosesModelService _goosesModelService;
        private readonly GooseControlViewModelFactory _gooseControlViewModelFactory;
        private readonly IFtpGooseModelService _ftpGooseModelService;
        private readonly ISclCommunicationModelService _sclCommunicationModelService;
        private readonly INavigationService _navigationService;
        public ConflictType ConflictType => ConflictType.ManualResolveNeeded;
        public GoosesControlsConflictResolver(IDeviceModelService deviceModelService, IGoosesModelService goosesModelService, GooseControlViewModelFactory gooseControlViewModelFactory,
            IFtpGooseModelService ftpGooseModelService, ISclCommunicationModelService sclCommunicationModelService, INavigationService navigationService)
        {
            _deviceModelService = deviceModelService;
            _goosesModelService = goosesModelService;
            _gooseControlViewModelFactory = gooseControlViewModelFactory;
            _ftpGooseModelService = ftpGooseModelService;
            _sclCommunicationModelService = sclCommunicationModelService;
            _navigationService = navigationService;
        }



        #region Implementation of IElementConflictResolver

        public string ConflictName => "Goose Controls";
        public bool GetIfConflictsExists(Guid deviceGuid, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            var deviceInsclModelInDevice = _deviceModelService.GetDeviceByGuid(sclModelInDevice, deviceGuid);
            var devicesclModelInProject = _deviceModelService.GetDeviceByGuid(sclModelInProject, deviceGuid);


            var gooseControlsInDevice = _goosesModelService.GetGooseControlsOfDevice(deviceInsclModelInDevice);
            var gooseControlsInProject = _goosesModelService.GetGooseControlsOfDevice(devicesclModelInProject);


            if (gooseControlsInDevice.Count != gooseControlsInProject.Count)
            {
                return true;
            }


            foreach (var gooseControlInDevice in gooseControlsInDevice)
            {
                var gooseControlInProject = gooseControlsInProject.FirstOrDefault((gc => gc.Name == gooseControlInDevice.Name));
                if (gooseControlInProject == null) return true;
                if (!gooseControlInProject.ModelElementCompareTo(gooseControlInDevice))
                {
                    return true;
                }
            }
            return false;

        }

        public async Task<ResolvingResult> ResolveConflict(bool isFromDevice, Guid deviceGuid, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            var deviceInsclModelInDevice = _deviceModelService.GetDeviceByGuid(sclModelInDevice, deviceGuid);
            var devicesclModelInProject = _deviceModelService.GetDeviceByGuid(sclModelInProject, deviceGuid);


            var gooseControlsInDevice = _goosesModelService.GetGooseControlsOfDevice(deviceInsclModelInDevice);
            var gooseControlsInProject = _goosesModelService.GetGooseControlsOfDevice(devicesclModelInProject);


            var projectOnlyGooseControls = GetProjectOnlyGooseControls(gooseControlsInProject, gooseControlsInDevice);
            var deviceOnlyGooseControls = GetDeviceOnlyGooseControls(gooseControlsInProject, gooseControlsInDevice);

            if (isFromDevice)
            {
                foreach (var projectOnlyGooseControl in projectOnlyGooseControls)
                {
                    _goosesModelService.DeleteGooseCbAndGseByName(projectOnlyGooseControl.Name, devicesclModelInProject);
                }
                foreach (var deviceOnlyGooseControl in deviceOnlyGooseControls)
                {
                    _goosesModelService.AddGseControl(deviceOnlyGooseControl.GetFirstParentOfType<ILogicalNode>().Name, deviceOnlyGooseControl.GetFirstParentOfType<ILDevice>().Inst, devicesclModelInProject, deviceOnlyGooseControl);
                    var gseToAdd = _sclCommunicationModelService.GetGsesForDevice(deviceInsclModelInDevice.Name, sclModelInDevice)
                        .FirstOrDefault((gse => gse.CbName == deviceOnlyGooseControl.Name));
                    _sclCommunicationModelService.AddGse(gseToAdd, sclModelInProject, _deviceModelService.GetDeviceByGuid(sclModelInProject, deviceGuid).Name);
                }

            }
            else
            {
                var gooseFtpDtos = new List<GooseFtpDto>();
                foreach (var gooseControlInProject in gooseControlsInProject)
                {
                    var gses = _sclCommunicationModelService.GetGsesForDevice(devicesclModelInProject.Name,
                        sclModelInProject);
                    gooseFtpDtos.Add(GetGooseFtpDto(gooseControlInProject, gses.FirstOrDefault((gse => gse.CbName == gooseControlInProject.Name))));
                }

                var res = await _ftpGooseModelService.WriteGooseDtosToDevice(deviceInsclModelInDevice, gooseFtpDtos);
                if (!res.IsSucceed)
                {
                    return new ResolvingResult(res.GetFirstError());
                }
                return new ResolvingResult() { IsRestartNeeded = true };
            }
            return ResolvingResult.SucceedResult;
        }

        public void ShowConflicts(Guid deviceGuid, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            var deviceInsclModelInDevice = _deviceModelService.GetDeviceByGuid(sclModelInDevice, deviceGuid);
            var devicesclModelInProject = _deviceModelService.GetDeviceByGuid(sclModelInProject, deviceGuid);


            var gooseControlsInDevice = _goosesModelService.GetGooseControlsOfDevice(deviceInsclModelInDevice);
            var gooseControlsInProject = _goosesModelService.GetGooseControlsOfDevice(devicesclModelInProject);


            var projectOnlyGooseControls = GetProjectOnlyGooseControls(gooseControlsInProject, gooseControlsInDevice);
            var deviceOnlyGooseControls = GetDeviceOnlyGooseControls(gooseControlsInProject, gooseControlsInDevice);

            var gooseControlsInDeviceVms =
                _gooseControlViewModelFactory.CreateGooseControlViewModel(deviceInsclModelInDevice,
                    gooseControlsInDevice);
            var gooseControlsInProjectVms =
                _gooseControlViewModelFactory.CreateGooseControlViewModel(devicesclModelInProject,
                    gooseControlsInProject);

            foreach (var gooseControlsInDeviceVm in gooseControlsInDeviceVms)
            {
                gooseControlsInDeviceVm.ChangeTracker.AcceptChanges();
            }

            foreach (var gooseControlsInProjectVm in gooseControlsInProjectVms)
            {
                gooseControlsInProjectVm.ChangeTracker.AcceptChanges();
            }

            projectOnlyGooseControls.ForEach((goose =>
                gooseControlsInProjectVms.FirstOrDefault((model => model.Name == goose.Name))?.ChangeTracker
                    .SetModified()));
            deviceOnlyGooseControls.ForEach((goose =>
                gooseControlsInDeviceVms.FirstOrDefault((model => model.Name == goose.Name))?.ChangeTracker
                    .SetModified()));
            _navigationService.OpenInWindow(GooseKeys.GoosePresentationKeys.GooseControlsConflictsView,
                $"Конфликты в блоках управления Goose устройства {devicesclModelInProject.Name}",
                new BiscNavigationParameters().AddParameterByName(
                    GooseKeys.GoosePresentationKeys.GooseControlsConflictContext,
                    new GooseControlsConflictContext(
                        new ObservableCollection<GooseControlViewModel>(gooseControlsInDeviceVms),
                        new ObservableCollection<GooseControlViewModel>(gooseControlsInProjectVms))));
        }


        private List<IGooseControl> GetDeviceOnlyGooseControls(List<IGooseControl> projectGooseControls,
            List<IGooseControl> deviceGooseControls)
        {
            List<IGooseControl> deviceOnlyGooseControls = new List<IGooseControl>();


            foreach (var deviceGooseControl in deviceGooseControls)
            {
                if (!projectGooseControls.Any((control => control.ModelElementCompareTo(deviceGooseControl))))
                {
                    deviceOnlyGooseControls.Add(deviceGooseControl);
                }
            }
            return deviceOnlyGooseControls;
        }
        private List<IGooseControl> GetProjectOnlyGooseControls(List<IGooseControl> projectGooseControls,
            List<IGooseControl> deviceGooseControls)
        {
            List<IGooseControl> projectOnlyOnlyGooseControls = new List<IGooseControl>();
            foreach (var projectGooseControl in projectGooseControls)
            {
                if (!deviceGooseControls.Any((control => control.ModelElementCompareTo(projectGooseControl))))
                {
                    projectOnlyOnlyGooseControls.Add(projectGooseControl);
                }
            }
            return projectOnlyOnlyGooseControls;
        }
        #endregion
        private GooseFtpDto GetGooseFtpDto(IGooseControl gooseControl, IGse gse)
        {
            var gooseFtpDto = new GooseFtpDto();
            gooseFtpDto.Name = gooseControl.Name;
            gooseFtpDto.AppId = uint.Parse(gse.AppIdDec);
            gooseFtpDto.FixedOffs = gooseControl.FixedOffs;
            gooseFtpDto.GoId = gooseControl.AppId;
            gooseFtpDto.GseType = gooseFtpDto.GseType;
            gooseFtpDto.MacAddress = gse.MacAddress;
            gooseFtpDto.MaxTime = (uint)gse.MaxTime.Value.Value;
            gooseFtpDto.MinTime = (uint)gse.MinTime.Value.Value;
            gooseFtpDto.SelectedDataset = gooseControl.DataSet;
            gooseFtpDto.VlanId = uint.Parse(gse.VlanId);
            gooseFtpDto.VlanPriority = (uint)gse.VlanPriority;
            gooseFtpDto.ConfRev = gooseControl.ConfRev;
            gooseFtpDto.LdInst = gooseControl.GetFirstParentOfType<ILDevice>().Inst;

            return gooseFtpDto;
        }
    }




}
