using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Common;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;
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
            var connection = _connectionPoolService.GetConnection(device.Ip).MmsConnection;

            foreach (var settingControlViewModel in settingControlViewModelsToSave)
            {

                if (!settingControlViewModel.ChangeTracker.GetIsModifiedRecursive()) continue;
                var lDevice = settingControlViewModel.Model.GetFirstParentOfType<ILDevice>();
             
              await  connection.SetSettingsControl("SP", device.Name, "LLN0", lDevice.Inst,
                    settingControlViewModel.ActiveSettingsGroup.Value.ToString());
            }


            settingControlViewModelsToSave.ForEach((model => model.ChangeTracker.AcceptChanges()));
            return OperationResult.SucceedResult;
        }
    }
}
