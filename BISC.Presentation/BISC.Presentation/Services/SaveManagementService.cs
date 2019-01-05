using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Presentation.Infrastructure.Keys;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Presentation.Services
{
   public class SaveManagementService: ISaveManagementService
    {
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly INavigationService _navigationService;
        private readonly IDeviceReconnectionService _deviceReconnectionService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly IBiscProject _biscProject;

        public SaveManagementService(ISaveCheckingService saveCheckingService,INavigationService navigationService,IDeviceReconnectionService deviceReconnectionService,IDeviceModelService deviceModelService,IBiscProject biscProject)
        {
            _saveCheckingService = saveCheckingService;
            _navigationService = navigationService;
            _deviceReconnectionService = deviceReconnectionService;
            _deviceModelService = deviceModelService;
            _biscProject = biscProject;
        }


        public async Task<bool> GetIsRegionCanBeClosed(string regionName)
        {
            if (_saveCheckingService.GetSaveCheckingEntities().Any((entity => entity.RegionName == regionName)))
            {

                SaveResult saveResultEnum = new SaveResult();
                var modifiedEntities = _saveCheckingService.GetSaveCheckingEntities().Where((entity =>
                    entity.RegionName == regionName)).ToList();

                var isReconnectNeeded = false;
                foreach (var modifiedEntity in modifiedEntities)
                {
                    if (!await modifiedEntity.SavingCommand.IsSavingByFtpNeeded()) continue;
                    isReconnectNeeded = true;
                    break;
                }

                if (isReconnectNeeded)
                {
                 await  _deviceReconnectionService.ExecuteBeforeRestart(
                        _deviceModelService.GetDeviceByName(_biscProject.MainSclModel.Value,
                            modifiedEntities.FirstOrDefault().DeviceKey));
                    return true;
                }


                if (modifiedEntities.Any() && modifiedEntities.First().ChangeTracker.GetIsModifiedRecursive())
                {
                    BiscNavigationParameters navigationParameters = new BiscNavigationParameters();
                    navigationParameters.AddParameterByName(SaveCheckingEntity.NavigationKey, modifiedEntities)
                        .AddParameterByName(nameof(SaveResult), saveResultEnum);
                    await _navigationService.NavigateViewToGlobalRegion(KeysForNavigation.ViewNames.SaveChangesViewName,
                        navigationParameters);
                }

                if (saveResultEnum.IsSaved)
                    await _saveCheckingService.ExecuteSave(modifiedEntities);

                return !saveResultEnum.IsCancelled;
            }
            else
            {
                return true;
            }
        }

    }
}
