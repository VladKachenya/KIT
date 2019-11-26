using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.HelpClasses;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.InformationModel.Model.Elements;
using BISC.Modules.InformationModel.Presentation.Factories;
using BISC.Modules.InformationModel.Presentation.Helpers;
using BISC.Modules.InformationModel.Presentation.ViewModels.SettingControl;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.InformationModel.Presentation.Services
{
    //Legacy
    public class SettingControlConflictResolver : IElementConflictResolver
    {
        private IDeviceModelService _deviceModelService;
        private IConnectionPoolService _connectionPoolService;
        private readonly IInfoModelService _infoModelService;
        private BISC.Presentation.Infrastructure.Services.INavigationService _navigationService;
        private readonly SettingControlViewModelFactory _settingControlViewModelFactory;
        public ConflictType ConflictType => ConflictType.AutomaticallyResolvingFromDevice;
        public SettingControlConflictResolver(IDeviceModelService deviceModelService, IConnectionPoolService connectionPoolService, IInfoModelService infoModelService,
            BISC.Presentation.Infrastructure.Services.INavigationService navigationService, SettingControlViewModelFactory settingControlViewModelFactory)
        {
            _deviceModelService = deviceModelService;
            _connectionPoolService = connectionPoolService;
            _infoModelService = infoModelService;
            _navigationService = navigationService;
            _settingControlViewModelFactory = settingControlViewModelFactory;
        }



        #region Implementation of IElementConflictResolver

        public bool GetIfConflictsExists(Guid deviceGuid, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            var deviceInsclModelInDevice = _deviceModelService.GetDeviceByGuid(sclModelInDevice, deviceGuid);
            var devicesclModelInProject = _deviceModelService.GetDeviceByGuid(sclModelInProject, deviceGuid);

            var settingsGroupsInDevice = _infoModelService.GetSettingControlsOfDevice(deviceInsclModelInDevice);
            var settingsGroupsInProject = _infoModelService.GetSettingControlsOfDevice(devicesclModelInProject);

            if (settingsGroupsInDevice.Count != settingsGroupsInProject.Count)
            {
                return true;
            }

            foreach (var settingControlInDevice in settingsGroupsInDevice)
            {
                if (!GetIsSettingsGroupsCollectionHasControl(settingsGroupsInProject, settingControlInDevice))
                {
                    return true;
                }
            }
            return false;
        }

        private bool GetIsSettingsGroupsCollectionHasControl(List<ISettingControl> collectionSettingControls,
            ISettingControl settingControlToCheck)
        {
            var settingControlInProject = collectionSettingControls.FirstOrDefault((sc => sc.GetFirstParentOfType<ILDevice>().Inst == settingControlToCheck.GetFirstParentOfType<ILDevice>().Inst));
            if (settingControlInProject == null) return false;
            return settingControlToCheck.ModelElementCompareTo(settingControlInProject);
        }


        public async Task<ResolvingResult> ResolveConflict(bool isFromDevice, Guid deviceGuid, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            var deviceInsclModelInDevice = _deviceModelService.GetDeviceByGuid(sclModelInDevice, deviceGuid);
            var devicesclModelInProject = _deviceModelService.GetDeviceByGuid(sclModelInProject, deviceGuid);

            var settingsGroupsInDevice = _infoModelService.GetSettingControlsOfDevice(deviceInsclModelInDevice);
            var settingsGroupsInProject = _infoModelService.GetSettingControlsOfDevice(devicesclModelInProject);

            foreach (var settingControl in settingsGroupsInDevice)
            {
                var settingControlInDevice = settingsGroupsInProject.FirstOrDefault((sc => sc.GetFirstParentOfType<ILDevice>().Inst == settingControl.GetFirstParentOfType<ILDevice>().Inst));
                if (settingControlInDevice != null)
                {
                    settingControlInDevice.ActSG = settingControl.ActSG;
                }
                else
                {
                    var newSettingsControl = new SettingControl
                    {
                        ActSG = settingControl.ActSG,
                        NumOfSGs = settingControl.NumOfSGs
                    };
                    var ldevice= _infoModelService.GetLDevicesFromDevices(devicesclModelInProject).FirstOrDefault((device =>
                            device.Inst == settingControl.GetFirstParentOfType<ILDevice>().Inst));
                    if (ldevice != null)
                    {
                        ldevice.LogicalNodeZero.Value.SettingControl.Value = newSettingsControl;
                    }
                      

                }
            }

            return ResolvingResult.SucceedResult;
        }
        public string ConflictName => "Setting Groups";
        public void ShowConflicts(Guid deviceGuid, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            var deviceInsclModelInDevice = _deviceModelService.GetDeviceByGuid(sclModelInDevice, deviceGuid);
            var devicesclModelInProject = _deviceModelService.GetDeviceByGuid(sclModelInProject, deviceGuid);

            var settingsGroupsInDevice = _infoModelService.GetSettingControlsOfDevice(deviceInsclModelInDevice);
            var settingsGroupsInProject = _infoModelService.GetSettingControlsOfDevice(devicesclModelInProject);
            var settingsGroupsInDeviceVms = new List<SettingControlViewModel>();
            foreach (var settingControlInDevice in settingsGroupsInDevice)
            {
                var vm = _settingControlViewModelFactory.CreateSettingControlViewModel(settingControlInDevice);
                if (GetIsSettingsGroupsCollectionHasControl(settingsGroupsInProject, settingControlInDevice))
                {
                   vm.ChangeTracker.SetModified();
                }
                settingsGroupsInDeviceVms.Add(vm);
            }
            var settingsGroupsInProjectVms = new List<SettingControlViewModel>();

            foreach (var settingControlInProject in settingsGroupsInProject)
            {
                var vm = _settingControlViewModelFactory.CreateSettingControlViewModel(settingControlInProject);
                if (GetIsSettingsGroupsCollectionHasControl(settingsGroupsInDevice, settingControlInProject))
                {
                    vm.ChangeTracker.SetModified();
                }
                settingsGroupsInProjectVms.Add(vm);
            }

            _navigationService.OpenInWindow(InfoModelKeys.SettingsControlConflictsViewKey, $"Конфликты Settings Group в устройстве {devicesclModelInProject.Name}",
                new BiscNavigationParameters().AddParameterByName(InfoModelKeys.SettingsControlConflictsContextKey,
                    new SettingsControlConflictsContext(settingsGroupsInDeviceVms, settingsGroupsInProjectVms)));

        }



        #endregion
    }
}
