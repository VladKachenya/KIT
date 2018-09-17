using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.InformationModel.Infrastucture.Services;

namespace BISC.Modules.Gooses.Model.Services
{
   public class GoosesModelService: IGoosesModelService
    {
        private readonly IInfoModelService _infoModelService;

        public GoosesModelService(IInfoModelService infoModelService)
        {
            _infoModelService = infoModelService;
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
    }
}
