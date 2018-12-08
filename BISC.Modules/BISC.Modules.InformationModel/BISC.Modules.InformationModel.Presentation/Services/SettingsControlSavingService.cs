using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Presentation.Factories;
using BISC.Modules.InformationModel.Presentation.ViewModels.SettingControl;

namespace BISC.Modules.InformationModel.Presentation.Services
{
    public class SettingsControlSavingService
    {
        private readonly IConnectionPoolService _connectionPoolService;


        public SettingsControlSavingService(IConnectionPoolService connectionPoolService)
        {
            _connectionPoolService = connectionPoolService;
        }



        public async Task<OperationResult> SaveSettingControlsAsync(
            List<SettingControlViewModel> settingControlViewModelsToSave,IDevice device)
        {

            //foreach (var settingControlViewModel in settingControlViewModelsToSave)
            //{
                
            //    if(!settingControlViewModel.ChangeTracker.GetIsModifiedRecursive())continue;
            //    _connectionPoolService.GetConnection(device.Ip).MmsConnection.wr

            //}


            //settingControlViewModelsToSave.ForEach((model =>model.ChangeTracker.AcceptChanges() ));
            return OperationResult.SucceedResult;
        }
    }
}
