using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Presentation.ViewModels.GooseControls;

namespace BISC.Modules.Gooses.Presentation.Services
{
    public class GooseControlSavingService
    {
        private readonly IFtpGooseModelService _ftpGooseModelService;
        private readonly IGoosesModelService _goosesModelService;

        public GooseControlSavingService(IFtpGooseModelService ftpGooseModelService,IGoosesModelService goosesModelService)
        {
            _ftpGooseModelService = ftpGooseModelService;
            _goosesModelService = goosesModelService;
        }

        public async Task SaveGooseControls(List<GooseControlViewModel> gooseControlViewModelsToSave,IDevice device,bool isInDevice)
        {
            var goosesExisting = _goosesModelService.GetGooseControlsOfDevice(device);
            
            foreach (var gooseControlViewModel in gooseControlViewModelsToSave)
            {
                if (isInDevice)
                {

                }

            }


          
        }
    }
}
