using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Model.Model.Matrix;
using BISC.Modules.Gooses.Presentation.Interfaces.Factories;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;

namespace BISC.Modules.Gooses.Presentation.Factories
{
    public class GooseControlBlockViewModelFactory: IGooseControlBlockViewModelFactory
    {
        private readonly IGoosesModelService _goosesModelService;
        private readonly IDatasetModelService _datasetModelService;
        private readonly Func<GooseControlBlockViewModel> _gooseControlBlockViewModelFunc;
        public GooseControlBlockViewModelFactory(IGoosesModelService goosesModelService , IDatasetModelService datasetModelService, Func<GooseControlBlockViewModel> gooseControlBlockViewModelFunc)
        {
            _goosesModelService = goosesModelService;
            _datasetModelService = datasetModelService;
            _gooseControlBlockViewModelFunc = gooseControlBlockViewModelFunc;
        }

        #region Implementation of IGooseControlBlockViewModelFactory

        public OperationResult<List<GooseControlBlockViewModel>> BuildGooseControlBlockViewModels(ISclModel sclModel, IDevice device)
        {
            List<GooseControlBlockViewModel> gooseControlBlockViewModels=new List<GooseControlBlockViewModel>();
            List<string> messagesList=new List<string>();


            var gooseControlBlocksSubscribed = _goosesModelService.GetGooseControlsSubscribed(device, sclModel);
            IGooseMatrix gooseMatrix = _goosesModelService.GetGooseMatrixForDevice(device);
            foreach (var gooseControlBlockSubscribed in gooseControlBlocksSubscribed)
            {
                GooseControlBlockViewModel gooseControlBlockViewModel = _gooseControlBlockViewModelFunc();
                gooseControlBlockViewModel.AppId = gooseControlBlockSubscribed.Item2.AppId;
                gooseControlBlockViewModel.Name = gooseControlBlockSubscribed.Item2.Name;
                gooseControlBlockViewModel.DataSetName = gooseControlBlockSubscribed.Item2.DataSet;

                gooseControlBlockViewModel.GoCbReference = gooseControlBlockSubscribed.Item1.Name + "LD0/LLN0$GO$" +
                                                           gooseControlBlockSubscribed.Item2.Name;

                //  MR771N127LD0 / LLN0$GO$gcbIn
                var dataSet = _datasetModelService.GetAllDataSetOfDevice(gooseControlBlockSubscribed.Item1).FirstOrDefault((set => set.Name == gooseControlBlockSubscribed.Item2.DataSet));

                var input = _goosesModelService.GetGooseInputsOfDevice(device).FirstOrDefault();
                if (input == null) break;
                List<IGooseRow> rowsForBlock = new List<IGooseRow>();
                foreach (var externalGooseReference in input.ExternalGooseReferences)
                {
                    IGooseRow relatedGooseRow = GetGooseRowForRef(externalGooseReference, gooseMatrix, dataSet);

                    if (relatedGooseRow == null) continue;
                    if (externalGooseReference.DaName == "q" || externalGooseReference.DaName == "stVal")
                    {
                        rowsForBlock.Add(relatedGooseRow);
                    }
                    else
                    {
                        messagesList.Add($"Элемент GOOSE.Dataset {externalGooseReference.AsString()} не был принят");
                    }
                }
                CheckBlockRows(rowsForBlock, messagesList);
                if (rowsForBlock.Count == 0) continue;
                var validityRowForBlock = GetValidityGooseRow(gooseMatrix, gooseControlBlockSubscribed.Item2.AppId);

                rowsForBlock.Add(validityRowForBlock);
                gooseControlBlockViewModel.SetRows(rowsForBlock);

                gooseControlBlockViewModels.Add(gooseControlBlockViewModel);


            }
            return new OperationResult<List<GooseControlBlockViewModel>>(gooseControlBlockViewModels,messagesList,true);
        }
        private void CheckBlockRows(List<IGooseRow> rowsForBlock,List<string> messagesList)
        {
            List<IGooseRow> rowsToRemove = new List<IGooseRow>();
            foreach (var gooseRow in rowsForBlock)
            {
                var part1 = gooseRow.ReferencePath.Split('.').First();
                var part2 = gooseRow.ReferencePath.Split('.').Last();
                switch (part2)
                {
                    case "stVal":
                        if (!rowsForBlock.Any((row =>
                            row.ReferencePath.Split('.').Last() == "q" && row.ReferencePath.Split('.').First() == part1)))
                        {
                            messagesList.Add($"Элемент состояния GOOSE.Dataset  {gooseRow.Signature} не дублируется качеством");
                            rowsToRemove.Add(gooseRow);
                        }

                        break;
                    case "q":
                        if (!rowsForBlock.Any((row =>
                            row.ReferencePath.Split('.').Last() == "stVal" && row.ReferencePath.Split('.').First() == part1)))
                        {
                            messagesList.Add($"Элемент качества GOOSE.Dataset  {gooseRow.Signature} не дублируется состоянием");
                            rowsToRemove.Add(gooseRow);
                        }

                        break;
                }
            }

            rowsToRemove.ForEach((row => rowsForBlock.Remove(row)));
        }

        private IGooseRow GetGooseRowForRef(IExternalGooseRef externalGooseRef, IGooseMatrix gooseMatrix, IDataSet dataset)
        {
          
            int fcdaNum = -1;
            foreach (var fcda in dataset.FcdaList)
            {
                if (CompareFcdaAndExtRef(externalGooseRef, fcda))
                {
                    fcdaNum = dataset.FcdaList.IndexOf(fcda);
                    foreach (var gooseRow in gooseMatrix.GooseRows)
                    {
                        if (gooseRow.ReferencePath == externalGooseRef.AsString())
                        {
                            return gooseRow;
                        }
                    }
                    break;
                }
            }
            if (fcdaNum == -1)
            {
                return null;
            }
            string type = externalGooseRef.DaName == "q" ? "Quality" : externalGooseRef.DaName == "stVal" ? "State" : "Unknown";
            return new GooseRow() { NumberOfFcdaInDataSetOfGoose = fcdaNum, ReferencePath = externalGooseRef.AsString(), Signature = externalGooseRef.AsString(), ValueList = new bool[64].ToList(), GooseRowType = type };

        }
        private IGooseRow GetValidityGooseRow(IGooseMatrix gooseMatrix, string gooseBlockName)
        {
            foreach (var gooseRow in gooseMatrix.GooseRows)
            {
                if (gooseRow.ReferencePath == gooseBlockName && gooseRow.GooseRowType == "Validity")
                {
                    return gooseRow;
                }
            }
            return new GooseRow() { ReferencePath = gooseBlockName, Signature = gooseBlockName + ".Validity", ValueList = new bool[64].ToList(), GooseRowType = "Validity" };

        }

        private bool CompareFcdaAndExtRef(IExternalGooseRef externalGooseRef, IFcda fcda)
        {
            if (externalGooseRef.Prefix != fcda.Prefix) return false;
            if (externalGooseRef.DaName != fcda.DaName) return false;
            if (externalGooseRef.DoName != fcda.DoName) return false;
            if (externalGooseRef.LdInst != fcda.LdInst) return false;
            if (externalGooseRef.LnInst != fcda.LnInst) return false;
            if (externalGooseRef.LnClass != fcda.LnClass) return false;
            return true;
        }

        #endregion
    }
}