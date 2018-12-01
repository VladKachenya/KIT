using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Common;
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
        private readonly IUniqueNameService _uniqueNameService;



        public GooseControlViewModelFactory(IGoosesModelService goosesModelService,
            ISclCommunicationModelService sclCommunicationModelService,
            IDatasetModelService datasetModelService, IBiscProject biscProject, IInfoModelService infoModelService, IUniqueNameService uniqueNameService)
        {
            _goosesModelService = goosesModelService;
            _sclCommunicationModelService = sclCommunicationModelService;
            _datasetModelService = datasetModelService;
            _biscProject = biscProject;
            _infoModelService = infoModelService;
            _uniqueNameService = uniqueNameService;
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
                    if (gseOfGoose.AppId != null)
                        gooseControlViewModel.AppId = uint.Parse(gseOfGoose.AppIdDec);
                    gooseControlViewModel.MacAddress = gseOfGoose.MacAddress;
                    if (gseOfGoose.MaxTime.Value != null)
                    {
                        gooseControlViewModel.MaxTime = (uint) gseOfGoose.MaxTime.Value.Value;
                    }

                    if (gseOfGoose.MinTime.Value != null)
                        gooseControlViewModel.MinTime = (uint) gseOfGoose.MinTime.Value.Value;

                    if (gseOfGoose.VlanId != null) gooseControlViewModel.VlanId = uint.Parse(gseOfGoose.VlanId);
                    if (gseOfGoose.VlanPriority != null) gooseControlViewModel.VlanPriority = (uint)gseOfGoose.VlanPriority;

                    gooseControlViewModel.VlanPriority = (uint) gseOfGoose.VlanPriority;
                    gooseControlViewModel.LdInst = gseOfGoose.LdInst;

                }

                var datasets = _datasetModelService.GetAllDataSetOfDevice(device);
                gooseControlViewModel.GoId = gooseControl.AppId;
                gooseControlViewModel.Name = gooseControl.Name;
                gooseControlViewModel.SelectedDataset = gooseControl.DataSet;
                gooseControlViewModel.AvailableDatasets = datasets.Select((set => set.Name)).ToList();
                gooseControlViewModel.IsDynamic = gooseControl.IsDynamic;
                gooseControlViewModel.ConfRev = gooseControl.ConfRev;
                gooseControlViewModel.FixedOffs = gooseControl.FixedOffs;
                gooseControlViewModel.GseType = gooseControl.GooseType;
                gooseControlViewModel.IsEditable = true;
                gooseControlViewModels.Add(gooseControlViewModel);
            }

            return gooseControlViewModels;
        }

        public GooseControlViewModel CreateGooseControlViewModel(IDevice device, List<string> existingNames)
        {
            var gooseControlViewModel = new GooseControlViewModel();

            gooseControlViewModel.Name = _uniqueNameService.GetUniqueName(existingNames, "NewGoose");
            gooseControlViewModel.GoId = gooseControlViewModel.Name;
            gooseControlViewModel.IsDynamic = true;
            gooseControlViewModel.AppId = 0;
            gooseControlViewModel.MacAddress = "01-0C-CD-01-00-00";
            gooseControlViewModel.VlanId = 0;
            gooseControlViewModel.VlanPriority = 4;
            gooseControlViewModel.MaxTime = 2000;
            gooseControlViewModel.MinTime = 10;
            gooseControlViewModel.ConfRev = 0;
            gooseControlViewModel.IsEditable = true;
            var ldevices = new List<ILDevice>();
            device.GetAllChildrenOfType(ref ldevices);
            var ldevice = ldevices.FirstOrDefault((lDevice => lDevice.Inst == "LD0"));

            if (ldevice == null)
                ldevice = ldevices.FirstOrDefault();
            gooseControlViewModel.ConfRev = 1;
            gooseControlViewModel.LdInst = ldevice.Inst;
            gooseControlViewModel.GseType = "GOOSE";
            gooseControlViewModel.FixedOffs = false;
            gooseControlViewModel.ChangeTracker.SetModified();


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