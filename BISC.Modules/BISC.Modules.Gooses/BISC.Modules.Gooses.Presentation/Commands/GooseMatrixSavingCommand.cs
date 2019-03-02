using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Model.Model;
using BISC.Modules.Gooses.Presentation.Interfaces;
using BISC.Modules.Gooses.Presentation.Interfaces.Factories;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;
using BISC.Presentation.Infrastructure.Commands;

namespace BISC.Modules.Gooses.Presentation.Commands
{
    public class GooseMatrixSavingCommand : ISavingCommand
    {
        private readonly IGoosesModelService _goosesModelService;
        private IDevice _device;
        private IBiscProject _biscProject;
        private ObservableCollection<GooseControlBlockViewModel> _gooseControlBlockViewModels;


        public GooseMatrixSavingCommand(IGoosesModelService goosesModelService, IBiscProject biscProject)
        {
            _goosesModelService = goosesModelService;
            _biscProject = biscProject;
        }
        #region Implementation of ISavingCommand

        public void Initialize(IDevice device, ObservableCollection<GooseControlBlockViewModel> gooseControlBlockViewModels)
        {
            _device = device;
            _gooseControlBlockViewModels = gooseControlBlockViewModels;
        }
        public async Task<OperationResult<SavingCommandResultEnum>> SaveAsync()
        {
            var gooseMatrixFtp = _goosesModelService.GetGooseMatrixFtpForDevice(_device);

            var gooseControlBlocksSubscribed =
         _goosesModelService.GetGooseControlsSubscribed(_device, _biscProject.MainSclModel.Value);

            var oldGocbFtpEntities = gooseMatrixFtp.GoCbFtpEntities.ToList();


            gooseMatrixFtp.GooseRowFtpEntities.Clear();
            gooseMatrixFtp.GooseRowQualityFtpEntities.Clear();


            foreach (var gooseControlBlockViewModel in _gooseControlBlockViewModels)
            {
                foreach (var gooseRowViewModel in gooseControlBlockViewModel.GooseRowViewModels)
                {
                    if (gooseRowViewModel.GooseRowType == "State")
                    {
                        foreach (var selectedValueInRow in gooseRowViewModel.SelectableValueViewModels.Where((model => model.SelectedValue)))
                        {
                            var gooseMatrixRow = new GooseRowFtpEntity
                            {
                                IndexOfGoose = oldGocbFtpEntities.First((entity =>
                                    entity.GoCbReference == gooseControlBlockViewModel.AppId)).IndexOfGoose,
                                BitIndex = selectedValueInRow.ColumnNumber + 1,
                                NumberOfFcdaInDataSetOfGoose = gooseRowViewModel.NumberOfFcdaInDataSet
                            };
                            gooseMatrixFtp.GooseRowFtpEntities.Add(gooseMatrixRow);
                        }
                    }
                    if (gooseRowViewModel.GooseRowType == "Quality")
                    {
                        foreach (var selectedValueInRow in gooseRowViewModel.SelectableValueViewModels.Where((model => model.SelectedValue)))
                        {
                            var gooseMatrixRow = new GooseRowQualityFtpEntity
                            {
                                IndexOfGoose = oldGocbFtpEntities.First((entity =>
                                    entity.GoCbReference == gooseControlBlockViewModel.AppId)).IndexOfGoose,
                                BitIndex = selectedValueInRow.ColumnNumber + 1,
                                NumberOfFcdaInDataSetOfGoose = gooseRowViewModel.NumberOfFcdaInDataSet
                            };
                            IGooseRowViewModel first = gooseControlBlockViewModel.GooseRowViewModels.Where((model => model.GooseRowType == "Validity")).FirstOrDefault();
                            if (first != null)
                                gooseMatrixRow.IsValiditySelected = first.SelectableValueViewModels
                                    .Any((model =>
                                        model.SelectedValue && model.ColumnNumber == selectedValueInRow.ColumnNumber));
                            gooseMatrixFtp.GooseRowQualityFtpEntities.Add(gooseMatrixRow);
                        }
                    }
                }
            }


            foreach (var gooseControlBlockSubscribed in gooseControlBlocksSubscribed)
            {
                GooseControlBlockViewModel gooseControlBlockViewModel =
                    _gooseControlBlockViewModels.FirstOrDefault(
                        (model => model.AppId.Split('$').Last() == gooseControlBlockSubscribed.Item2.AppId));
                if (gooseControlBlockViewModel == null) continue;
                var input = _goosesModelService.GetGooseInputsOfDevice(_device).FirstOrDefault();
                if (input == null) continue;
                input.ExternalGooseReferences.Clear();
                foreach (var gooseRowViewModel in gooseControlBlockViewModel.GooseRowViewModels)
                {
                    if (gooseRowViewModel.GooseRowType == "Validity") continue;
                    IExternalGooseRef externalGooseRef = new ExternalGooseRef();
                    var fcda = gooseRowViewModel.RelatedDataSet.FcdaList[gooseRowViewModel.NumberOfFcdaInDataSet];
                    externalGooseRef.DaName = fcda.DaName;
                    externalGooseRef.DoName = fcda.DoName;
                    externalGooseRef.IedName =
                        gooseRowViewModel.RelatedDataSet.GetFirstParentOfType<IDevice>().Name;
                    externalGooseRef.LdInst = fcda.LdInst;
                    externalGooseRef.LnClass = fcda.LnClass;
                    externalGooseRef.LnInst = fcda.LnInst;
                    externalGooseRef.Prefix = fcda.Prefix;
                    input.ExternalGooseReferences.Add(externalGooseRef);
                }
            }

            _goosesModelService.SetGooseMatrixFtpForDevice(_device, gooseMatrixFtp);
            return new OperationResult<SavingCommandResultEnum>("goven");
        }

        public async Task<bool> IsSavingByFtpNeeded()
        {
            return false;
        }

        public async Task<OperationResult> ValidateBeforeSave()
        {
            return OperationResult.SucceedResult;
        }

        #endregion
    }
}
