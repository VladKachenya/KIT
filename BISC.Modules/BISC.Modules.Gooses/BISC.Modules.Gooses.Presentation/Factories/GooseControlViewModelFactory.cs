using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Presentation.ViewModels.GooseControls;

namespace BISC.Modules.Gooses.Presentation.Factories
{
    public class GooseControlViewModelFactory
    {
        private readonly IGoosesModelService _goosesModelService;
        private readonly ISclCommunicationModelService _sclCommunicationModelService;
        private readonly IDatasetModelService _datasetModelService;
        private readonly IBiscProject _biscProject;


        public GooseControlViewModelFactory(IGoosesModelService goosesModelService, ISclCommunicationModelService sclCommunicationModelService,
            IDatasetModelService datasetModelService,IBiscProject biscProject)
        {
            _goosesModelService = goosesModelService;
            _sclCommunicationModelService = sclCommunicationModelService;
            _datasetModelService = datasetModelService;
            _biscProject = biscProject;
        }
        public List<GooseControlViewModel> CreateGooseControlViewModel(IDevice device,List<IGooseControl> gooseControls)
        {

            List<GooseControlViewModel> gooseControlViewModels=new List<GooseControlViewModel>();
            foreach (var gooseControl in gooseControls)
            {
                var gooseControlViewModel = new GooseControlViewModel();
                var gses = _sclCommunicationModelService.GetGsesForDevice(device.Name, _biscProject.MainSclModel.Value);
                var gseOfGoose = gses.FirstOrDefault((gse => gse.CbName == gooseControl.Name));
                if (gseOfGoose != null)
                {
                    gooseControlViewModel.AppId = uint.Parse(gseOfGoose.AppId);
                    gooseControlViewModel.MacAddress = gseOfGoose.MacAddress;
                    gooseControlViewModel.MaxTime = (uint)gseOfGoose.MaxTime.Value.Value;
                    gooseControlViewModel.MinTime = (uint)gseOfGoose.MinTime.Value.Value;
                    gooseControlViewModel.VlanId = uint.Parse(gseOfGoose.VlanId);
                    gooseControlViewModel.VlanPriority = (uint)gseOfGoose.VlanPriority;
                }
                var datasets = _datasetModelService.GetAllDataSetOfDevice(device);
                gooseControlViewModel.GoId = gooseControl.AppId;
                gooseControlViewModel.Name = gooseControl.Name;
                gooseControlViewModel.SelectedDataset = gooseControl.DataSet;
                gooseControlViewModel.AvailableDatasets = datasets.Select((set => set.Name)).ToList();
                gooseControlViewModel.IsDynamic = gooseControl.IsDynamic;
                gooseControlViewModels.Add(gooseControlViewModel);
            }

            return gooseControlViewModels;

        }
    }
}
