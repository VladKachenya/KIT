using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Presentation.Interfaces.Factories;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BISC.Modules.Gooses.Infrastructure.Factorys;

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



        public OperationResult<List<GooseControlBlockViewModel>> BuildGooseControlBlockViewModels(ISclModel sclModel,
            IDevice device)
        {

            List<GooseControlBlockViewModel> gooseControlBlockViewModels = new List<GooseControlBlockViewModel>();
            List<string> messagesList = new List<string>();

            gooseControlBlockViewModels.AddRange(BuildBlocks(device));
            return new OperationResult<List<GooseControlBlockViewModel>>(gooseControlBlockViewModels,
                messagesList, true);
        }

        private IEnumerable<GooseControlBlockViewModel> BuildBlocks(IDevice device)
        {
            List<GooseControlBlockViewModel> gooseControlBlockViewModels = new List<GooseControlBlockViewModel>();
            IGooseMatrixFtp gooseMatrix = _gooseMatrixFtpService.GetGooseMatrixFtpForDevice(device);
            var listOfGoosesModelInfos = _goosesModelService.GetGooseDeviceInputOfProject(_biscProject, device).GooseInputModelInfoList.ToList();

            //Работать сдесь
            foreach (var gooseModelInfo in listOfGoosesModelInfos)
            {
                var gooseControlBlockViewModel = BuildProjectBlock(gooseModelInfo, gooseMatrix);
                gooseControlBlockViewModels.Add(gooseControlBlockViewModel);

                ////Тут создаются строчки
                //if (false && relatedGoCb != null)
                //{
                //    var relatedRows = gooseMatrix.GooseRowFtpEntities.Where((entity =>
                //            entity.IndexOfGoose == relatedGoCb.IndexOfGoose)).ToList();

                //    relatedRows.AddRange(gooseMatrix.GooseRowQualityFtpEntities.Where((row => row.IndexOfGoose == relatedGoCb.IndexOfGoose)).ToList());

                //    gooseControlBlockViewModel.GooseRowViewModels =
                //        _gooseRowViewModelFactory.CreateGooseFtpRowsViewModel(relatedRows, gooseControlBlockViewModel);

                //    gooseControlBlockViewModels.Add(gooseControlBlockViewModel);
                //}
            }
            return gooseControlBlockViewModels;
        }




        private GooseControlBlockViewModel BuildProjectBlock(IGooseInputModelInfo gooseInput, IGooseMatrixFtp gooseMatrixFtp)
        {
            GooseControlBlockViewModel gooseControlBlockViewModel = _gooseControlBlockViewModelFunc();

            gooseControlBlockViewModel.AppId = gooseInput.EmittingDeviceName + " " + gooseInput.EmittingGooseControl.Value.Name;
            gooseControlBlockViewModel.Name = gooseInput.EmittingGooseControl.Value.Name;
            gooseControlBlockViewModel.GoCbReference = _cbFtpEntityFactory.GetIGoCbFtpEntityFromGooseInputModelInfo(gooseInput);

            gooseControlBlockViewModel.GooseRowViewModels =
                _gooseRowViewModelFactory.BuildGooseRowViewModels(gooseControlBlockViewModel, gooseInput, gooseMatrixFtp);



            return gooseControlBlockViewModel;
        }
    }
}