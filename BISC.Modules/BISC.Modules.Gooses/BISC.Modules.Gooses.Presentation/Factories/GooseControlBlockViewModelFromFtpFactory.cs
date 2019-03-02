using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Presentation.Interfaces.Factories;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Model.Model;
using BISC.Modules.Gooses.Model.Model.Matrix;

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

        public GooseControlBlockViewModelFromFtpFactory(IGoosesModelService goosesModelService,
            IDatasetModelService datasetModelService, Func<GooseControlBlockViewModel> gooseControlBlockViewModelFunc, IBiscProject biscProject, 
            IDeviceModelService deviceModelService, GooseRowViewModelFactory gooseRowViewModelFactory,ISclCommunicationModelService sclCommunicationModelService)
        {
            _gooseRowViewModelFactory = gooseRowViewModelFactory;
            _sclCommunicationModelService = sclCommunicationModelService;

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
            //gooseControlBlockViewModels.AddRange(BuildProjectOnlyBlocks(device));


            //var gooseControlBlocksSubscribed = _goosesModelService.GetGooseControlsSubscribed(device, sclModel);
            //IGooseMatrixFtp gooseMatrix = _goosesModelService.GetGooseMatrixFtpForDevice(device);
            //foreach (var gooseControlBlockSubscribed in gooseControlBlocksSubscribed)
            //{
            //    GooseControlBlockViewModel gooseControlBlockViewModel = _gooseControlBlockViewModelFunc();
            //    gooseControlBlockViewModel.AppId = gooseControlBlockSubscribed.Item2.AppId;
            //    gooseControlBlockViewModel.Name = gooseControlBlockSubscribed.Item2.Name;
            //    gooseControlBlockViewModel.DataSetName = gooseControlBlockSubscribed.Item2.DataSet;

            //    gooseControlBlockViewModel.GoCbReference = gooseControlBlockSubscribed.Item1.Name + "LD0/LLN0$GO$" +
            //                                               gooseControlBlockSubscribed.Item2.Name;


            ////  MR771N127LD0 / LLN0$GO$gcbIn
            //var dataSet = _datasetModelService.GetAllDataSetOfDevice(gooseControlBlockSubscribed.Item1)
            //    .FirstOrDefault((set => set.Name == gooseControlBlockSubscribed.Item2.DataSet));

            //var input = _goosesModelService.GetGooseInputsOfDevice(device).FirstOrDefault();
            //if (input == null) break;
            //List<IGooseRowFtpEntity> rowsForBlock = new List<IGooseRowFtpEntity>();
            //foreach (var externalGooseReference in input.ExternalGooseReferences)
            //{
            //    IGooseRow relatedGooseRow = GetGooseRowForRef(externalGooseReference, gooseMatrix, dataSet);

            //    if (relatedGooseRow == null) continue;
            //    if (externalGooseReference.DaName == "q" || externalGooseReference.DaName == "stVal")
            //    {
            //        rowsForBlock.Add(relatedGooseRow);
            //    }
            //    else
            //    {
            //        messagesList.Add($"Элемент GOOSE.Dataset {externalGooseReference.AsString()} не был принят");
            //    }
            //}

            //CheckBlockRows(rowsForBlock, messagesList);
            //if (rowsForBlock.Count == 0) continue;
            //var validityRowForBlock = GetValidityGooseRow(gooseMatrix, gooseControlBlockSubscribed.Item2.AppId);

            //rowsForBlock.Add(validityRowForBlock);
            //gooseControlBlockViewModel.SetRows(rowsForBlock);

            //   gooseControlBlockViewModels.Add(gooseControlBlockViewModel);


            //  }

            return new OperationResult<List<GooseControlBlockViewModel>>(gooseControlBlockViewModels, messagesList,
                true);
        }

        private IEnumerable<GooseControlBlockViewModel> BuildBlocks(IDevice device)
        {
            List<GooseControlBlockViewModel> gooseControlBlockViewModels = new List<GooseControlBlockViewModel>();
            var input = _goosesModelService.GetGooseInputsOfDevice(device).FirstOrDefault();

            IGooseMatrixFtp gooseMatrix = _goosesModelService.GetGooseMatrixFtpForDevice(device);
            foreach (var goCbFtpEntity in gooseMatrix.GoCbFtpEntities)
            {
                if (input == null || !_deviceModelService.GetDevicesFromModel(_biscProject.MainSclModel.Value).Any((device1 => goCbFtpEntity.GoCbReference.Split('/').FirstOrDefault().Contains(device1.Name))) )
                { 
                    var relatedRows =
                        gooseMatrix.GooseRowFtpEntities.Where((entity =>
                            entity.IndexOfGoose == goCbFtpEntity.IndexOfGoose)).ToList();
                    relatedRows.AddRange(gooseMatrix.GooseRowQualityFtpEntities.Where((row => row.IndexOfGoose == goCbFtpEntity.IndexOfGoose)).ToList());

                    GooseControlBlockViewModel gooseControlBlockViewModel = _gooseControlBlockViewModelFunc();
                    gooseControlBlockViewModel.AppId = goCbFtpEntity.GoCbReference;

                    gooseControlBlockViewModel.GooseRowViewModels =
                        _gooseRowViewModelFactory.CreateGooseFtpOnlyRowsViewModel(relatedRows,gooseControlBlockViewModel);
                    gooseControlBlockViewModels.Add(gooseControlBlockViewModel);
                }
                else
                {
                    gooseControlBlockViewModels.Add(BuildProjectBlock(device,goCbFtpEntity,gooseMatrix,input));

                }
            }
            return gooseControlBlockViewModels;
        }




        private GooseControlBlockViewModel BuildProjectBlock(IDevice device,IGoCbFtpEntity goCbFtpEntity,IGooseMatrixFtp gooseMatrix,IGooseInput gooseInput)
        {
            GooseControlBlockViewModel gooseControlBlockViewModel = _gooseControlBlockViewModelFunc();

            var relatedRows =
                gooseMatrix.GooseRowFtpEntities.Where((entity =>
                    entity.IndexOfGoose == goCbFtpEntity.IndexOfGoose)).ToList();
            relatedRows.AddRange(gooseMatrix.GooseRowQualityFtpEntities.Where((row => row.IndexOfGoose == goCbFtpEntity.IndexOfGoose)).ToList());

            gooseControlBlockViewModel.AppId = goCbFtpEntity.GoCbReference;

          var deviceOfGocb=  _deviceModelService.GetDevicesFromModel(_biscProject.MainSclModel.Value).FirstOrDefault((device1 =>
                goCbFtpEntity.GoCbReference.Split('/').FirstOrDefault().Contains(device1.Name)));


            if (TryGetRelatedGooseControl(out var relatedGooseControl, goCbFtpEntity, deviceOfGocb,
                _biscProject.MainSclModel.Value))
            {
                var datasets = _datasetModelService.GetAllDataSetOfDevice(deviceOfGocb);
                var relatedDataset = datasets.FirstOrDefault((set =>set.Name==relatedGooseControl.DataSet));
                gooseControlBlockViewModel.GooseRowViewModels =
                    _gooseRowViewModelFactory.CreateGooseProjectRowsViewModel(relatedRows, relatedDataset, gooseControlBlockViewModel,gooseInput);
            }
            return gooseControlBlockViewModel;
        }




        private bool TryGetRelatedGooseControl(out IGooseControl relatedControl, IGoCbFtpEntity goCbFtpEntity,IDevice device , ISclModel sclModel)
        {
            relatedControl = null;
           
            if (device == null)
            {
                return false;
            }

            var gooses = _goosesModelService.GetGooseControlsOfDevice(device);

            var gses=_sclCommunicationModelService.GetGsesForDevice(device.Name, _biscProject.MainSclModel.Value);
            var relatedGse = gses.FirstOrDefault((gse => gse.AppId == goCbFtpEntity.AppId));


            var goose = gooses.FirstOrDefault((control => control.Name == relatedGse.CbName));
            if (goose != null)
            {
                relatedControl = goose;
                return true;
            }
            return false;
        }

    }
}