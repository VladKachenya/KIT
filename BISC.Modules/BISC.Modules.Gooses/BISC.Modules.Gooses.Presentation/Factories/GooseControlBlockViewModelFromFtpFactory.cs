using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Factorys;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Presentation.Interfaces.Factories;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BISC.Modules.Gooses.Infrastructure.Keys;

namespace BISC.Modules.Gooses.Presentation.Factories
{
    public class GooseControlBlockViewModelFromFtpFactory : IGooseControlBlockViewModelFactory
    {
        private readonly IGoosesModelService _goosesModelService;
        private readonly IDatasetModelService _datasetModelService;
        private readonly Func<GooseControlBlockViewModel> _gooseControlBlockViewModelFunc;
        private readonly IBiscProject _biscProject;
        private readonly IDeviceModelService _deviceModelService;
        private GooseRowViewModelFactory _gooseRowViewModelFactory;
        private readonly ISclCommunicationModelService _sclCommunicationModelService;
        private readonly IGooseMatrixFtpService _gooseMatrixFtpService;
        private readonly IGoCbFtpEntityFactory _cbFtpEntityFactory;

        public GooseControlBlockViewModelFromFtpFactory(IGoosesModelService goosesModelService,
            IDatasetModelService datasetModelService, Func<GooseControlBlockViewModel> gooseControlBlockViewModelFunc, IBiscProject biscProject,
            IDeviceModelService deviceModelService, GooseRowViewModelFactory gooseRowViewModelFactory, ISclCommunicationModelService sclCommunicationModelService,
            IGooseMatrixFtpService gooseMatrixFtpService, IGoCbFtpEntityFactory cbFtpEntityFactory)
        {
            _gooseRowViewModelFactory = gooseRowViewModelFactory;
            _sclCommunicationModelService = sclCommunicationModelService;
            _gooseMatrixFtpService = gooseMatrixFtpService;
            _cbFtpEntityFactory = cbFtpEntityFactory;

            _goosesModelService = goosesModelService;
            _datasetModelService = datasetModelService;
            _gooseControlBlockViewModelFunc = gooseControlBlockViewModelFunc;
            _biscProject = biscProject;
            _deviceModelService = deviceModelService;
        }



        public async Task<OperationResult<List<GooseControlBlockViewModel>>> BuildGooseControlBlockViewModels(ISclModel sclModel,
            IDevice device)
        {

            List<GooseControlBlockViewModel> gooseControlBlockViewModels = new List<GooseControlBlockViewModel>();
            List<string> messagesList = new List<string>();

            gooseControlBlockViewModels.AddRange(await BuildBlocks(device));
            return new OperationResult<List<GooseControlBlockViewModel>>(gooseControlBlockViewModels,
                messagesList, true);
        }

        private async Task<IEnumerable<GooseControlBlockViewModel>> BuildBlocks(IDevice device)
        {
            List<GooseControlBlockViewModel> gooseControlBlockViewModels = new List<GooseControlBlockViewModel>();
            IGooseMatrixFtp gooseMatrix = _gooseMatrixFtpService.GetGooseMatrixFtpForDevice(device);
            var listOfGoosesModelInfos = _goosesModelService.GetGooseDeviceInputOfProject(_biscProject, device).GooseInputModelInfoList.ToList();

            //Работать сдесь
            foreach (var gooseModelInfo in listOfGoosesModelInfos)
            {
                var gooseControlBlockViewModel = await BuildProjectBlock(gooseModelInfo, gooseMatrix, device);
                gooseControlBlockViewModels.Add(gooseControlBlockViewModel);
            }
            return gooseControlBlockViewModels;
        }


        private async Task<GooseControlBlockViewModel> BuildProjectBlock(IGooseInputModelInfo gooseInput, IGooseMatrixFtp gooseMatrixFtp, IDevice parientDevice)
        {
            GooseControlBlockViewModel gooseControlBlockViewModel = _gooseControlBlockViewModelFunc();

            gooseControlBlockViewModel.AppId = gooseInput.EmittingDeviceName + " " + gooseInput.EmittingGooseControl.Value.Name;
            gooseControlBlockViewModel.Name = gooseInput.EmittingGooseControl.Value.Name;
            gooseControlBlockViewModel.GoCbReference = _cbFtpEntityFactory.GetIGoCbFtpEntityFromGooseInputModelInfo(gooseInput);

            switch (parientDevice.Type)
            {
                case DeviceKeys.DeviceTypes.MR5:
                    gooseControlBlockViewModel.IsConsigerTheQuality = false;
                    gooseControlBlockViewModel.ColumnsName = new List<string>();
                    gooseControlBlockViewModel.ColumnsName.Add(GooseKeys.GoInNameKeys.IndicationResetKey);
                    gooseControlBlockViewModel.ColumnsName.Add(GooseKeys.GoInNameKeys.FaultResetKey);
                    gooseControlBlockViewModel.ColumnsName.Add(GooseKeys.GoInNameKeys.SystemLogResetKey);
                    gooseControlBlockViewModel.ColumnsName.Add(GooseKeys.GoInNameKeys.AlarmLogResetKey);
                    gooseControlBlockViewModel.ColumnsName.Add(GooseKeys.GoInNameKeys.TurnOffBreaker);
                    gooseControlBlockViewModel.ColumnsName.Add(GooseKeys.GoInNameKeys.TurnOnBreaker);
                    break;
                default:
                    gooseControlBlockViewModel.IsConsigerTheQuality = true;
                    gooseControlBlockViewModel.ColumnsName = new List<string>();
                    for (int i = 0; i < 64; i++)
                    {
                        gooseControlBlockViewModel.ColumnsName.Add(i.ToString());
                    }
                    break;
            }

            gooseControlBlockViewModel.GooseRowViewModels =
              await _gooseRowViewModelFactory.BuildGooseRowViewModels(gooseControlBlockViewModel, gooseInput, gooseMatrixFtp);

            return gooseControlBlockViewModel;
        }
    }
}