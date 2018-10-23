using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Model.Model;
using BISC.Modules.Gooses.Presentation.ViewModels.GooseControls;

namespace BISC.Modules.Gooses.Presentation.Services
{
    public class GooseControlSavingService
    {
        private readonly IFtpGooseModelService _ftpGooseModelService;
        private readonly IGoosesModelService _goosesModelService;

        public GooseControlSavingService(IFtpGooseModelService ftpGooseModelService, IGoosesModelService goosesModelService)
        {
            _ftpGooseModelService = ftpGooseModelService;
            _goosesModelService = goosesModelService;
        }

        public async Task<OperationResult> SaveGooseControls(List<GooseControlViewModel> gooseControlViewModelsToSave, IDevice device, bool isInDevice)
        {
            var goosesExisting = _goosesModelService.GetGooseControlsOfDevice(device);
            if (isInDevice)
            {
                List<GooseFtpDto> gooseFtpDtos = gooseControlViewModelsToSave.Where((model => model.IsDynamic)).Select((GetGooseFtpDtosFromViewModel)).ToList();

                var res = await _ftpGooseModelService.WriteGooseDtosToDevice(device.Ip, gooseFtpDtos);

                if (!res.IsSucceed)
                {
                    return new OperationResult(res.GetFirstError());
                }

            }
            foreach (var gooseControlViewModel in gooseControlViewModelsToSave)
            {
                if (!gooseControlViewModel.IsDynamic) continue;


                if (gooseControlViewModel.ChangeTracker.GetIsModifiedRecursive())
                {
                    var existingGooseControl = goosesExisting.FirstOrDefault(control => control.Name == gooseControlViewModel.Name);
                    if (existingGooseControl != null)
                    {
                        MapGooseControlFromViewModel(existingGooseControl, gooseControlViewModel);
                    }
                    else
                    {
                        
                    }
                }


            }

            foreach (var gooseControlExisting in goosesExisting)
            {
                if (!gooseControlViewModelsToSave.Any((model => model.Name == gooseControlExisting.Name)))
                {
                    _goosesModelService.DeleteGooseCbAndGseByName(gooseControlExisting.Name, device);
                }
            }
            return OperationResult.SucceedResult;


        }

        private GooseFtpDto GetGooseFtpDtosFromViewModel(GooseControlViewModel gooseControlViewModel)
        {
            return new GooseFtpDto();
        }


        private void MapGooseControlFromViewModel(IGooseControl gooseControl, GooseControlViewModel gooseControlViewModel)
        {

        }
    }
}
