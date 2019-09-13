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
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Presentation.Interfaces;

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
        private readonly Func<IGooseMatrixRowDescription> _gooseMatrixRowDescriptionFunc;
        private readonly Func<IGooseMatrixSelectableCellViewModel> _gooseMatrixSelectableCellViewModelFunc;

        public GooseControlBlockViewModelFromFtpFactory(
            IGoosesModelService goosesModelService,
            IDatasetModelService datasetModelService,
            Func<GooseControlBlockViewModel> gooseControlBlockViewModelFunc,
            IBiscProject biscProject,
            IDeviceModelService deviceModelService,
            GooseRowViewModelFactory gooseRowViewModelFactory,
            ISclCommunicationModelService sclCommunicationModelService,
            IGooseMatrixFtpService gooseMatrixFtpService,
            IGoCbFtpEntityFactory cbFtpEntityFactory,
            Func<IGooseMatrixRowDescription> gooseMatrixRowDescriptionFunc,
            Func<IGooseMatrixSelectableCellViewModel> gooseMatrixSelectableCellViewModelFunc)
        {
            _gooseRowViewModelFactory = gooseRowViewModelFactory;
            _sclCommunicationModelService = sclCommunicationModelService;
            _gooseMatrixFtpService = gooseMatrixFtpService;
            _cbFtpEntityFactory = cbFtpEntityFactory;
            _gooseMatrixRowDescriptionFunc = gooseMatrixRowDescriptionFunc;
            _gooseMatrixSelectableCellViewModelFunc = gooseMatrixSelectableCellViewModelFunc;

            _goosesModelService = goosesModelService;
            _datasetModelService = datasetModelService;
            _gooseControlBlockViewModelFunc = gooseControlBlockViewModelFunc;
            _biscProject = biscProject;
            _deviceModelService = deviceModelService;
        }


        #region Implementation of IGooseControlBlockViewModelFactory

        public async Task<OperationResult<List<GooseControlBlockViewModel>>> BuildGooseControlBlockViewModels(ISclModel sclModel,
            IDevice device)
        {

            List<GooseControlBlockViewModel> gooseControlBlockViewModels = new List<GooseControlBlockViewModel>();
            List<string> messagesList = new List<string>();

            gooseControlBlockViewModels.AddRange(await BuildBlocks(device));
            return new OperationResult<List<GooseControlBlockViewModel>>(gooseControlBlockViewModels,
                messagesList, true);
        }

        public DataTable BuildGooseMatrixDataTable(ISclModel sclModel, IDevice device)
        {
            if (device.Type == DeviceKeys.DeviceTypes.MR5) return null;
            var table = new DataTable();

            CreateDataTableColumns(table);
            var gooseInputList = _goosesModelService.GetGooseDeviceInputOfProject(_biscProject, device).GooseInputModelInfoList.ToList();
            CreateDataTableRow(gooseInputList, table);

            return table;
        }

        #endregion

        #region private methods

        private void CreateDataTableColumns(DataTable table)
        {
            //Creating columns
            table.Columns.Add(new DataColumn() { ColumnName = "GooseControl", DataType = typeof(IGooseMatrixRowDescription), ReadOnly = true });

            // Columns for MR7XX
            for (int i = 0; i < 64; i++)
            {
                table.Columns.Add(new DataColumn() { ColumnName = $"{i + 1}", DataType = typeof(IGooseMatrixSelectableCellViewModel), ReadOnly = false });
            }
        }

        private void CreateDataTableRow(List<IGooseInputModelInfo> gooseInputModelInfos, DataTable table)
        {
            foreach (var gooseInput in gooseInputModelInfos)
            {
                var parientGooseRow = _gooseMatrixRowDescriptionFunc();
                parientGooseRow.RowType = GooseKeys.GooseSubscriptionRowType.Goose;
                parientGooseRow.DataSetName = gooseInput.EmittingDataSet.Value.Name;
                parientGooseRow.IndexOfFcdaInDataSet = -1;
                parientGooseRow.RowName = gooseInput.GocbRef;
                parientGooseRow.GoCbReference = gooseInput.GocbRef;
                table.Rows.Add(parientGooseRow);

                IGooseMatrixRowDescription gooseRow;
                var rowList = new List<IGooseMatrixRowDescription>();

                foreach (var fcda in gooseInput.EmittingDataSet.Value.FcdaList)
                {
                    gooseRow = _gooseMatrixRowDescriptionFunc();
                    switch (fcda.DaName)
                    {
                        case "q":
                            gooseRow.RowType = GooseKeys.GooseSubscriptionRowType.Quality;
                            break;
                        case "stVal":
                        case "state":
                            gooseRow.RowType = GooseKeys.GooseSubscriptionRowType.State;
                            break;
                        default:
                            continue;
                    }

                    gooseRow.IndexOfFcdaInDataSet = gooseInput.EmittingDataSet.Value.FcdaList.IndexOf(fcda);
                    gooseRow.DoiDataRef = $"{parientGooseRow.GoCbReference}/{fcda.LdInst}.{fcda.Prefix + fcda.LnClass + fcda.LnInst}.{fcda.DoName}";
                    gooseRow.RowName =
                        gooseInput.EmittingDeviceName + " " + gooseInput.EmittingGooseControl.Value.Name +
                        " [" + fcda.DoName + "." + fcda.DaName + "] (" + gooseRow.RowType.ToString() + ")";
                    rowList.Add(gooseRow);
                }

                gooseRow = _gooseMatrixRowDescriptionFunc();
                gooseRow.RowType = GooseKeys.GooseSubscriptionRowType.Separator;
                gooseRow.RowName = GooseKeys.GooseSubscriptionRowType.State.ToString();
                table.Rows.Add(gooseRow);

                foreach (var gooseMatrixRowDescription in rowList.Where(r => r.RowType == GooseKeys.GooseSubscriptionRowType.State))
                {
                    table.Rows.Add(gooseMatrixRowDescription);
                }

                gooseRow = _gooseMatrixRowDescriptionFunc();
                gooseRow.RowType = GooseKeys.GooseSubscriptionRowType.Separator;
                gooseRow.RowName = GooseKeys.GooseSubscriptionRowType.Quality.ToString();
                table.Rows.Add(gooseRow);

                foreach (var gooseMatrixRowDescription in rowList.Where(r => r.RowType == GooseKeys.GooseSubscriptionRowType.Quality))
                {
                    table.Rows.Add(gooseMatrixRowDescription);
                }
            }
        }

        #endregion


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
                        gooseControlBlockViewModel.ColumnsName.Add((i + 1).ToString());
                    }
                    break;
            }

            gooseControlBlockViewModel.GooseRowViewModels =
              await _gooseRowViewModelFactory.BuildGooseRowViewModels(gooseControlBlockViewModel, gooseInput, gooseMatrixFtp);

            return gooseControlBlockViewModel;
        }
    }
}