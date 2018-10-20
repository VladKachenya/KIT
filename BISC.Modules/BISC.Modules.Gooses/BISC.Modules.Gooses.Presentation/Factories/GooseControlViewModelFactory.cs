using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Presentation.ViewModels.GooseControls;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;

namespace BISC.Modules.Gooses.Presentation.Factories
{
    public class GooseControlViewModelFactory
    {
        private readonly IGoosesModelService _goosesModelService;
        private readonly ISclCommunicationModelService _sclCommunicationModelService;
        private readonly IDatasetModelService _datasetModelService;
        private readonly IBiscProject _biscProject;
        private readonly IInfoModelService _infoModelService;


        public GooseControlViewModelFactory(IGoosesModelService goosesModelService,
            ISclCommunicationModelService sclCommunicationModelService,
            IDatasetModelService datasetModelService, IBiscProject biscProject, IInfoModelService infoModelService)
        {
            _goosesModelService = goosesModelService;
            _sclCommunicationModelService = sclCommunicationModelService;
            _datasetModelService = datasetModelService;
            _biscProject = biscProject;
            _infoModelService = infoModelService;
        }

        public List<GooseControlViewModel> CreateGooseControlViewModel(IDevice device,
            List<IGooseControl> gooseControls)
        {
            List<GooseControlViewModel> gooseControlViewModels = new List<GooseControlViewModel>();
            foreach (var gooseControl in gooseControls)
            {
                var gooseControlViewModel = new GooseControlViewModel();
                var gses = _sclCommunicationModelService.GetGsesForDevice(device.Name, _biscProject.MainSclModel.Value);
                var gseOfGoose = gses.FirstOrDefault((gse => gse.CbName == gooseControl.Name));
                if (gseOfGoose != null)
                {
                    gooseControlViewModel.AppId = uint.Parse(gseOfGoose.AppId);
                    gooseControlViewModel.MacAddress = gseOfGoose.MacAddress;
                    gooseControlViewModel.MaxTime = (uint) gseOfGoose.MaxTime.Value.Value;
                    gooseControlViewModel.MinTime = (uint) gseOfGoose.MinTime.Value.Value;
                    gooseControlViewModel.VlanId = uint.Parse(gseOfGoose.VlanId);
                    gooseControlViewModel.VlanPriority = (uint) gseOfGoose.VlanPriority;
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

        public GooseControlViewModel CreateGooseControlViewModel(IDevice device)
        {
            var gooseControlViewModel = new GooseControlViewModel();


            gooseControlViewModel.GoId = "NewGoose";
            gooseControlViewModel.Name = "NewGoose";
            gooseControlViewModel.IsDynamic = true;
            gooseControlViewModel.AppId = 0;
            gooseControlViewModel.MacAddress = "01-0C-CD-01-00-00";
            gooseControlViewModel.VlanId = 0;
            gooseControlViewModel.VlanPriority = 4;
            gooseControlViewModel.MaxTime = 10;
            gooseControlViewModel.MinTime = 2000;
            gooseControlViewModel.ChangeTracker.SetNew();


            //var lds = _infoModelService.GetLDevicesFromDevices(device);
            //ILDevice lDevice = null;
            //if (lds == null)
            //{
            //    lDevice = lds.FirstOrDefault((ld => ld.Inst == "LD0"));
            //}

            //if (lDevice == null)
            //{
            //    lDevice = scl.IED[0].AccessPoint[0].Server.LDevice.First();
            //}
            var datasets = _datasetModelService.GetAllDataSetOfDevice(device);
            gooseControlViewModel.AvailableDatasets = datasets.Select((set => set.Name)).ToList();
            gooseControlViewModel.SelectedDataset = gooseControlViewModel.AvailableDatasets.FirstOrDefault();



            return gooseControlViewModel;
        }
    }
}